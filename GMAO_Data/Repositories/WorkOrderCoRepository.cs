using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class WorkOrderCoRepository
    {
        private readonly DbManager _context;

        public WorkOrderCoRepository()
        {
            _context = new DbManager();
        }

        // WorkOrders
        public void AddWorkOrder(WorkOrder workOrder) => _context.WorkOrders.Add(workOrder);

        public WorkOrder GetWorkOrderById(int id)
        {
            var workOrder = _context.WorkOrders
                .Include(w => w.MaintenanceCorrective)
                .FirstOrDefault(w => w.Id == id);

            // Chargement manuel des entités liées (enfants de collections)
            if (workOrder != null)
            {
                // Chargement manuel des pièces utilisées
                workOrder.PiecesUtilisees = _context.WorkOrder_Pieces
                    .Where(p => p.WorkOrderId == workOrder.Id)
                    .ToList();

                // Pour chaque pièce utilisée, charger la pièce liée
                foreach (var pu in workOrder.PiecesUtilisees)
                {
                    pu.Piece = _context.PiecesDeRechanges.Find(pu.PieceId);
                }
            }

            return workOrder;
        }


        public List<WorkOrder> GetWorkOrdersByResponsable(int responsableId)
        {
            return _context.WorkOrders
                .Where(w => w.MaintenanceCorrective.ResponsableId == responsableId)
                .Include(w => w.MaintenanceCorrective)
                .ToList();
        }

        public List<WorkOrder> GetWorkOrdersByResponsables(List<int> responsablesIds)
        {
            return _context.WorkOrders
                .Where(w => responsablesIds.Contains(w.MaintenanceCorrective.ResponsableId))
                .Include(w => w.MaintenanceCorrective)
                .ToList();
        }


        public bool WorkOrderExistsForMaintenance(int maintenanceId)
        {
            return _context.WorkOrders
                .Any(w => w.MaintenanceCorrective.MaintenanceId == maintenanceId);
        }

        public void RemoveWorkOrder(WorkOrder workOrder) => _context.WorkOrders.Remove(workOrder);

        public List<WorkOrder> GetAllWorkOrders() => _context.WorkOrders.ToList();

        // Pieces utilisées
        public List<WorkOrder_Piece> GetPiecesForWorkOrder(int workOrderId)
        {
            return _context.WorkOrder_Pieces
                .Where(p => p.WorkOrderId == workOrderId)
                .ToList();
        }

        public void AddWorkOrderPiece(WorkOrder_Piece piece) => _context.WorkOrder_Pieces.Add(piece);

        public void RemoveWorkOrderPieces(List<WorkOrder_Piece> pieces) => _context.WorkOrder_Pieces.RemoveRange(pieces);

        // MaintenanceCorrective
        public MaintenanceCorrective GetMaintenanceCorrectiveById(int id)
        {
            return _context.MaintenancesCorrectives
                .Include(m => m.Equipement)
                .FirstOrDefault(m => m.MaintenanceId == id);
        }

        // Equipement
        public void UpdateEquipement(Equipement equipement) => _context.Entry(equipement).State = EntityState.Modified;

        // Pièce
        public PieceDeRechange GetPieceById(int id) => _context.PiecesDeRechanges.Find(id);

        public void UpdatePiece(PieceDeRechange piece) => _context.Entry(piece).State = EntityState.Modified;

        public void SaveChanges() => _context.SaveChanges();
    }
}
