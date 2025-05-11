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
    public class WorkOrderPlanRepository
    {
        private readonly DbManager db;

        public WorkOrderPlanRepository()
        {
            db = new DbManager();
        }

        public void Add(WorkOrder wo)
        {
            db.WorkOrders.Add(wo);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }

        public void Remove(WorkOrder wo)
        {
            db.WorkOrders.Remove(wo);
          
        }

        public WorkOrder GetById(int id)
        {
            return db.WorkOrders
                     .Include("PiecesUtilisees")
                     .Include("Intervention.PiecesReservees")
                     .Include("Intervention.MaintenancePlanifiee.Equipement")
                     .FirstOrDefault(w => w.Id == id);
        }

        public WorkOrder GetById3(int id)
        {
            return db.WorkOrders
                     .Include("PiecesUtilisees")
                      .Include("Intervention.MaintenancePlanifiee.Interventions")
                     .FirstOrDefault(w => w.Id == id);
        }

        public void RestaurerEtatInterventionEtMaintenanceAprèsSuppression(WorkOrder wo)
        {
            var intervention = wo.Intervention;
            if (intervention != null)
            {
                intervention.Etat = "New";
                db.Entry(intervention).State = EntityState.Modified;

                var maintenance = intervention.MaintenancePlanifiee;
                if (maintenance != null && maintenance.Interventions.All(i => i.Etat == "New"))
                {
                    maintenance.Statut = "Nouvelle";
                    db.Entry(maintenance).State = EntityState.Modified;
                }
            }
        }



        public WorkOrder GetById2(int id)
        {
            return db.WorkOrders
                     .Include("PiecesUtilisees.Piece")
                     .Include("Intervention.PiecesReservees.Piece")
                     .Include("Intervention.MaintenancePlanifiee.Equipement")
                     .FirstOrDefault(w => w.Id == id);
        }


        public List<WorkOrder> GetByResponsableId(int idResponsable)
        {
            return db.WorkOrders
                     .Include("Intervention.MaintenancePlanifiee.Equipement")
                     .Include("Intervention.MaintenancePlanifiee.Responsable")
                     .Where(wo => wo.Intervention.MaintenancePlanifiee.ResponsableId == idResponsable)
                     .ToList();
        }

        public List<WorkOrder> GetAllWithEquipement()
        {
            return db.WorkOrders
                     .Include("Intervention.MaintenancePlanifiee.Equipement")
                     .ToList();
        }

        public void RemovePiecesUtilisees(WorkOrder wo)
        {
            db.WorkOrder_Pieces.RemoveRange(wo.PiecesUtilisees);
        }

        public void AddPieceUtilisee(WorkOrder_Piece wp)
        {
            db.WorkOrder_Pieces.Add(wp);
        }

        public List<WorkOrder> GetByResponsables(List<int> responsableIds)
        {
            return db.WorkOrders
                     .Include("Intervention.MaintenancePlanifiee.Equipement")
                     .Where(wo => responsableIds.Contains(wo.Intervention.MaintenancePlanifiee.ResponsableId))
                     .ToList();
        }

        public PieceDeRechange GetPieceStock(int pieceId)
        {
            return db.PiecesDeRechanges.FirstOrDefault(p => p.pieceId == pieceId);
        }

        public Intervention GetInterventionById(int id)
        {
            return db.Interventions
                     .Include("PiecesReservees.Piece")
                     .Include("MaintenancePlanifiee.Equipement")
                     .FirstOrDefault(i => i.Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
