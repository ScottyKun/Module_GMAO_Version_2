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
   public class WorkOrderCoService
    {
        private readonly WorkOrderCoRepository _repository;
        private readonly UserService _userService;

        public WorkOrderCoService()
        {
            _repository = new WorkOrderCoRepository();
            _userService = new UserService();
        }

        public void AjouterWorkOrder(WorkOrder workOrder, List<WorkOrder_Piece> piecesUtilisees)
        {
            _repository.AddWorkOrder(workOrder);
            foreach (var pieceUtilisee in piecesUtilisees)
            {
                var piece = _repository.GetPieceById(pieceUtilisee.PieceId);
                if (piece != null)
                {
                    piece.quantite -= pieceUtilisee.Quantite;
                    _repository.UpdatePiece(piece);
                }
                _repository.AddWorkOrderPiece(pieceUtilisee);
            }
            _repository.SaveChanges();
        }

        public void SupprimerWorkOrder(int id)
        {
            var workOrder = _repository.GetWorkOrderById(id);
            if (workOrder == null) return;

            var pieces = _repository.GetPiecesForWorkOrder(id);
            foreach (var pieceUtilisee in pieces)
            {
                var piece = _repository.GetPieceById(pieceUtilisee.PieceId);
                if (piece != null)
                {
                    piece.quantite += pieceUtilisee.Quantite;
                    _repository.UpdatePiece(piece);
                }
            }

            _repository.RemoveWorkOrderPieces(pieces);
            _repository.RemoveWorkOrder(workOrder);
            _repository.SaveChanges();
        }

        public void ModifierWorkOrder(WorkOrder updatedWorkOrder, List<WorkOrder_Piece> nouvellesPieces)
        {
            var ancienWorkOrder = _repository.GetWorkOrderById(updatedWorkOrder.Id);
            if (ancienWorkOrder == null) return;

            var anciennesPieces = _repository.GetPiecesForWorkOrder(updatedWorkOrder.Id);
            foreach (var ancienne in anciennesPieces)
            {
                var piece = _repository.GetPieceById(ancienne.PieceId);
                if (piece != null)
                {
                    piece.quantite += ancienne.Quantite;
                    _repository.UpdatePiece(piece);
                }
            }

            _repository.RemoveWorkOrderPieces(anciennesPieces);

            foreach (var nouvelle in nouvellesPieces)
            {
                var piece = _repository.GetPieceById(nouvelle.PieceId);
                if (piece != null)
                {
                    piece.quantite -= nouvelle.Quantite;
                    _repository.UpdatePiece(piece);
                }
                _repository.AddWorkOrderPiece(nouvelle);
            }

            ancienWorkOrder.Nom = updatedWorkOrder.Nom;
            ancienWorkOrder.DateExecution = updatedWorkOrder.DateExecution;
            ancienWorkOrder.Terminee = updatedWorkOrder.Terminee;
            ancienWorkOrder.Cout = updatedWorkOrder.Cout;

            _repository.SaveChanges();
        }

        public List<WorkOrderDTO> GetAll()
        {
            return _repository.GetAllWorkOrders()
                .Select(w => new WorkOrderDTO
                {
                    Id = w.Id,
                    Nom = w.Nom,
                    DateExecution = w.DateExecution,
                    Terminee = w.Terminee,
                    Cout = w.Cout,
                    MaintenanceId = w.MaintenanceCorrective?.MaintenanceId ?? 0,
                    MaintenanceDescription = w.MaintenanceCorrective?.Description
                }).ToList();
        }

        public List<WorkOrderDTO> GetAllByUtilisateur()
        {
            if (UserContext.Role == "Responsable")
            {
                var list = _repository.GetWorkOrdersByResponsable(UserContext.IdUser);
                return list.Select(w => new WorkOrderDTO
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
            else
            {
                var responsables = _userService.GetResponsablesPourTechnicien(UserContext.IdUser);
                var list = _repository.GetWorkOrdersByResponsables(responsables);
                return list.Select(w => new WorkOrderDTO
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
        }

        public WorkOrder GetWorkOrderById(int id)
        {
            return _repository.GetWorkOrderById(id);
        }

        public MaintenanceCorrective GetMaintenanceCorrectiveById(int id)
        {
            return _repository.GetMaintenanceCorrectiveById(id);
        }

        public bool WorkOrderExistsForMaintenance(int maintenanceId)
        {
            return _repository.WorkOrderExistsForMaintenance(maintenanceId);
        }
    }
}
