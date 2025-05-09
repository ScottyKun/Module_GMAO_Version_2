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
   public class EquipementService
    {
        private readonly EquipementRepository _repository= new EquipementRepository() ;

      

        public List<EquipementDTO> GetAll()
        {
            if (UserContext.Role == "Responsable")
            {
                var equipements = _repository.GetAllByResponsableId(UserContext.IdUser);

                return equipements.Select(e => new EquipementDTO
                {
                    Id = e.id,
                    Nom = e.nom,
                    Categorie = e.Categorie?.nom,
                    DateAchat = e.dateAchat,
                    Responsable = e.responsable?.nom,
                    Statut = e.statut,
                    DateFinGarantie = e.dateFinGarantie,
                    MaintenanceTeam = e.maintenanceTeam?.nom,
                    Commentaires = e.commentaires
                }).ToList();
            }

            return null;
        }

        public EquipementDTO2 GetById(int id)
        {
            var equipement = _repository.GetById(id);

            if (equipement == null)
                return null;

            return new EquipementDTO2
            {
                Id = equipement.id,
                NomResponsable = equipement.responsable?.nom ?? "-",
                NomEquipe = equipement.maintenanceTeam?.nom ?? "-",
                MaintenanceTeamId = equipement.maintenanceTeamId
            };
        }

        public EquipementDTO3 GetById3(int id)
        {
            var equipement = _repository.GetById(id);

            if (equipement == null)
                return null;

            return new EquipementDTO3
            {
                Id = equipement.id,
                Nom = equipement.nom,
                CategorieId = equipement.CategorieId,
                ResponsableId = equipement.responsableId,
                MaintenanceTeamId = equipement.maintenanceTeamId,
                DateAchat = equipement.dateAchat,
                DateFinGarantie = equipement.dateFinGarantie,
                Statut = equipement.statut,
                Commentaires = equipement.commentaires
            };
        }


        public Equipement2DTO GetById2(int id)
        {
            var eq = _repository.GetById(id);
            if (eq == null) return null;

            return new Equipement2DTO
            {
                Id = eq.id,
                Nom = eq.nom,
                MaintenanceTeam = eq.maintenanceTeam != null ? new EquipeDTO2
                {
                    Id = eq.maintenanceTeam.teamId,
                    Nom = eq.maintenanceTeam.nom
                } : null
            };
        }


        public void Add(EquipementDTO3 dto)
        {
            var entity = new Equipement
            {
                nom = dto.Nom,
                CategorieId = dto.CategorieId,
                responsableId = dto.ResponsableId,
                maintenanceTeamId = dto.MaintenanceTeamId,
                dateAchat = dto.DateAchat,
                dateFinGarantie = dto.DateFinGarantie,
                statut = dto.Statut,
                commentaires = dto.Commentaires
            };

            _repository.Add(entity);
        }

        public void Update(EquipementDTO3 dto)
        {
            var entity = _repository.GetById(dto.Id);
            if (entity == null) throw new Exception("Équipement non trouvé.");

            entity.nom = dto.Nom;
            entity.CategorieId = dto.CategorieId;
            entity.responsableId = dto.ResponsableId;
            entity.maintenanceTeamId = dto.MaintenanceTeamId;
            entity.dateAchat = dto.DateAchat;
            entity.dateFinGarantie = dto.DateFinGarantie;
            entity.statut = dto.Statut;
            entity.commentaires = dto.Commentaires;

            _repository.Update(entity);
        }

        public void Delete(int id) => _repository.Delete(id);

        public List<CategoryDTO> GetCategories()
        {
            return _repository.GetCategories()
                              .Select(c => new CategoryDTO { id = c.categoryId, nom = c.nom })
                              .ToList();
        }

        public List<UserDTO> GetResponsable()
        {
            return _repository.GetResponsables()
                              .Where(u => u.idUser == UserContext.IdUser)
                              .Select(u => new UserDTO
                              {
                                  idUser = u.idUser,
                                  nom = u.nom,
                                  prenom = u.prenom,
                                  email = u.email,
                                  tel = u.tel
                              }).ToList();
        }

        public List<TeamInfo> GetTeams()
        {
            return _repository.GetTeamsActives()
                              .Select(t => new TeamInfo
                              {
                                  Id = t.teamId,
                                  Nom = t.nom,
                                  Statut = t.statut,
                                  ChefEquipeId = t.chefEquipeId,
                                  NomChef = t.chefEquipe != null ? $"{t.chefEquipe.nom} {t.chefEquipe.prenom}" : ""
                              }).ToList();
        }

        public List<EquipementLightDTO> GetEquipementsLightByResponsable(int idUser)
        {
            return _repository.GetEquipementsLightByResponsable(idUser)
                              .Select(e => new EquipementLightDTO
                              {
                                  Id = e.id,
                                  Nom = e.nom
                              }).ToList();
        }

        public List<PieceReservationView> GetPiecesDeRechangeParEquipement(int equipementId)
        {
            return _repository.GetPiecesByEquipementId(equipementId)
                              .Select(p => new PieceReservationView
                              {
                                  PieceId = p.PieceDeRechange.pieceId,
                                  Nom = p.PieceDeRechange.nom,
                                  QuantiteStock = p.PieceDeRechange.quantite,
                                  QuantiteAReserver = 0
                              }).ToList();
        }
    }
}
