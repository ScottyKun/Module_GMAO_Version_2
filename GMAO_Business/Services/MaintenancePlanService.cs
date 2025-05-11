using GMAO_Business.DTOs;
using GMAO_Business.Entities;
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

        public void Creer(MaintenancePlanifieeDTO2 dto)
        {
            if (dto.DateDebut >= dto.DateFin)
                throw new ArgumentException("La date de début doit être antérieure à la date de fin.");

            var maintenance = new MaintenancePlanifiee
            {
                Description = dto.Description,
                Statut = "Nouvelle",
                DateDebut = dto.DateDebut,
                DateFin = dto.DateFin,
                RecurrenceJours = dto.RecurrenceJours,
                ResponsableId = dto.ResponsableId,
                EquipementId = dto.EquipementId,
                EquipeId = dto.EquipeId,
                NbInterventions = CalculerNbInterventions(dto.DateDebut, dto.DateFin, dto.RecurrenceJours),
                NbInterventionsFinish = 0,
                CoutReel = 0,
                CoutPrevu = 0
        };

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

        public MaintenancePlanifieeDTO2 GetById(int id)
        {
            var maintenance = _repository.GetById(id);
            if (maintenance == null)
                return null;

            return new MaintenancePlanifieeDTO2
            {
                MaintenanceId = maintenance.MaintenanceId,
                Description = maintenance.Description,
                Statut = maintenance.Statut,
                DateDebut = maintenance.DateDebut,
                DateFin = maintenance.DateFin,
                RecurrenceJours = maintenance.RecurrenceJours,
                ResponsableId = maintenance.ResponsableId,
                EquipementId = maintenance.EquipementId,
                EquipeId = maintenance.EquipeId
            };
        }

        public MaintenancePlanifieeDTO GetById2(int id)
        {
            var m = _repository.GetById(id);
            if (m == null)
                return null;

            return new MaintenancePlanifieeDTO
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
            };
        }


        public void RecalculerCouts(int maintenanceId)
        {
            _repository.RecalculerCouts(maintenanceId);
        }

        public void Modifier(MaintenancePlanifieeDTO2 dto)
        {

            if (dto.DateDebut >= dto.DateFin)
                throw new ArgumentException("La date de début doit être antérieure à la date de fin.");
            var modif = new MaintenancePlanifiee
            {
                MaintenanceId = dto.MaintenanceId,
                Description = dto.Description,
                DateDebut = dto.DateDebut,
                DateFin = dto.DateFin,
                RecurrenceJours = dto.RecurrenceJours,
                NbInterventions = CalculerNbInterventions(dto.DateDebut, dto.DateFin, dto.RecurrenceJours)
            };

            _repository.Update(modif);
        }
       
        public void Supprimer(int maintenanceId)
        {
            _repository.Delete(maintenanceId);
        }

        public List<MaintenanceLightDTO> GetMaintenancesDisponiblesPourIntervention()
        {
            var maintenances = _repository.GetAllDisponiblesPourIntervention(UserContext.IdUser, UserContext.Role);

            return maintenances.Select(m => new MaintenanceLightDTO
            {
                MaintenanceId = m.MaintenanceId,
                Description = m.Description
            }).ToList();
        }


        public void ModifierStatut(int maintenanceId)
        {
            _repository.ModifierStatut(maintenanceId, "En cours");
        }
    }
}
