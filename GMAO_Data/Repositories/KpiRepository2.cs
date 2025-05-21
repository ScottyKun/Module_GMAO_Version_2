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
    public class KpiRepository2
    {
        private readonly DbManager db;

        public KpiRepository2()
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

        public List<(int EquipementId, string NomEquipement, decimal CoutTotal)> GetCoutsParEquipement(List<int> accessiblesIds)
        {
            var data = db.Set<WorkOrder>()
                .Where(wo =>
                    (wo.MaintenanceCorrective != null && accessiblesIds.Contains(wo.MaintenanceCorrective.EquipementId)) ||
                    (wo.Intervention != null && wo.Intervention.MaintenancePlanifiee != null &&
                     accessiblesIds.Contains(wo.Intervention.MaintenancePlanifiee.EquipementId)))
                .Select(wo => new
                {
                    EquipementId = wo.MaintenanceCorrective != null
                        ? wo.MaintenanceCorrective.EquipementId
                        : wo.Intervention.MaintenancePlanifiee.EquipementId,
                    Cout = wo.Cout
                })
                .ToList();

            var grouped = data
                .GroupBy(x => x.EquipementId)
                .Select(g => new
                {
                    EquipementId = g.Key,
                    CoutTotal = g.Sum(x => x.Cout),
                    NomEquipement = db.Equipements.Where(e => e.id == g.Key).Select(e => e.nom).FirstOrDefault()
                })
                .ToList();

            return grouped
                .Select(x => (x.EquipementId, x.NomEquipement, x.CoutTotal))
                .ToList();
        }

        public List<(int EquipementId, string NomEquipement, int NbPannes)> GetNbPannesParEquipement(List<int> accessiblesIds, DateTime debut, DateTime fin)
        {
            // On inclut la date d'aujourd'hui dans la plage (fin inclusif + 1 jour)
            var finInclusif = fin.Date.AddDays(1).AddTicks(-1);

            // Pannes correctives dans la période
            var pannesCorrectives = db.MaintenancesCorrectives
                .Where(m => accessiblesIds.Contains(m.EquipementId) && m.DateCreation >= debut && m.DateCreation <= finInclusif)
                .Select(m => new { m.EquipementId });

            // Interventions en échec dans la période (lié à un équipement via MaintenancePlanifiee)
            var pannesEchec = db.Interventions
                .Where(i => i.Etat == "Echec"
                         && i.DatePrevue >= debut
                         && i.DatePrevue <= finInclusif
                         && i.MaintenancePlanifiee != null
                         && accessiblesIds.Contains(i.MaintenancePlanifiee.EquipementId))
                .Select(i => new { EquipementId = i.MaintenancePlanifiee.EquipementId });

            // Concaténer les deux sources (pannes correctives + échecs)
            var toutesPannes = pannesCorrectives.Concat(pannesEchec);

            // Groupement par équipement + comptage distinct
            var query = toutesPannes
                .GroupBy(p => p.EquipementId)
                .Select(g => new
                {
                    EquipementId = g.Key,
                    NomEquipement = db.Equipements.Where(e => e.id == g.Key).Select(e => e.nom).FirstOrDefault(),
                    NbPannes = g.Count()
                });

            return query.AsEnumerable()
                        .Select(x => (x.EquipementId, x.NomEquipement, x.NbPannes))
                        .ToList();
        }


        public List<(int EquipementId, double Duree)> GetMTTRData(List<int> accessiblesIds)
        {
            return db.WorkOrders
                .Where(wo => wo.MaintenanceCorrective != null && wo.Terminee &&
                             accessiblesIds.Contains(wo.MaintenanceCorrective.EquipementId))
                .Select(wo => new
                {
                    wo.MaintenanceCorrective.EquipementId,
                    DateExecution = wo.DateExecution,
                    DateCreation = wo.MaintenanceCorrective.DateCreation
                })
                .AsEnumerable() // passage en mémoire
                .Select(wo => (
                    wo.EquipementId,
                    (wo.DateExecution - wo.DateCreation).TotalHours
                ))
                .ToList();
        }


        public List<(int EquipementId, DateTime Date)> GetDatesPannesPourMTBF(List<int> accessiblesIds)
        {
            var correctives = db.MaintenancesCorrectives
                .Where(m => accessiblesIds.Contains(m.EquipementId))
                .Select(m => new { m.EquipementId, m.DateCreation })
                .ToList()
                .Select(m => (m.EquipementId, m.DateCreation));

            var echecsPlanifies = db.Interventions
                .Include("MaintenancePlanifiee")
                .Where(i => i.Etat == "Echec" &&
                            i.MaintenancePlanifiee != null &&
                            accessiblesIds.Contains(i.MaintenancePlanifiee.EquipementId))
                .Select(i => new { i.MaintenancePlanifiee.EquipementId, i.DatePrevue })
                .ToList()
                .Select(i => (i.EquipementId, i.DatePrevue));

            return correctives.Concat(echecsPlanifies).ToList();
        }



        public string GetNomEquipement(int equipementId)
        {
            return db.Equipements
                .Where(e => e.id == equipementId)
                .Select(e => e.nom)
                .FirstOrDefault();
        }

        public List<(int ResponsableId, string Nom, string Prenom, int NbPrevues, int NbRealisees)> GetPlanificationParResponsable()
        {
            var responsables = db.Users.Where(u => u.fonction == "Responsable").ToList();

            var maintenances = db.MaintenancesPlanifiees
                .Include("Interventions")
                .ToList();

            return responsables.Select(responsable =>
            {
                var mResp = maintenances.Where(m => m.ResponsableId == responsable.idUser);
                return (
                    ResponsableId: responsable.idUser,
                    Nom: responsable.nom,
                    Prenom: responsable.prenom,
                    NbPrevues: mResp.Sum(m => m.NbInterventions),
                    NbRealisees: mResp.Sum(m => m.NbInterventionsFinish)
                );
            }).ToList();
        }


        public List<(int ResponsableId, string Nom, string Prenom, int TotalWO, int Termines)> GetClotureWoParResponsable(DateTime debut, DateTime fin)
        {
            var workOrders = db.WorkOrders
                .Include("MaintenanceCorrective")
                .Include("Intervention")
                    .Include("Intervention.MaintenancePlanifiee")
                .Where(wo => wo.DateExecution >= debut && wo.DateExecution <= fin)
                .ToList();

            var responsables = db.Users.Where(u => u.fonction == "Responsable").ToList();


            return responsables.Select(r =>
            {
                var userId = r.idUser;

                var woResponsable = workOrders.Where(wo =>
                    (wo.MaintenanceCorrective != null && wo.MaintenanceCorrective.ResponsableId == userId) ||
                    (wo.Intervention != null && wo.Intervention.MaintenancePlanifiee != null &&
                     wo.Intervention.MaintenancePlanifiee.ResponsableId == userId)).ToList();

                return (
                    ResponsableId: r.idUser,
                    Nom: r.nom,
                    Prenom: r.prenom,
                    TotalWO: woResponsable.Count,
                    Termines: woResponsable.Count(wo => wo.Terminee)
                );
            }).ToList();
        }

        public List<(string NomEquipe, double TempsMoyenHeures)> GetTempsMoyenInterventionParEquipe()
        {
            var equipes = db.Maintenance_Teams.ToList();

            var result = equipes.Select(eq =>
            {
                var workOrders = db.WorkOrders
                   .Include("MaintenanceCorrective")
                .Include("Intervention")
                    .Include("Intervention.MaintenancePlanifiee")
                    .Where(wo =>
                        (wo.MaintenanceCorrective != null && wo.MaintenanceCorrective.EquipeId == eq.teamId) ||
                        (wo.Intervention != null && wo.Intervention.MaintenancePlanifiee != null &&
                         wo.Intervention.MaintenancePlanifiee.EquipeId == eq.teamId))
                    .ToList();

                var durees = workOrders
                    .Select(wo =>
                    {
                        if (wo.MaintenanceCorrective != null)
                            return (wo.DateExecution - wo.MaintenanceCorrective.DateCreation).TotalHours;
                        else if (wo.Intervention?.MaintenancePlanifiee != null)
                            return (wo.DateExecution - wo.Intervention.MaintenancePlanifiee.DateCreation).TotalHours;
                        else
                            return 0.0;
                    })
                    .ToList();

                double moyenne = durees.Any() ? Math.Round(durees.Average(), 2) : 0;

                return (NomEquipe: eq.nom, TempsMoyenHeures: moyenne);
            }).ToList();

            return result;
        }

        public List<(string NomEquipe, int Total, int Reussites)> GetTauxReussiteMaintenanceParEquipe()
        {
            var equipes = db.Maintenance_Teams.ToList();

            var maintenances = db.Maintenances.ToList();

            var workOrders = db.WorkOrders
                .Include("MaintenanceCorrective")
                .Include("Intervention")
                .Include("Intervention.MaintenancePlanifiee")
                .ToList();

            return equipes.Select(eq =>
            {
                var maintsEquipe = maintenances.Where(m => m.EquipeId == eq.teamId).ToList();
                int total = maintsEquipe.Count;

                int reussies = maintsEquipe.Count(m =>
                {
                    var dernierWO = workOrders
                        .Where(wo =>
                            (wo.MaintenanceCorrective != null && wo.MaintenanceCorrective.MaintenanceId == m.MaintenanceId) ||
                            (wo.Intervention?.MaintenancePlanifiee != null &&
                             wo.Intervention.MaintenancePlanifiee.MaintenanceId == m.MaintenanceId))
                        .OrderByDescending(wo => wo.DateExecution)
                        .FirstOrDefault();

                    if (dernierWO == null) return false;

                    var dateFin = dernierWO.DateExecution;
                    var dateLimite = dateFin.AddDays(30);

                    bool nouvelleMaintenance = maintenances.Any(mx =>
                        mx.EquipementId == m.EquipementId &&
                        mx.DateCreation > dateFin &&
                        mx.DateCreation <= dateLimite);

                    return !nouvelleMaintenance;
                });

                return (eq.nom, total, reussies);
            }).ToList();
        }



    }
}
