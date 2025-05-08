using GMAO_Business.DTOs;
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
    public class InterventionService
    {
        private readonly InterventionRepository _repository;
        private readonly MaintenancePlanService _maintenancePlanService;
        private readonly UserService _userService;

        public InterventionService()
        {
            _repository = new InterventionRepository();
            _maintenancePlanService = new MaintenancePlanService();
            _userService = new UserService();
        }

        public void Creer(InterventionCreationDTO dto)
        {
            var i = new Intervention
            {
                Nom = dto.Nom,
                DatePrevue = dto.DatePrevue,
                MaintenancePlanifieeId = dto.MaintenancePlanifieeId,
                Etat = dto.Etat
            };

            _repository.Add(i);

            var maintenance = _maintenancePlanService.GetById(i.MaintenancePlanifieeId);
            if (maintenance != null)
            {
                maintenance.Statut = "En cours";
                _maintenancePlanService.Modifier(maintenance);
            }
        }

        public InterventionDTO2 GetById(int id)
        {
            var intervention = _repository.GetById(id);
            if (intervention == null)
                return null;

            return new InterventionDTO2
            {
                Id = intervention.Id,
                Nom = intervention.Nom,
                Etat = intervention.Etat,
                DatePrevue = intervention.DatePrevue,
                DescriptionMaintenance = intervention.MaintenancePlanifiee.Description,
                EquipementNom = intervention.MaintenancePlanifiee.Equipement.nom,
                EquipementId = intervention.MaintenancePlanifiee.EquipementId,
                ResponsableId = intervention.MaintenancePlanifiee.ResponsableId,
                PiecesReservees = intervention.PiecesReservees.Select(pr => new PieceReservationDTO
                {
                    PieceId = pr.PieceId,
                    Quantite = pr.Quantite
                }).ToList()
            };
        }


        public List<InterventionLightDTO2> GetByMaintenanceId(int maintenancePlanifieeId)
        {
            var interventions = _repository.GetByMaintenanceId(maintenancePlanifieeId);

            return interventions.Select(i => new InterventionLightDTO2
            {
                Id = i.Id,
                DatePrevue = i.DatePrevue
            }).ToList();
        }


        public void Supprimer(int id)
        {
            var i = _repository.GetById(id);
            if (i == null) return;

            if (i.WorkOrders.Any(wo => wo.Terminee))
                throw new InvalidOperationException("Impossible de supprimer une intervention avec des WorkOrders terminés.");

            var maintenance = i.MaintenancePlanifiee;

            i.PiecesReservees.Clear();
            _repository.Delete(i);

            if (!maintenance.Interventions.Any())
            {
                maintenance.Statut = "Nouvelle";
                maintenance.CoutPrevu = 0;
                maintenance.CoutReel = 0;
                maintenance.NbInterventionsFinish = 0;
            }
            else
            {
                _maintenancePlanService.RecalculerCouts(maintenance.MaintenanceId);
            }
        }

        public void ModifierComplet(InterventionModificationDTO dto)
        {
            var intervention = _repository.GetById(dto.Id);
            if (intervention == null || intervention.Etat != "New")
                throw new InvalidOperationException("Impossible de modifier une intervention en cours.");

            intervention.Nom = dto.Nom;
            intervention.DatePrevue = dto.DatePrevue;

            var nouvellesEntites = dto.PiecesReservees.Select(p => new Intervention_Piece
            {
                InterventionId = intervention.Id,
                PieceId = p.PieceId,
                Quantite = p.Quantite
            }).ToList();

            decimal totalPrevu = _repository.PreparerPiecesEtCalculerCout(nouvellesEntites);

            _repository.RemoveReservedPieces(intervention.PiecesReservees.ToList());
            _repository.AddReservedPieces(nouvellesEntites);
            intervention.Cout = totalPrevu;

            _repository.Update(intervention);
            _maintenancePlanService.RecalculerCouts(intervention.MaintenancePlanifieeId);
        }



        public void RecalculerCout(int interventionId)
        {
            var i = _repository.GetById(interventionId);
            if (i == null) return;

            decimal coutPrevu = i.PiecesReservees.Sum(p => p.Quantite * p.Piece.prix);
            decimal coutReel = i.WorkOrders.Where(w => w.Terminee).Sum(w => w.Cout);

            i.Cout = coutPrevu;
            _repository.Update(i);
            _maintenancePlanService.RecalculerCouts(i.MaintenancePlanifieeId);
        }

        public List<InterventionDTO> GetAllDTOByResponsable(int idResponsable)
        {
            var list = _repository.GetAllByResponsable(idResponsable);

            return list.Select(i => new InterventionDTO
            {
                Id = i.Id,
                Nom = i.Nom,
                Etat = i.Etat,
                DatePrevue = i.DatePrevue,
                Cout = i.Cout,
                MaintenanceId = i.MaintenancePlanifieeId,
                MaintenanceDescription = i.MaintenancePlanifiee.Description,
                EquipementNom = i.MaintenancePlanifiee.Equipement.nom
            }).ToList();
        }

        public bool ExisteInterventionPour(int maintenanceId, DateTime date)
        {
            return _repository.ExisteInterventionPour(maintenanceId, date);
        }

        public List<InterventionLightDTO> GetInterventionsDisponiblesPourWO(int idResponsable)
        {
            var list = _repository.GetDisponiblesPourWO(idResponsable);

            return list.Select(i => new InterventionLightDTO
            {
                Id = i.Id,
                Description = i.Nom
            }).ToList();
        }

        public List<InterventionDTO> GetAllPourUtilisateur()
        {
            List<Intervention> list;

            if (UserContext.Role == "Responsable")
            {
                list = _repository.GetAllByResponsable(UserContext.IdUser);
            }
            else
            {
                var responsables = _userService.GetResponsablesPourTechnicien(UserContext.IdUser);
                var all = _repository.GetAll();
                list = all.Where(i => responsables.Contains(i.MaintenancePlanifiee.ResponsableId)).ToList();
            }

            return list.Select(i => new InterventionDTO
            {
                Id = i.Id,
                Nom = i.Nom,
                Etat = i.Etat,
                DatePrevue = i.DatePrevue,
                Cout = i.Cout,
                MaintenanceId = i.MaintenancePlanifieeId,
                MaintenanceDescription = i.MaintenancePlanifiee.Description,
                EquipementNom = i.MaintenancePlanifiee.Equipement.nom
            }).ToList();
        }
    }

}