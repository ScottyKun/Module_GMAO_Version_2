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
    public class PieceDeRechangeRepository
    {
        private readonly DbManager db = new DbManager();

        public List<PieceDeRechange> GetAllWithRelations()
        {
            return db.PiecesDeRechanges
                     .Include("Stock")
                     .Include("LiaisonsEquipements.Equipement")
                     .ToList();
        }

        public List<Equipement_Pieces> GetRelationsByEquipement(int equipementId)
        {
            return db.Equipement_PieceDeRechanges
                     .Where(ep => ep.EquipementId == equipementId)
                     .Include("PieceDeRechange.Stock")
                     .Include("PieceDeRechange.LiaisonsEquipements.Equipement")
                     .ToList();
        }

        public void Add(PieceDeRechange piece)
        {
            db.PiecesDeRechanges.Add(piece);
            db.SaveChanges();
        }

        public void AddLiaisons(List<Equipement_Pieces> liaisons)
        {
            db.Equipement_PieceDeRechanges.AddRange(liaisons);
            db.SaveChanges();
        }

        public PieceDeRechange GetByIdWithLiaisons(int id)
        {
            return db.PiecesDeRechanges
                     .Include("LiaisonsEquipements")
                     .FirstOrDefault(p => p.pieceId == id);
        }

        public List<Equipement_Pieces> GetLiaisonsByPiece(int pieceId)
        {
            return db.Equipement_PieceDeRechanges
                     .Where(e => e.PieceDeRechangeId == pieceId)
                     .ToList();
        }

        public void RemoveLiaisons(IEnumerable<Equipement_Pieces> liaisons)
        {
            db.Equipement_PieceDeRechanges.RemoveRange(liaisons);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }

        public void DeletePieceWithLiaisons(PieceDeRechange piece, IEnumerable<Equipement_Pieces> liaisons)
        {
            db.Equipement_PieceDeRechanges.RemoveRange(liaisons);
            db.PiecesDeRechanges.Remove(piece);
            db.SaveChanges();
        }

        public void CreateDemande(DemandeAchat demande)
        {
            db.DemandesAchat.Add(demande);
            db.SaveChanges();
        }

        public List<Stock> GetStocks() => db.Stocks.ToList();

        public List<Equipement> GetEquipements() => db.Equipements.ToList();

        public List<int> GetEquipementIdsByPieceId(int pieceId)
        {
            return db.Equipement_PieceDeRechanges
                     .Where(e => e.PieceDeRechangeId == pieceId)
                     .Select(e => e.EquipementId)
                     .ToList();
        }
    }
}
