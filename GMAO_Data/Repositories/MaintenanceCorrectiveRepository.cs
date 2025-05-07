using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class MaintenanceCorrectiveRepository
    {
        private readonly DbManager db;

        public MaintenanceCorrectiveRepository()
        {
            db = new DbManager();
        }

      
        public bool ExisteStockSuffisant(int pieceId, int quantite)
        {
            var piece = db.PiecesDeRechanges.FirstOrDefault(p => p.pieceId == pieceId);
            return piece != null && piece.quantite >= quantite;
        }

        public void AjouterMaintenance(MaintenanceCorrective maintenance)
        {
            db.MaintenancesCorrectives.Add(maintenance);
            db.SaveChanges();
        }

        public void AjouterReservationPiece(int maintenanceId, int pieceId, int quantite)
        {
            db.Corrective_Pieces.Add(new Corrective_Piece
            {
                MaintenanceCorrectiveId = maintenanceId,
                PieceId = pieceId,
                Quantite = quantite
            });
            db.SaveChanges();
        }

        public decimal CalculerCoutTotal(int maintenanceId)
        {
            return db.Corrective_Pieces
                .Where(p => p.MaintenanceCorrectiveId == maintenanceId)
                .Select(p => p.Piece.prix * p.Quantite)
                .DefaultIfEmpty(0)
                .Sum();
        }

        public MaintenanceCorrective GetById(int id)
        {
            return db.MaintenancesCorrectives
                     .Include("Equipement")
                     .Include("Responsable")
                     .Include("WorkOrder")
                     .FirstOrDefault(m => m.MaintenanceId == id);
        }

        public List<MaintenanceCorrective> GetAllByResponsable(int userId)
        {
            return db.MaintenancesCorrectives
                     .Include("Equipement")
                     .Include("Responsable")
                     .Where(m => m.ResponsableId == userId)
                     .ToList();
        }

        public void ModifierMaintenance(MaintenanceCorrective m)
        {
            db.SaveChanges();
        }

        public void SupprimerReservations(int maintenanceId)
        {
            db.Database.ExecuteSqlCommand("DELETE FROM Corrective_Piece WHERE MaintenanceCorrectiveId = @p0", maintenanceId);
        }

        public void SupprimerMaintenance(MaintenanceCorrective maintenance)
        {
            db.Corrective_Pieces.RemoveRange(maintenance.PiecesReservees);
            db.MaintenancesCorrectives.Remove(maintenance);
            db.SaveChanges();
        }

        public MaintenanceCorrective FindById(int id)
        {
            return db.MaintenancesCorrectives.Find(id);
        }

        public Equipement GetEquipement(int equipementId)
        {
            return db.Equipements.FirstOrDefault(e => e.id == equipementId);
        }

        public List<PieceDeRechange> GetAllPieces()
        {
            return db.PiecesDeRechanges.ToList();
        }
    }
}
