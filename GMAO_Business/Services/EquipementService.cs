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
   public class EquipementService
    {
        private readonly EquipementRepository _repository;

        public EquipementService(EquipementRepository repository)
        {
            _repository = repository;
        }

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

        public Equipement GetById(int id) => _repository.GetById(id);

        public void Add(Equipement equipement) => _repository.Add(equipement);

        public void Update(Equipement equipement) => _repository.Update(equipement);

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
