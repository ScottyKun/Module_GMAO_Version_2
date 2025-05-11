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

        public void Add(Intervention i)
        {
            db.Interventions.Add(i);
            db.SaveChanges();
        }

        public void Update(Intervention i)
        {
            db.Entry(i).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Intervention i)
        {
            db.Interventions.Remove(i);
            db.SaveChanges();
        }

        public Intervention GetById(int id)
        {
            return db.Interventions
                     .Include("MaintenancePlanifiee")
                     .Include("PiecesReservees")
                     .Include("WorkOrders")
                     .FirstOrDefault(i => i.Id == id);
        }

        public List<Intervention> GetByMaintenanceId(int maintenancePlanifieeId)
        {
            return db.Interventions
                     .Where(i => i.MaintenancePlanifieeId == maintenancePlanifieeId)
                     .ToList();
        }

        public Intervention GetForDelete(int id)
        {
            return db.Interventions
                     .Include("PiecesReservees")
                     .Include("WorkOrders")
                     .Include("MaintenancePlanifiee")
                     .FirstOrDefault(x => x.Id == id);
        }

        public void RemoveReservedPieces(IEnumerable<Intervention_Piece> pieces)
        {
            db.Intervention_Pieces.RemoveRange(pieces);
            db.SaveChanges();
        }

        public void AddReservedPieces(IEnumerable<Intervention_Piece> pieces)
        {
            db.Intervention_Pieces.AddRange(pieces);
            db.SaveChanges();
        }

        public PieceDeRechange GetPieceStockById(int pieceId)
        {
            return db.PiecesDeRechanges.FirstOrDefault(p => p.pieceId == pieceId);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public List<Intervention> GetAll()
        {
            return db.Interventions.ToList();
        }

        public List<Intervention> GetAllWithMaintenance()
        {
            return db.Interventions
                     .Include("MaintenancePlanifiee")
                     .Include("MaintenancePlanifiee.Equipement")
                     .Include("MaintenancePlanifiee.Responsable")
                     .ToList();
        }

        public bool ExisteInterventionPour(int maintenanceId, DateTime date)
        {
            DateTime dateDebut = date.Date;
            DateTime dateFin = dateDebut.AddDays(1);

            return db.Interventions
                     .Any(i => i.MaintenancePlanifieeId == maintenanceId &&
                               i.DatePrevue >= dateDebut &&
                               i.DatePrevue < dateFin);
        }

        public List<Intervention> GetDisponiblesPourWO(int idResponsable)
        {
            return db.Interventions
                     .Include("MaintenancePlanifiee")
                     .Where(i => i.WorkOrders.Count == 0 &&
                                 i.MaintenancePlanifiee.ResponsableId == idResponsable &&
                                 i.MaintenancePlanifiee.Statut != "Terminee")
                     .ToList();
        }

        public List<Intervention> GetAllByResponsable(int idResponsable)
        {
            return db.Interventions
                     .Include("MaintenancePlanifiee")
                     .Include("MaintenancePlanifiee.Equipement")
                     .Include("MaintenancePlanifiee.Responsable")
                     .Where(i => i.MaintenancePlanifiee.ResponsableId == idResponsable)
                     .ToList();
        }

        public List<Intervention> GetAllByResponsables(List<int> idsResponsables)
        {
            return db.Interventions
                     .Include("MaintenancePlanifiee")
                     .Include("MaintenancePlanifiee.Equipement")
                     .Include("MaintenancePlanifiee.Responsable")
                     .Where(i => idsResponsables.Contains(i.MaintenancePlanifiee.ResponsableId))
                     .ToList();
        }

        public void LoadInterventions(MaintenancePlanifiee maintenance)
        {
            db.Entry(maintenance).Collection(m => m.Interventions).Load();
        }

        public decimal PreparerPiecesEtCalculerCout(List<Intervention_Piece> pieces)
        {
            decimal total = 0;

            foreach (var piece in pieces)
            {
                var stock = db.PiecesDeRechanges.FirstOrDefault(p => p.pieceId == piece.PieceId);
                if (stock == null)
                    throw new InvalidOperationException($"Pièce {piece.PieceId} introuvable.");

                if (stock.quantite < piece.Quantite)
                    throw new InvalidOperationException($"Stock insuffisant pour la pièce \"{stock.nom}\".");

                total += piece.Quantite * stock.prix;
            }

            return total;
        }


    }
}
