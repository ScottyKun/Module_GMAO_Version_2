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
    public class KpiService2
    {
        private readonly KpiRepository2 _repository;
        private readonly int userId;

        public KpiService2()
        {
            _repository = new KpiRepository2();
            userId = UserContext.IdUser;
        }

        private void SaveRapport(string titre, string type, object donnees)
        {
            _repository.SaveRapport(userId, titre, type, donnees);
        }

        private List<int> GetAccessibleEquipementIds()
        {
            return _repository.GetAccessibleEquipementIds(userId);
        }

        public List<KpiABCResult> GetClassificationABC()
        {
            var ids = GetAccessibleEquipementIds();
            var rawData = _repository.GetCoutsParEquipement(ids);

            var total = rawData.Sum(x => x.CoutTotal);
            decimal cumul = 0;
            var result = new List<KpiABCResult>();

            foreach (var item in rawData.OrderByDescending(x => x.CoutTotal))
            {
                cumul += item.CoutTotal;
                var pourcentage = cumul / total;

                var categorie = pourcentage <= 0.8m ? "A"
                              : pourcentage <= 0.95m ? "B"
                              : "C";

                result.Add(new KpiABCResult
                {
                    EquipementId = item.EquipementId,
                    NomEquipement = item.NomEquipement,
                    CoutTotal = item.CoutTotal,
                    CategorieABC = categorie
                });
            }

            SaveRapport("Classification ABC générée", "ClassificationABC", result);
            return result;
        }

        public List<KpiTauxPanneResult> GetTauxPanneParEquipement(DateTime debut, DateTime fin)
        {
            var ids = GetAccessibleEquipementIds();
            var data = _repository.GetNbPannesParEquipement(ids, debut, fin);

            var result = data.Select(x => new KpiTauxPanneResult
            {
                EquipementId = x.EquipementId,
                NomEquipement = x.NomEquipement,
                NbPannes = x.NbPannes
            }).ToList();

            SaveRapport($"Nb de pannes entre {debut:yyyy-MM-dd} et {fin:yyyy-MM-dd}", "TauxPanne", result);
            return result;
        }

        public List<KpiMTTRResult> GetMTTRParEquipement()
        {
            var accessiblesIds = GetAccessibleEquipementIds();
            var data = _repository.GetMTTRData(accessiblesIds);

            var result = data
                .GroupBy(d => d.EquipementId)
                .Select(g => new KpiMTTRResult
                {
                    EquipementId = g.Key,
                    NomEquipement = _repository.GetNomEquipement(g.Key),
                    MTTRenHeures = g.Average(x => x.Duree)
                })
                .ToList();

            SaveRapport("MTTRParEquipement générée", "MTTR", result);
            return result;
        }

        public List<KpiMTBFResult> GetMTBFParEquipement()
        {
            var accessiblesIds = GetAccessibleEquipementIds();
            var data = _repository.GetDatesPannesPourMTBF(accessiblesIds);
            var aujourdHui = DateTime.Now;

            var result = data
                .GroupBy(d => d.EquipementId)
                .Select(g =>
                {
            // Liste ordonnée des dates de pannes
            var dates = g
                        .Select(x => x.Date)
                        .Distinct()
                        .OrderBy(d => d)
                        .ToList();

                    if (dates.Count == 0)
                        return null;

            // Inclure aujourd’hui comme "panne fictive" si dernière panne < aujourd’hui
            if (dates.Last() < aujourdHui)
                    {
                        dates.Add(aujourdHui);
                    }

                    var deltas = new List<double>(); // en heures

            for (int i = 1; i < dates.Count; i++)
                    {
                        deltas.Add((dates[i] - dates[i - 1]).TotalHours); // Intervalles en heures
            }

                    return new KpiMTBFResult
                    {
                        EquipementId = g.Key,
                        NomEquipement = _repository.GetNomEquipement(g.Key),
                        MTBFenJours = (deltas.Count > 0 ? deltas.Average() : 0) / 24
                    };
                })
                .Where(x => x != null)
                .ToList();

            SaveRapport("MTBFParEquipement générée (avec aujourd'hui)", "MTBF", result);
            return result;
        }


        public List<KpiPlanificationResult> GetTauxPlanificationParResponsable()
        {
            var raw = _repository.GetPlanificationParResponsable();
            var result = raw.Select(x => new KpiPlanificationResult
            {
                Responsable = $"{x.Nom} {x.Prenom}",
                NbPrevues = x.NbPrevues,
                NbRealisees = x.NbRealisees
            }).ToList();

            SaveRapport("Taux d'intervention planifiee effectue par responsable", "% interventions effectuees", result);
            return result;
        }

        public List<KpiClotureWoResult> GetTauxClotureWoParResponsable(DateTime dateDebut, DateTime dateFin)
        {
            var raw = _repository.GetClotureWoParResponsable(dateDebut, dateFin);

            var result = raw.Select(x => new KpiClotureWoResult
            {
                Responsable = $"{x.Nom} {x.Prenom}",
                TotalWO = x.TotalWO,
                Termines = x.Termines
            }).ToList();

            SaveRapport("Taux de cloture de WO", "% WO terminee dans le mois", result);
            return result;
        }

        public List<KpiEquipeTempsMoyen> GetTempsMoyenInterventionParEquipe()
        {
            var data = _repository.GetTempsMoyenInterventionParEquipe();

            var result = data.Select(x => new KpiEquipeTempsMoyen
            {
                Equipe = x.NomEquipe,
                TempsMoyenHeures = x.TempsMoyenHeures
            }).ToList();

            SaveRapport("Temps moyen d'intervention", "Temps moyen d'intervention", result);
            return result;
        }


        public List<KpiEquipeReussite> GetTauxReussiteMaintenanceParEquipe()
        {
            var data = _repository.GetTauxReussiteMaintenanceParEquipe();

            var result= data.Select(x => new KpiEquipeReussite
            {
                Equipe = x.NomEquipe,
                Total = x.Total,
                Reussites = x.Reussites,
                TauxReussite = x.Total > 0 ? Math.Round((double)x.Reussites / x.Total * 100, 2) : 0
            }).ToList();

            SaveRapport("Taux de réussite des maintenances", "Taux de réussite des maintenances", result);
            return result;
        }

    }
}
