using GMAO_Business.DTOs;
using GMAO_Data.Entities;
using GMAO_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.Services
{
   public class MaintenancePlanService
    {
        private readonly MaintenancePlanifieeRepository _repository;
        private readonly MaintenanceService _maintenanceService;

        public MaintenancePlanService()
        {
            _repository = new MaintenancePlanifieeRepository();
            _maintenanceService = new MaintenanceService(); // Toujours logique métier
        }

        public void Creer(MaintenancePlanifiee maintenance)
        {
            if (!_maintenanceService.PeutCreerMaintenance(maintenance.EquipementId, maintenance.DateDebut, maintenance.DateFin))
                throw new InvalidOperationException("Une maintenance existe déjà sur cet équipement durant la période indiquée.");

            maintenance.Statut = "Nouvelle";
            maintenance.NbInterventions = CalculerNbInterventions(maintenance.DateDebut, maintenance.DateFin, maintenance.RecurrenceJours);
            maintenance.NbInterventionsFinish = 0;
            maintenance.CoutReel = 0;
            maintenance.CoutPrevu = 0;

            _repository.Add(maintenance);
        }

        public int CalculerNbInterventions(DateTime debut, DateTime fin, int recurrence)
        {
            if (recurrence <= 0 || debut > fin)
                return 0;

            int count = 0;
            for (var date = debut.Date; date <= fin.Date; date = date.AddDays(recurrence))
            {
                count++;
            }

            return count;
        }

        public List<MaintenancePlanifieeDTO> GetAllDTOByResponsable(int idResponsable)
        {
            var maintenances = _repository.GetAllByResponsable(idResponsable);

            return maintenances.Select(m => new MaintenancePlanifieeDTO
            {
                MaintenanceId = m.MaintenanceId,
                Description = m.Description,
                Statut = m.Statut,
                DateDebut = m.DateDebut,
                DateFin = m.DateFin,
                RecurrenceJours = m.RecurrenceJours,
                NbInterventions = m.NbInterventions,
                NbInterventionsFinish = m.NbInterventionsFinish,
                CoutPrevu = m.CoutPrevu,
                CoutReel = m.CoutReel,
                EquipementNom = m.Equipement?.nom,
                ResponsableNom = m.Responsable?.nom
            }).ToList();
        }

        public MaintenancePlanifiee GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void RecalculerCouts(int maintenanceId)
        {
            _repository.RecalculerCouts(maintenanceId);
        }

        public void Modifier(MaintenancePlanifiee modif)
        {
            modif.NbInterventions = CalculerNbInterventions(modif.DateDebut, modif.DateFin, modif.RecurrenceJours);
            _repository.Update(modif);
        }

        public void Supprimer(int maintenanceId)
        {
            _repository.Delete(maintenanceId);
        }

        public List<MaintenanceLightDTO> GetMaintenancesDisponiblesPourIntervention()
        {
            var maintenances = _repository.GetAllDisponiblesPourIntervention();
            return maintenances.Select(m => new MaintenanceLightDTO
            {
                MaintenanceId = m.MaintenanceId,
                Description = m.Description
            }).ToList();
        }
    }
}
