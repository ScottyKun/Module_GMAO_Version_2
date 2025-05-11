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
        private readonly DbManager db;

        public WorkOrderCoRepository()
        {
            db = new DbManager();
        }

        // WorkOrders
        public WorkOrder GetById(int id)
        {
            return db.WorkOrders
                     .Include(w => w.MaintenanceCorrective.PiecesReservees.Select(r => r.Piece))
                     .Include(w => w.PiecesUtilisees.Select(u => u.Piece))
                     .FirstOrDefault(w => w.Id == id);
        }



        public WorkOrder GetForTermination(int id)
        {
            return db.WorkOrders
                     .Include("MaintenanceCorrective.PiecesReservees")
                     .FirstOrDefault(w => w.Id == id);
        }

        public WorkOrder GetForModification(int id)
        {
            return db.WorkOrders
                     .Include("PiecesUtilisees")
                     .Include("MaintenanceCorrective.PiecesReservees")
                     .FirstOrDefault(w => w.Id == id);
        }

        public void Add(WorkOrder workOrder)
        {
            db.WorkOrders.Add(workOrder);
        }

        public void Delete(WorkOrder workOrder)
        {
            db.WorkOrders.Remove(workOrder);
        }

        public List<WorkOrder> GetAllByResponsableId(int responsableId)
        {
            return db.WorkOrders
                     .Where(w => w.MaintenanceCorrective.ResponsableId == responsableId)
                     .ToList();
        }

        public List<WorkOrder> GetAllByResponsables(List<int> responsables)
        {
            return db.WorkOrders
                     .Where(w => responsables.Contains(w.MaintenanceCorrective.ResponsableId))
                     .ToList();
        }

        public bool ExistePourMaintenance(int maintenanceId)
        {
            return db.WorkOrders.Any(w => w.MaintenanceCorrective != null && w.MaintenanceCorrective.MaintenanceId == maintenanceId);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public WorkOrder GetForSuppression(int id)
        {
            return db.WorkOrders
                     .Include("PiecesUtilisees")
                     .Include("MaintenanceCorrective")
                     .FirstOrDefault(w => w.Id == id);
        }

        public List<WorkOrder_Piece> GetUtilisationsByWorkOrderId(int id)
        {
            return db.WorkOrder_Pieces.Where(p => p.WorkOrderId == id).ToList();
        }

        public void RemoveUtilisations(List<WorkOrder_Piece> utilisations)
        {
            db.WorkOrder_Pieces.RemoveRange(utilisations);
        }

        public void AddUtilisation(WorkOrder_Piece utilisation)
        {
            db.WorkOrder_Pieces.Add(utilisation);
        }

        public PieceDeRechange GetPiece(int pieceId)
        {
            return db.PiecesDeRechanges.FirstOrDefault(p => p.pieceId == pieceId);
        }

        public MaintenanceCorrective GetMaintenanceCorrective(int maintenanceId)
        {
            return db.MaintenancesCorrectives.FirstOrDefault(m => m.MaintenanceId == maintenanceId);
        }

        public Equipement GetEquipement(int id)
        {
            return db.Equipements.FirstOrDefault(e => e.id == id);
        }
    }
}
