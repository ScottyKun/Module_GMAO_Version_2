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
   public class WorkOrderCoService
    {
        private readonly WorkOrderCoRepository _repository;

        public WorkOrderCoService()
        {
            _repository = new WorkOrderCoRepository();
        }

        public void CreerWorkOrderPourCorrective(int maintenanceCorrectiveId, string nom, DateTime dateExecution, string rapport)
        {
            var maintenance = _repository.GetMaintenanceCorrective(maintenanceCorrectiveId);
            if (maintenance == null)
                throw new InvalidOperationException("Maintenance corrective introuvable.");

            var workOrder = new WorkOrder
            {
                Nom = nom,
                DateExecution = dateExecution,
                Terminee = false,
                Cout = 0,
                Rapport = rapport,
                MaintenanceCorrective = maintenance
            };

            _repository.Add(workOrder);

            maintenance.Statut = "En cours";
            _repository.Save();
        }

        public void TerminerWorkOrder(int workOrderId, List<PieceUtilisationDTO> piecesUtilisees)
        {
            var workOrder = _repository.GetForTermination(workOrderId);
            if (workOrder == null)
                throw new InvalidOperationException("WorkOrder introuvable.");

            if (workOrder.Terminee)
                throw new InvalidOperationException("Ce WorkOrder est déjà terminé.");

            if (piecesUtilisees == null || !piecesUtilisees.Any())
                throw new InvalidOperationException("Aucune pièce utilisée. Impossible de terminer le WorkOrder.");

            _repository.RemoveUtilisations(_repository.GetUtilisationsByWorkOrderId(workOrderId));

            decimal total = 0;

            foreach (var usage in piecesUtilisees)
            {
                var piece = _repository.GetPiece(usage.PieceId);
                if (piece == null)
                    throw new InvalidOperationException($"Pièce {usage.PieceId} introuvable.");

                var reservee = workOrder.MaintenanceCorrective?.PiecesReservees?.FirstOrDefault(r => r.PieceId == usage.PieceId);
                if (reservee == null || usage.Quantite > reservee.Quantite)
                    throw new InvalidOperationException($"Quantité utilisée ({usage.Quantite}) > réservée ({reservee?.Quantite ?? 0}) pour \"{piece.nom}\".");

                if (piece.quantite < usage.Quantite)
                    throw new InvalidOperationException($"Stock insuffisant pour \"{piece.nom}\".");

                piece.quantite -= usage.Quantite;

                _repository.AddUtilisation(new WorkOrder_Piece
                {
                    WorkOrderId = workOrderId,
                    PieceId = usage.PieceId,
                    Quantite = usage.Quantite
                });

                total += usage.Quantite * piece.prix;
            }

            workOrder.Cout = total;
            workOrder.Terminee = true;
            workOrder.MaintenanceCorrective.Statut = "Terminée";

            _repository.Save();
        }

        public void ModifierWorkOrder(int workOrderId, string nom, DateTime dateExecution, string rapport, List<PieceUtilisationDTO> pieces)
        {
            var workOrder = _repository.GetForModification(workOrderId);
            if (workOrder == null || workOrder.Terminee)
                throw new InvalidOperationException("WorkOrder terminé ou inexistant.");

            workOrder.Nom = nom;
            workOrder.DateExecution = dateExecution;
            workOrder.Rapport = rapport;

            _repository.RemoveUtilisations(workOrder.PiecesUtilisees.ToList());

            decimal total = 0;

            foreach (var p in pieces)
            {
                var piece = _repository.GetPiece(p.PieceId);
                if (piece == null)
                    throw new InvalidOperationException($"Pièce {p.PieceId} introuvable.");

                var reservee = workOrder.MaintenanceCorrective?.PiecesReservees?.FirstOrDefault(r => r.PieceId == p.PieceId);
                if (reservee == null || p.Quantite > reservee.Quantite)
                    throw new InvalidOperationException($"Utilisation {p.Quantite} > réservée ({reservee?.Quantite ?? 0}) pour \"{piece.nom}\".");

                total += p.Quantite * piece.prix;

                _repository.AddUtilisation(new WorkOrder_Piece
                {
                    WorkOrderId = workOrderId,
                    PieceId = p.PieceId,
                    Quantite = p.Quantite
                });
            }

            workOrder.Cout = total;
            _repository.Save();
        }

        public void Supprimer(int workOrderId)
        {
            var workOrder = _repository.GetForSuppression(workOrderId);
            if (workOrder == null)
                return;

            if (workOrder.Terminee)
                throw new InvalidOperationException("Impossible de supprimer un WorkOrder terminé.");

            workOrder.MaintenanceCorrective.Statut = "Nouvelle";

            _repository.RemoveUtilisations(workOrder.PiecesUtilisees.ToList());
            _repository.Delete(workOrder);
            _repository.Save();
        }

        public void MarquerWorkOrderCommeImpossible(int workOrderId)
        {
            var workOrder = _repository.GetForTermination(workOrderId);
            if (workOrder == null)
                throw new InvalidOperationException("WorkOrder introuvable.");

            if (workOrder.Terminee)
                throw new InvalidOperationException("Déjà terminé.");

            var total = workOrder.PiecesUtilisees
                .Sum(p => (_repository.GetPiece(p.PieceId)?.prix ?? 0) * p.Quantite);

            workOrder.Terminee = true;
            workOrder.Cout = total;
            workOrder.Rapport = "Maintenance impossible à exécuter.";

            var maintenance = workOrder.MaintenanceCorrective;
            if (maintenance != null)
            {
                maintenance.Statut = "Echec";

                foreach (var reservation in maintenance.PiecesReservees)
                {
                    var piece = _repository.GetPiece(reservation.PieceId);
                    var utilisee = workOrder.PiecesUtilisees.FirstOrDefault(u => u.PieceId == reservation.PieceId);
                    var àRestituer = reservation.Quantite - (utilisee?.Quantite ?? 0);

                    if (àRestituer > 0 && piece != null)
                        piece.quantite += àRestituer;
                }

                var equipement = _repository.GetEquipement(maintenance.EquipementId);
                if (equipement != null)
                    equipement.statut = false;
            }

            _repository.Save();
        }

        public WorkOrderCoDTODetails GetById(int id)
        {
            var workOrder = _repository.GetById(id);
            if (workOrder == null) return null;

            var dto = new WorkOrderCoDTODetails
            {
                Id = workOrder.Id,
                Nom = workOrder.Nom,
                DateExecution = workOrder.DateExecution,
                Cout = workOrder.Cout,
                Terminee = workOrder.Terminee,
                MaintenanceId = workOrder.MaintenanceCorrective?.MaintenanceId ?? 0,
                MaintenanceDescription = workOrder.MaintenanceCorrective?.Description ?? "",
                PiecesReservees = new List<PieceReservationView>(),
                PiecesUtilisees = new List<PieceUtilisationView>()
            };

            if (workOrder.MaintenanceCorrective != null)
            {
                dto.PiecesReservees = workOrder.MaintenanceCorrective.PiecesReservees.Select(r => new PieceReservationView
                {
                    PieceId = r.PieceId,
                    Nom = r.Piece.nom,
                    QuantiteStock = r.Piece.quantite,
                    QuantiteAReserver = r.Quantite
                }).ToList();
            }

            dto.PiecesUtilisees = workOrder.PiecesUtilisees.Select(u => new PieceUtilisationView
            {
                PieceId = u.PieceId,
                Nom = u.Piece.nom,
                Quantite = u.Quantite
            }).ToList();

            return dto;
        }


        public List<WorkOrderDTO> GetAllByResponsable(int idUser)
        {
            return _repository.GetAllByResponsableId(idUser)
                    .Select(w => new WorkOrderDTO
                    {
                        Id = w.Id,
                        Nom = w.Nom,
                        DateExecution = w.DateExecution,
                        Terminee = w.Terminee,
                        Cout = w.Cout,
                        MaintenanceId = w.MaintenanceCorrective.MaintenanceId,
                        MaintenanceDescription = w.MaintenanceCorrective.Description
                    }).ToList();
        }

        public List<WorkOrderDTO> GetAllByUtilisateur()
        {
            if (UserContext.Role == "Responsable")
            {
                return GetAllByResponsable(UserContext.IdUser);
            }

            var responsables = new UserService().GetResponsablesPourTechnicien(UserContext.IdUser);
            return _repository.GetAllByResponsables(responsables)
                    .Select(w => new WorkOrderDTO
                    {
                        Id = w.Id,
                        Nom = w.Nom,
                        DateExecution = w.DateExecution,
                        Terminee = w.Terminee,
                        Cout = w.Cout,
                        MaintenanceId = w.MaintenanceCorrective.MaintenanceId,
                        MaintenanceDescription = w.MaintenanceCorrective.Description
                    }).ToList();
        }

        public bool ExisteWorkOrderPourMaintenance(int maintenanceId)
        {
            return _repository.ExistePourMaintenance(maintenanceId);
        }

        public WorkOrderDTO GetById2(int id)
        {
            var w = _repository.GetById(id);
            if (w == null) return null;

            var dto = new WorkOrderDTO
            {
                Id = w.Id,
                Nom = w.Nom,
                DateExecution = w.DateExecution,
                Terminee = w.Terminee,
                Cout = w.Cout,
                MaintenanceId = w.MaintenanceCorrective.MaintenanceId,
                MaintenanceDescription = w.MaintenanceCorrective.Description
            };

            return dto;
        }
    }
}
