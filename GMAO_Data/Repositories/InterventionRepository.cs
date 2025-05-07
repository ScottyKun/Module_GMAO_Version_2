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
    public class InterventionRepository
    {
        private readonly DbManager db;

        public InterventionRepository()
        {
            db = new DbManager();
        }

        public void Add(Intervention i) => db.Interventions.Add(i);

        public Intervention GetById(int id) =>
            db.Interventions
              .Include("MaintenancePlanifiee")
              .Include("PiecesReservees.Piece")
              .Include("WorkOrders")
              .FirstOrDefault(i => i.Id == id);

        public List<Intervention> GetByMaintenanceId(int maintenancePlanifieeId) =>
            db.Interventions
              .Where(i => i.MaintenancePlanifieeId == maintenancePlanifieeId)
              .ToList();

        public Intervention GetForDeletion(int id) =>
            db.Interventions
              .Include("PiecesReservees")
              .Include("WorkOrders")
              .Include("MaintenancePlanifiee")
              .FirstOrDefault(x => x.Id == id);

        public void RemovePieces(List<Intervention_Piece> pieces) =>
            db.Intervention_Pieces.RemoveRange(pieces);

        public void Remove(Intervention i) => db.Interventions.Remove(i);

        public void UpdateIntervention(Intervention i) =>
            db.Entry(i).State = EntityState.Modified;

        public void AddPieces(List<Intervention_Piece> pieces) =>
            db.Intervention_Pieces.AddRange(pieces);

        public PieceDeRechange GetPieceById(int id) =>
            db.PiecesDeRechanges.FirstOrDefault(x => x.pieceId == id);

        public List<PieceDeRechange> GetAllPieces() => db.PiecesDeRechanges.ToList();

        public List<Intervention> GetAll() => db.Interventions.ToList();

        public List<Intervention> GetAllWithResponsable(int responsableId) =>
            db.Interventions
              .Include("MaintenancePlanifiee")
              .Include("MaintenancePlanifiee.Equipement")
              .Include("MaintenancePlanifiee.Responsable")
              .Where(i => i.MaintenancePlanifiee.ResponsableId == responsableId)
              .ToList();

        public bool ExistsForDate(int maintenanceId, DateTime dateDebut, DateTime dateFin) =>
            db.Interventions.Any(i => i.MaintenancePlanifieeId == maintenanceId &&
                                       i.DatePrevue >= dateDebut &&
                                       i.DatePrevue < dateFin);

        public List<Intervention> GetInterventionsDisponiblesPourWO(int idResponsable) =>
            db.Interventions
              .Include("MaintenancePlanifiee")
              .Where(i => i.WorkOrders.Count == 0 &&
                          i.MaintenancePlanifiee.ResponsableId == idResponsable &&
                          i.MaintenancePlanifiee.Statut != "Terminee")
              .ToList();

        public void SaveChanges() => db.SaveChanges();
    }
}
