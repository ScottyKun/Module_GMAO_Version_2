using GMAO_Business.Entities;
using GMAO_Business.KPI;
using GMAO_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.Services
{
    public class KPIService
    {
        private readonly KpiRepository repository;
        private readonly int userId;

        public KPIService()
        {
            repository = new KpiRepository();
            userId = UserContext.IdUser;
        }

        private List<int> GetAccessibleEquipementIds()
        {
            return repository.GetAccessibleEquipementIds(userId);
        }

        private void SaveRapport(string titre, string type, object donnees)
        {
            repository.SaveRapport(userId, titre, type, donnees);
        }

        public List<KpiResult> GetTopEquipementsCout()
        {
            var ids = GetAccessibleEquipementIds();
            var workOrders = repository.GetWorkOrdersWithEquipements(ids);

            var data = workOrders
                .GroupBy(wo =>
                    wo.MaintenanceCorrective?.Equipement?.nom ??
                    wo.Intervention?.MaintenancePlanifiee?.Equipement?.nom)
                .Where(g => g.Key != null)
                .Select(g => new KpiResult
                {
                    Label = g.Key,
                    Value = g.Sum(w => w.Cout)
                })
                .OrderByDescending(k => k.Value)
                .Take(5)
                .ToList();

            SaveRapport("Top 5 équipements les plus coûteux (correctives + planifiées)", "TopEquipementsCout", data);
            return data;
        }

        public List<KpiResult> GetTopPiecesUtilisation()
        {
            var ids = GetAccessibleEquipementIds();
            var data = repository.GetWorkOrderPieces(ids)
                .GroupBy(wop => wop.Piece.nom)
                .Select(g => new KpiResult
                {
                    Label = g.Key,
                    Value = g.Sum(wop => wop.Quantite)
                })
                .OrderByDescending(k => k.Value)
                .Take(5)
                .ToList();

            SaveRapport("Top 5 pièces les plus utilisées généré", "Top 5 pièces", data);
            return data;
        }

        public decimal GetCoutTotalMaintenanceCorrective()
        {
            var ids = GetAccessibleEquipementIds();
            var total = repository.GetQueryableWorkOrders(ids)
                .Where(wo => wo.MaintenanceCorrective != null)
                .Sum(wo => (decimal?)wo.Cout) ?? 0;

            SaveRapport("Coût total des maintenances correctives généré", "CoutTotalCorrective", total);
            return total;
        }

        public decimal GetCoutTotalMaintenancePlanifiee()
        {
            var ids = GetAccessibleEquipementIds();
            var total = repository.GetQueryableWorkOrders(ids)
                .Where(wo => wo.Intervention != null && wo.Intervention.MaintenancePlanifiee != null)
                .Sum(wo => (decimal?)wo.Cout) ?? 0;

            SaveRapport("Coût total des maintenances planifiées généré", "CoutTotalPlanifiee", total);
            return total;
        }

        public decimal GetCoutTotalMaintenance()
        {
            var ids = GetAccessibleEquipementIds();
            var total = repository.GetQueryableWorkOrders(ids)
                .Sum(wo => (decimal?)wo.Cout) ?? 0;

            SaveRapport("Coût total global de la maintenance généré", "CoutTotalGlobal", total);
            return total;
        }

        public decimal GetCoutMoyenMaintenanceCorrective()
        {
            var ids = GetAccessibleEquipementIds();
            var moyenne = repository.GetQueryableWorkOrders(ids)
                .Where(wo => wo.MaintenanceCorrective != null)
                .Average(wo => (decimal?)wo.Cout) ?? 0;

            SaveRapport("Coût moyen par maintenance corrective généré", "CoutMoyenCorrective", moyenne);
            return moyenne;
        }

        public decimal GetCoutMoyenInterventionPlanifiee()
        {
            var ids = GetAccessibleEquipementIds();
            var moyenne = repository.GetQueryableWorkOrders(ids)
                .Where(wo => wo.Intervention != null && wo.Intervention.MaintenancePlanifiee != null)
                .Average(wo => (decimal?)wo.Cout) ?? 0;

            SaveRapport("Coût moyen par intervention planifiée généré", "CoutMoyenPlanifiee", moyenne);
            return moyenne;
        }

        public List<KpiResult> GetCoutParEquipement()
        {
            var ids = GetAccessibleEquipementIds();
            var workOrders = repository.GetWorkOrdersWithEquipements(ids);

            var result = workOrders
                .GroupBy(wo =>
                    wo.MaintenanceCorrective?.Equipement?.nom ??
                    wo.Intervention?.MaintenancePlanifiee?.Equipement?.nom)
                .Where(g => g.Key != null)
                .Select(g => new KpiResult
                {
                    Label = g.Key,
                    Value = g.Sum(w => w.Cout)
                })
                .OrderByDescending(r => r.Value)
                .ToList();

            SaveRapport("Coût total par équipement généré", "CoutParEquipement", result);
            return result;
        }

        public List<KpiResult> GetRepartitionTypeMaintenance()
        {
            var total = GetCoutTotalMaintenance();
            if (total == 0) return new List<KpiResult>();

            var corrective = GetCoutTotalMaintenanceCorrective();
            var planifiee = GetCoutTotalMaintenancePlanifiee();

            var result = new List<KpiResult>
        {
            new KpiResult { Label = "Corrective", Value = Math.Round((corrective / total) * 100, 2) },
            new KpiResult { Label = "Planifiée", Value = Math.Round((planifiee / total) * 100, 2) }
        };

            SaveRapport("Répartition des coûts par type de maintenance générée", "RepartitionTypeMaintenance", result);
            return result;
        }

        public List<KpiBudgetResult> GetCoutPrevuVsReelParAnnee()
        {
            var ids = GetAccessibleEquipementIds();

            var result = repository.GetBudgets()
                .Select(b => new KpiBudgetResult
                {
                    Annee = b.Annee,
                    BudgetPrevu = b.Montant,
                    CoutReel = repository.GetQueryableWorkOrders(ids)
                        .Where(wo => wo.DateExecution.Year == b.Annee)
                        .Sum(wo => (decimal?)wo.Cout) ?? 0
                })
                .ToList();

            SaveRapport("Coût prévu vs coût réel par année généré", "CoutPrevuVsReel", result);
            return result;
        }

        public List<KpiBudgetMensuelResult> GetDepenseMensuelleCumulative(int annee)
        {
            var budget = repository.GetBudgets().FirstOrDefault(b => b.Annee == annee);
            if (budget == null) return new List<KpiBudgetMensuelResult>();

            decimal budgetMensuel = budget.Montant / 12;
            var ids = GetAccessibleEquipementIds();

            var depenses = repository.GetQueryableWorkOrders(ids)
                .Where(wo => wo.DateExecution.Year == annee)
                .GroupBy(wo => wo.DateExecution.Month)
                .ToDictionary(g => g.Key, g => g.Sum(w => w.Cout));

            decimal cumul = 0;

            var result = Enumerable.Range(1, 12)
                .Select(mois =>
                {
                    cumul += depenses.ContainsKey(mois) ? depenses[mois] : 0;
                    return new KpiBudgetMensuelResult
                    {
                        Mois = mois,
                        CoutReel = cumul,
                        BudgetLisse = budgetMensuel * mois
                    };
                })
                .ToList();

            SaveRapport("Dépenses mensuelles cumulées générées", "DepensesMensuelles", result);
            return result;
        }


        public decimal GetPourcentageBudgetUtilise(int annee)
        {
            var budget = repository.GetBudgetMontant(annee);
            if (budget == 0)
                return 0;

            var coutReel = repository.GetCoutReelParAnnee(annee);
            var pourcentage = (coutReel / budget) * 100;
            SaveRapport($"% budget utilisé en {annee} calculé", $"PourcentageBudgetUtilise_{annee}", pourcentage);
            return pourcentage;
        }


        public List<KpiMonthlyEvolutionResult> GetEvolutionMensuelleDesCouts(int annee)
        {
            var raw = repository.GetCoutsMensuelsParType(annee);

            var result = raw.Select(x => new KpiMonthlyEvolutionResult
            {
                Mois = x.Mois,
                CoutCorrective = x.CoutCorrective,
                CoutPlanifiee = x.CoutPlanifiee
            }).ToList();

            SaveRapport($"Évolution mensuelle des coûts pour {annee}", $"EvolutionMensuelle_{annee}", result);
            return result;
        }

        public List<KpiBudgetResult> GetEcartBudgetParAnnee()
        {
            var raw = repository.GetBudgetsEtCouts();

            var result = raw.Select(x => new KpiBudgetResult
            {
                Annee = x.Annee,
                BudgetPrevu = x.BudgetPrevu,
                CoutReel = x.CoutReel,
                Ecart = x.BudgetPrevu - x.CoutReel
            }).ToList();

            SaveRapport("Écart budget par année", "EcartBudget", result);
            return result;
        }

        public List<KpiResult> GetTauxDepassementBudget()
        {
            var raw = repository.GetDepassementsBudget();

            var result = raw.Select(x => new KpiResult
            {
                Label = x.Annee.ToString(),
                Value = Math.Round(((x.CoutReel - x.BudgetPrevu) / x.BudgetPrevu) * 100, 2)
            }).ToList();

            SaveRapport("Taux de dépassement du budget", "TauxDepassementBudget", result);
            return result;
        }

        public List<KpiBudgetMoisResult> GetCoutPrevuVsReelParMois()
        {
            var accessiblesIds = GetAccessibleEquipementIds();
            var raw = repository.GetCoutPrevuReelParMois(accessiblesIds);

            var result = raw.Select(x => new KpiBudgetMoisResult
            {
                Annee = x.Annee,
                Mois = x.Mois,
                CoutPrevu = x.CoutPrevu,
                CoutReel = x.CoutReel
            }).ToList();

            SaveRapport("Coût prévu vs réel mensuel", "CoutPrevuVsReel", result);
            return result;
        }

        public List<KpiEcartParTypeMaintenanceResult> GetEcartParTypeMaintenanceParEquipement()
        {
            var accessiblesIds = GetAccessibleEquipementIds();

            var rawResults = repository.GetEcartParTypeMaintenanceParEquipement(accessiblesIds);

            var result = rawResults.Select(x => new KpiEcartParTypeMaintenanceResult
            {
                Equipement = x.Equipement,
                TypeMaintenance = x.TypeMaintenance,
                CoutPrevu = x.CoutPrevu,
                CoutReel = x.CoutReel
            }).ToList();

            SaveRapport("Écart coût prévu/réel par équipement et type", "EcartParTypeMaintenance", result);
            return result;
        }


    }

}
