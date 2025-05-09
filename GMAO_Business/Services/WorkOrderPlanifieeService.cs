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
    public class WorkOrderPlanifieeService
    {
        private readonly WorkOrderPlanRepository _repository;
        private readonly InterventionService _interventionService;
        private readonly MaintenancePlanService _maintenancePlanService;

        public WorkOrderPlanifieeService()
        {
            _repository = new WorkOrderPlanRepository();
            _interventionService = new InterventionService();
            _maintenancePlanService = new MaintenancePlanService();
        }

        public void Creer(int interventionId, string nom, DateTime dateExecution, string rapport)
        {
            var wo = new WorkOrder
            {
                Nom = nom,
                DateExecution = dateExecution,
                Rapport = rapport,
                Terminee = false,
                Cout = 0,
                InterventionId = interventionId
            };

            _repository.Add(wo);

            var intervention = _repository.GetInterventionById(interventionId);
            if (intervention != null)
            {
                intervention.Etat = "Pending";
                _repository.Save();
            }
        }

        public void Modifier(int id, string nom, DateTime dateExecution, string rapport, List<PieceUtilisationDTO> pieces)
        {
            var wo = _repository.GetById(id);
            if (wo == null || wo.Terminee)
                throw new InvalidOperationException("Impossible de modifier ce WorkOrder.");

            wo.Nom = nom;
            wo.DateExecution = dateExecution;
            wo.Rapport = rapport;

            _repository.RemovePiecesUtilisees(wo);

            decimal total = 0;
            foreach (var p in pieces)
            {
                var stock = _repository.GetPieceStock(p.PieceId);
                var reservee = wo.Intervention?.PiecesReservees?.FirstOrDefault(r => r.PieceId == p.PieceId);

                if (stock == null || reservee == null || p.Quantite > reservee.Quantite)
                    throw new InvalidOperationException($"Erreur avec la pièce {p.PieceId}");

                total += stock.prix * p.Quantite;

                _repository.AddPieceUtilisee(new WorkOrder_Piece
                {
                    WorkOrderId = wo.Id,
                    PieceId = p.PieceId,
                    Quantite = p.Quantite
                });
            }

            wo.Cout = total;
            _repository.Save();

            _interventionService.RecalculerCout(wo.InterventionId.Value);
        }

        public void Terminer(int id, List<PieceUtilisationDTO> pieces)
        {
            var wo = _repository.GetById(id);

            if (wo == null || wo.Terminee)
                throw new InvalidOperationException("Déjà terminé");

            if (pieces == null || !pieces.Any())
                throw new InvalidOperationException("Aucune pièce utilisée");

            _repository.RemovePiecesUtilisees(wo);

            decimal total = 0;
            foreach (var p in pieces)
            {
                var piece = _repository.GetPieceStock(p.PieceId);
                if (piece == null || piece.quantite < p.Quantite)
                    throw new InvalidOperationException($"Stock insuffisant pour {piece?.nom}");

                var reservee = wo.Intervention.PiecesReservees.FirstOrDefault(r => r.PieceId == p.PieceId);
                if (reservee == null || reservee.Quantite < p.Quantite)
                    throw new InvalidOperationException($"Quantité dépassée pour {piece.nom}");

                piece.quantite -= p.Quantite;

                _repository.AddPieceUtilisee(new WorkOrder_Piece
                {
                    WorkOrderId = id,
                    PieceId = p.PieceId,
                    Quantite = p.Quantite
                });

                total += p.Quantite * piece.prix;
            }

            wo.Cout = total;
            wo.Terminee = true;
            wo.Intervention.Etat = "Terminee";

            _repository.Save();
            _interventionService.RecalculerCout(wo.InterventionId.Value);
        }

        public void Supprimer(int id)
        {
            var wo = _repository.GetById(id);
            if (wo == null || wo.Terminee)
                throw new InvalidOperationException("Impossible de supprimer un WO terminé.");

            _repository.RemovePiecesUtilisees(wo);
            _repository.Remove(wo);

            var intervention = wo.Intervention;
            if (intervention != null)
            {
                intervention.Etat = "New";

                var maintenance = intervention.MaintenancePlanifiee;
                if (maintenance != null && maintenance.Interventions.All(i => i.Etat == "New"))
                {
                    maintenance.Statut = "Nouvelle";
                }
            }

            _repository.Save();

            var idMaintenance = wo.Intervention?.MaintenancePlanifieeId;
            if (idMaintenance != null)
            {
                _maintenancePlanService.RecalculerCouts(idMaintenance.Value);
            }
        }

        public List<WorkOrderDTO2> GetAllDTOByResponsable(int idResponsable)
        {
            var result = _repository.GetByResponsableId(idResponsable);

            return result.Select(wo => new WorkOrderDTO2
            {
                Id = wo.Id,
                Nom = wo.Nom,
                DateExecution = wo.DateExecution,
                Terminee = wo.Terminee,
                Rapport = wo.Rapport,
                Cout = wo.Cout,
                InterventionId = wo.InterventionId ?? 0,
                DescriptionIntervention = wo.Intervention?.Nom,
                EquipementNom = wo.Intervention?.MaintenancePlanifiee?.Equipement?.nom
            }).ToList();
        }

        public WorkOrderDetailsDTO GetById(int id)
        {
            var wo = _repository.GetById2(id);
            if (wo == null) return null;

            return new WorkOrderDetailsDTO
            {
                Id = wo.Id,
                Nom = wo.Nom,
                DateExecution = wo.DateExecution,
                Terminee = wo.Terminee,
                Rapport = wo.Rapport,
                InterventionId = wo.Intervention?.Id ?? 0,
                DescriptionIntervention = wo.Intervention?.Nom ?? "-",
                EquipementNom = wo.Intervention?.MaintenancePlanifiee?.Equipement?.nom ?? "-",
                PiecesReservees = wo.Intervention?.PiecesReservees.Select(p => new PieceReservationView
                {
                    PieceId = p.PieceId,
                    Nom = p.Piece.nom,
                    QuantiteStock = p.Piece.quantite,
                    QuantiteAReserver = p.Quantite
                }).ToList() ?? new List<PieceReservationView>(),

                PiecesUtilisees = wo.PiecesUtilisees.Select(p => new PieceUtilisationView
                {
                    PieceId = p.PieceId,
                    Nom = p.Piece.nom,
                    Quantite = p.Quantite
                }).ToList()
            };
        }


        public void MarquerCommeImpossible(int id)
        {
            var wo = _repository.GetById(id);
            if (wo == null || wo.Terminee)
                throw new InvalidOperationException("WorkOrder introuvable ou déjà terminé.");

            if (!wo.PiecesUtilisees.Any())
                throw new InvalidOperationException("Impossible : aucune pièce utilisée dans ce WorkOrder.");

            wo.Terminee = true;
            wo.Intervention.Etat = "Echec";
            wo.Rapport = "Impossible";

            var utilisees = wo.PiecesUtilisees.ToList();
            var reservees = wo.Intervention.PiecesReservees.ToList();

            foreach (var res in reservees)
            {
                var piece = res.Piece;
                var quantiteUtilisee = utilisees.FirstOrDefault(u => u.PieceId == res.PieceId)?.Quantite ?? 0;
                var quantiteARestituer = res.Quantite - quantiteUtilisee;

                if (quantiteARestituer > 0)
                {
                    piece.quantite += quantiteARestituer;
                }
            }

            wo.Cout = utilisees.Sum(u => u.Quantite * u.Piece.prix);

            var maintenance = wo.Intervention.MaintenancePlanifiee;
            if (maintenance != null)
            {
                maintenance.Statut = "Echec";
                if (maintenance.Equipement != null)
                    maintenance.Equipement.statut = false;
            }

            _repository.Save();
            _interventionService.RecalculerCout(wo.InterventionId.Value);
        }

        public List<WorkOrderDTO2> GetAllDTOByUtilisateur()
        {
            if (UserContext.Role == "Responsable")
            {
                return GetAllDTOByResponsable(UserContext.IdUser);
            }
            else
            {
                var responsables = new UserService().GetResponsablesPourTechnicien(UserContext.IdUser);
                var workOrders = _repository.GetByResponsables(responsables);

                return workOrders.Select(wo => new WorkOrderDTO2
                {
                    Id = wo.Id,
                    Nom = wo.Nom,
                    DateExecution = wo.DateExecution,
                    Terminee = wo.Terminee,
                    Rapport = wo.Rapport,
                    Cout = wo.Cout,
                    InterventionId = wo.InterventionId ?? 0,
                    DescriptionIntervention = wo.Intervention?.Nom,
                    EquipementNom = wo.Intervention?.MaintenancePlanifiee?.Equipement?.nom
                }).ToList();
            }
        }
    }
}
