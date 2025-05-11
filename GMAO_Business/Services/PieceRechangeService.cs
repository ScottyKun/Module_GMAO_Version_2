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
   public class PieceRechangeService
    {
        private readonly PieceDeRechangeRepository repository = new PieceDeRechangeRepository();

        public List<PieceDTO> GetAllPieces()
        {
            return repository.GetAllWithRelations().Select(p => new PieceDTO
            {
                PieceId = p.pieceId,
                Nom = p.nom,
                Reference = p.reference,
                Prix = p.prix,
                DateAjout = p.dateAjout,
                Quantite = p.quantite,
                StockNom = p.Stock?.nom,
                Equipements = p.LiaisonsEquipements.Select(ep => ep.Equipement.nom).ToList()
            }).ToList();
        }

        public List<PieceDTO> GetPiecesCritiques(int seuil)
        {
            return GetAllPieces().Where(p => p.Quantite < seuil).ToList();
        }

        public List<PieceDTO> GetByEquipement(int equipementId)
        {
            var relations = repository.GetRelationsByEquipement(equipementId);
            return relations.Select(ep => ep.PieceDeRechange).Distinct().Select(p => new PieceDTO
            {
                PieceId = p.pieceId,
                Nom = p.nom,
                Reference = p.reference,
                Prix = p.prix,
                DateAjout = p.dateAjout,
                Quantite = p.quantite,
                StockNom = p.Stock?.nom,
                Equipements = p.LiaisonsEquipements.Select(ep => ep.Equipement.nom).ToList()
            }).ToList();
        }

        public void AddPiece(PieceDeRechangeDTO dto)
        {
            var piece = new PieceDeRechange
            {
                nom = dto.Nom,
                reference = dto.Reference,
                prix = dto.Prix,
                dateAjout = dto.DateAjout,
                quantite = dto.Quantite,
                StockId = dto.StockId
            };

            repository.Add(piece);

            var liaisons = dto.EquipementIds.Select(id => new Equipement_Pieces
            {
                EquipementId = id,
                PieceDeRechangeId = piece.pieceId
            }).ToList();

            repository.AddLiaisons(liaisons);
        }


        public void UpdatePiece(PieceDeRechangeDTO dto)
        {
            if (dto.PieceId==null)
                throw new ArgumentException("L'ID de la pièce à modifier est requis.");

            var piece = repository.GetByIdWithLiaisons(dto.PieceId);
            if (piece != null)
            {
                piece.nom = dto.Nom;
                piece.reference = dto.Reference;
                piece.prix = dto.Prix;
                piece.dateAjout = dto.DateAjout;
                piece.quantite = dto.Quantite;
                piece.StockId = dto.StockId;

                var anciennes = repository.GetLiaisonsByPiece(piece.pieceId);
                repository.RemoveLiaisons(anciennes);

                var nouvelles = dto.EquipementIds.Select(id => new Equipement_Pieces
                {
                    EquipementId = id,
                    PieceDeRechangeId = piece.pieceId
                }).ToList();
                repository.AddLiaisons(nouvelles);

                repository.Update();
            }
        }


        public void DeletePiece(int id)
        {
            var piece = repository.GetByIdWithLiaisons(id);
            if (piece != null)
            {
                var liaisons = repository.GetLiaisonsByPiece(id);
                repository.DeletePieceWithLiaisons(piece, liaisons);
            }
        }

        public void CreateDemandeAchat(PieceDTO piece)
        {
            var demande = new DemandeAchat
            {
                Libelle = $"Commande automatique pour la pièce '{piece.Nom}'",
                NomPiece = piece.Nom,
                Reference = piece.Reference,
                DateDemande = DateTime.Now
            };

            repository.CreateDemande(demande);
        }

        public List<StockDTO> GetStocks()
        {
            return repository.GetStocks().Select(s => new StockDTO
            {
                Id = s.stockId,
                Nom = s.nom
            }).ToList();
        }

        public List<EquipementLightDTO> GetEquipements()
        {
            return repository.GetEquipements().Select(e => new EquipementLightDTO
            {
                Id = e.id,
                Nom = e.nom
            }).ToList();
        }

        public List<int> GetByEquipementIds(int pieceId)
        {
            return repository.GetEquipementIdsByPieceId(pieceId);
        }

        public EquipementLightDTO GetEquipementByNomApprox(string nom)
        {
            nom = nom.Trim().ToLower();
            var equipements = repository.GetEquipements();
            var equipement = equipements.FirstOrDefault(e => e.nom != null && e.nom.ToLower().Contains(nom));

            if (equipement == null) return null;

            return new EquipementLightDTO
            {
                Id = equipement.id,
                Nom = equipement.nom
            };
        }

    }
}
