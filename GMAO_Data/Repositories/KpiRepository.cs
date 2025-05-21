using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class KpiRepository
    {
        private readonly DbManager db;

        public KpiRepository()
        {
            db = new DbManager();
        }

        public User GetUserWithEquipements(int userId)
        {
            return db.Set<User>()
                     .Include("equipementsResponsable")
                     .FirstOrDefault(u => u.idUser == userId);
        }

        public List<int> GetAccessibleEquipementIds(int userId)
        {
            var user = GetUserWithEquipements(userId);
            if (user == null) return new List<int>();

            bool isAdmin = user.fonction != null && user.fonction == "Administrateur";
            if (isAdmin)
                return db.Set<Equipement>().Select(e => e.id).ToList();

            return user.equipementsResponsable.Select(e => e.id).ToList();
        }

        public List<WorkOrder> GetWorkOrdersWithEquipements(List<int> equipementIds)
        {
            return db.Set<WorkOrder>()
                .Where(wo =>
                    (wo.MaintenanceCorrective != null && equipementIds.Contains(wo.MaintenanceCorrective.EquipementId)) ||
                    (wo.Intervention != null && wo.Intervention.MaintenancePlanifiee != null &&
                     equipementIds.Contains(wo.Intervention.MaintenancePlanifiee.EquipementId)))
                .ToList();
        }

        public IQueryable<WorkOrder> GetQueryableWorkOrders(List<int> equipementIds)
        {
            return db.Set<WorkOrder>().Where(wo =>
                (wo.MaintenanceCorrective != null && equipementIds.Contains(wo.MaintenanceCorrective.EquipementId)) ||
                (wo.Intervention != null && wo.Intervention.MaintenancePlanifiee != null &&
                 equipementIds.Contains(wo.Intervention.MaintenancePlanifiee.EquipementId)));
        }

        public IQueryable<WorkOrder_Piece> GetWorkOrderPieces(List<int> equipementIds)
        {
            return db.Set<WorkOrder_Piece>()
                .Where(wop =>
                    (wop.WorkOrder.MaintenanceCorrective != null && equipementIds.Contains(wop.WorkOrder.MaintenanceCorrective.EquipementId)) ||
                    (wop.WorkOrder.Intervention != null && wop.WorkOrder.Intervention.MaintenancePlanifiee != null &&
                     equipementIds.Contains(wop.WorkOrder.Intervention.MaintenancePlanifiee.EquipementId)));
        }

        public IQueryable<Budget> GetBudgets()
        {
            return db.Set<Budget>();
        }

        public void SaveRapport(int userId, string titre, string type, object donnees)
        {
            var json = JsonConvert.SerializeObject(donnees);
            var last = db.Set<Rapport>()
                         .Where(r => r.UserId == userId && r.Type == type)
                         .OrderByDescending(r => r.DateCreation)
                         .FirstOrDefault();

            if (last != null && last.DonneesJson == json)
                return;

            db.Set<Rapport>().Add(new Rapport
            {
                UserId = userId,
                Titre = titre,
                Type = type,
                DonneesJson = json,
                DateCreation = DateTime.Now
            });

            db.SaveChanges();
        }

        public decimal GetBudgetMontant(int annee)
        {
            return db.Set<Budget>().Where(b => b.Annee == annee).Select(b => b.Montant).FirstOrDefault();
        }

        public decimal GetCoutReelParAnnee(int annee)
        {
            return db.Set<WorkOrder>()
                     .Where(wo => wo.DateExecution.Year == annee)
                     .Sum(wo => (decimal?)wo.Cout) ?? 0;
        }

        public List<(int Mois, decimal CoutCorrective, decimal CoutPlanifiee)> GetCoutsMensuelsParType(int annee)
        {
            return Enumerable.Range(1, 12).Select(mois =>
            {
                var coutCorrective = db.Set<WorkOrder>()
                    .Where(wo => wo.MaintenanceCorrective != null &&
                                 wo.DateExecution.Year == annee &&
                                 wo.DateExecution.Month == mois)
                    .Sum(wo => (decimal?)wo.Cout) ?? 0;

                var coutPlanifiee = db.Set<WorkOrder>()
                    .Where(wo => wo.InterventionId != null &&
                                 wo.DateExecution.Year == annee &&
                                 wo.DateExecution.Month == mois)
                    .Sum(wo => (decimal?)wo.Cout) ?? 0;

                return (mois, coutCorrective, coutPlanifiee);
            }).ToList();
        }

        public List<(int Annee, decimal BudgetPrevu, decimal CoutReel)> GetBudgetsEtCouts()
        {
            return db.Set<Budget>()
                .Select(b => new
                {
                    b.Annee,
                    BudgetPrevu = b.Montant,
                    CoutReel = db.Set<WorkOrder>()
                        .Where(wo => wo.DateExecution.Year == b.Annee)
                        .Sum(wo => (decimal?)wo.Cout) ?? 0
                })
                .ToList()
                .Select(x => (x.Annee, x.BudgetPrevu, x.CoutReel))
                .ToList();
        }

        public List<(int Annee, decimal BudgetPrevu, decimal CoutReel)> GetDepassementsBudget()
        {
            return db.Set<Budget>()
                .Select(b => new
                {
                    b.Annee,
                    b.Montant,
                    CoutReel = db.Set<WorkOrder>()
                        .Where(wo => wo.DateExecution.Year == b.Annee)
                        .Sum(wo => (decimal?)wo.Cout) ?? 0
                })
                .Where(x => x.CoutReel > x.Montant)
                .ToList()
                .Select(x => (x.Annee, x.Montant, x.CoutReel))
                .ToList();
        }

        public List<(int Annee, int Mois, decimal CoutPrevu, decimal CoutReel)> GetCoutPrevuReelParMois(List<int> accessiblesIds)
        {
            var mcData = db.Set<MaintenanceCorrective>()
                .Where(mc => accessiblesIds.Contains(mc.EquipementId) && mc.WorkOrder != null)
                .Select(mc => new
                {
                    mc.DateCreation,
                    CoutPrevu = mc.Cout,
                    CoutReel = mc.WorkOrder.Cout
                });

            var mpData = db.Set<MaintenancePlanifiee>()
                .Where(mp => accessiblesIds.Contains(mp.EquipementId))
                .Select(mp => new
                {
                    mp.DateCreation,
                    CoutPrevu = mp.CoutPrevu,
                    CoutReel = mp.CoutReel
                });

            return mcData
                .Concat(mpData)
                .ToList()
                .GroupBy(x => new { x.DateCreation.Year, x.DateCreation.Month })
                .Select(g => (
                    g.Key.Year,
                    g.Key.Month,
                    g.Sum(x => x.CoutPrevu),
                    g.Sum(x => x.CoutReel)
                ))
                .OrderBy(x => x.Item1)
                .ThenBy(x => x.Item2)
                .ToList();
        }

        public void SupprimerTousLesRapports()
        {
            var rapports = db.Set<Rapport>().ToList();
            if (rapports.Any())
            {
                db.Rapports.RemoveRange(rapports);
                db.SaveChanges();
            }
        }

        public List<(string Equipement, string TypeMaintenance, decimal CoutPrevu, decimal CoutReel)> GetEcartParTypeMaintenanceParEquipement(List<int> accessiblesIds)
        {
            var correctives = db.Set<MaintenanceCorrective>()
                .Where(mc => accessiblesIds.Contains(mc.EquipementId))
                .Select(mc => new
                {
                    Equipement = mc.Equipement.nom,
                    TypeMaintenance = "Corrective",
                    CoutPrevu = mc.Cout,
                    CoutReel = mc.WorkOrder != null ? mc.WorkOrder.Cout : 0
                });

            var planifiees = db.Set<MaintenancePlanifiee>()
                .Where(mp => accessiblesIds.Contains(mp.EquipementId))
                .Select(mp => new
                {
                    Equipement = mp.Equipement.nom,
                    TypeMaintenance = "Planifiée",
                    CoutPrevu = mp.CoutPrevu,
                    CoutReel = mp.CoutReel
                });

            return correctives
                .Concat(planifiees)
                .AsEnumerable() // pour faire la projection vers le tuple côté C#
                .Select(x => (x.Equipement, x.TypeMaintenance, x.CoutPrevu, x.CoutReel))
                .ToList();
        }


    }
}
