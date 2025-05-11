using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using GMAO_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.Services
{
    public class PlanningService
    {
        private readonly PlanningRepository _repository;
        private readonly AlerteService _alerteService;

        public PlanningService()
        {
            _repository = new PlanningRepository();
            _alerteService = new AlerteService();
        }

        public void PlanifierProchainesInterventions(int maintenanceId)
        {
            var maintenance = _repository.GetMaintenanceWithInterventions(maintenanceId);
            if (maintenance == null) return;

            var datesRecurrence = CalculerDatesRecurrence(maintenance.DateDebut, maintenance.DateFin, maintenance.RecurrenceJours);
            var interventionsExistantes = maintenance.Interventions.Select(i => i.DatePrevue.Date).ToList();

            foreach (var date in datesRecurrence)
            {
                bool interventionExiste = interventionsExistantes.Contains(date);

                if (date == DateTime.Today)
                {
                    if (!interventionExiste)
                    {
                        _alerteService.Ajouter(new Alerte
                        {
                            Libelle = "Alerte Maintenance",
                            Message = $"Aujourd'hui {date:dd/MM/yyyy} était prévu pour une intervention ({maintenance.Description}) mais aucune n'a été créée.",
                            Priorite = "Élevée",
                            ResponsableId = maintenance.ResponsableId,
                            DateCreation = DateTime.Now,
                            Terminee = false
                        });
                    }
                    else
                    {
                        var intervention = maintenance.Interventions.FirstOrDefault(i => i.DatePrevue.Date == date);
                        if (intervention != null && intervention.Etat != "Terminee" && intervention.Etat != "Echec")
                        {
                            _alerteService.Ajouter(new Alerte
                            {
                                Libelle = "Alerte Maintenance",
                                Message = $"L’intervention prévue aujourd'hui {date:dd/MM/yyyy} pour {maintenance.Description} n’est pas encore terminée.",
                                Priorite = "Élevée",
                                ResponsableId = maintenance.ResponsableId,
                                DateCreation = DateTime.Now,
                                Terminee = false
                            });
                        }
                    }
                }
                else if (date == DateTime.Today.AddDays(2) && !interventionExiste)
                {
                    _alerteService.Ajouter(new Alerte
                    {
                        Libelle = "Alerte Maintenance",
                        Message = $"Dans 2 jours ({date:dd/MM/yyyy}), une intervention est prévue : {maintenance.Description}",
                        Priorite = "Moyenne",
                        ResponsableId = maintenance.ResponsableId,
                        DateCreation = DateTime.Now,
                        Terminee = false
                    });
                }
                else if (date < DateTime.Today && !interventionExiste)
                {
                    _alerteService.Ajouter(new Alerte
                    {
                        Libelle = "Alerte Maintenance",
                        Message = $"Aucune intervention créée pour la date de récurrence {date:dd/MM/yyyy} - {maintenance.Description}",
                        Priorite = "Élevée",
                        ResponsableId = maintenance.ResponsableId,
                        DateCreation = DateTime.Now,
                        Terminee = false
                    });
                }
            }

            _repository.Save();
        }

        public void PlanifierProchainesInterventionsPourToutes()
        {
            var ids = _repository.GetNonTermineesMaintenanceIds();
            foreach (var id in ids)
                PlanifierProchainesInterventions(id);
        }

        public void PlanifierPourResponsable(int responsableId)
        {
            var ids = _repository.GetMaintenanceIdsForResponsable(responsableId);
            foreach (var id in ids)
                PlanifierProchainesInterventions(id);
        }

        public void NettoyerAnciennesAlertes(int delaiJours = 2)
        {
            var alertes = _repository.GetOldAlertes(delaiJours);
            foreach (var alerte in alertes)
                _alerteService.Supprimer(alerte.Id);

            _repository.Save();
        }

        public void VerifierStockEtGenererAlertes()
        {
            var piecesCritiques = _repository.GetPiecesCritiques();
            var alertesDuJour = _repository.GetMessagesAlertesStockDuJour();

            foreach (var piece in piecesCritiques)
            {
                if (!alertesDuJour.Any(msg => msg.StartsWith(piece.nom + ",")))
                {
                    _repository.AjouterAlerte(new Alerte
                    {
                        Libelle = "Alerte Stock",
                        Message = $"{piece.nom}, référence{piece.reference} est insuffisant. Accéder à la gestion des stocks pour commander.",
                        Priorite = "Élevée",
                        DateCreation = DateTime.Now,
                        Terminee = false
                    });
                }
            }

            _repository.Save();
        }

        public void VerifierMaintenancesCorrectivesSansWO(int responsableId)
        {
            var maintenances = _repository.GetCorrectivesSansWorkOrder(responsableId);
            var alertesDuJour = _repository.GetMessagesAlertesCorrectivesDuJour();

            foreach (var m in maintenances)
            {
                if (!alertesDuJour.Any(msg => msg.StartsWith($"La maintenance corrective \"{m.Description}\"")))
                {
                    _repository.AjouterAlerte(new Alerte
                    {
                        Libelle = "Alerte Corrective",
                        Message = $"La maintenance corrective \"{m.Description}\" n’a toujours pas de WorkOrder après 2 jours.",
                        Priorite = "Élevée",
                        ResponsableId = m.ResponsableId,
                        DateCreation = DateTime.Now,
                        Terminee = false
                    });
                }
            }

            _repository.Save();
        }

        private List<DateTime> CalculerDatesRecurrence(DateTime dateDebut, DateTime dateFin, int recurrence)
        {
            var dates = new List<DateTime>();
            for (var date = dateDebut.Date; date <= dateFin.Date; date = date.AddDays(recurrence))
                dates.Add(date);

            return dates;
        }
    }
}
