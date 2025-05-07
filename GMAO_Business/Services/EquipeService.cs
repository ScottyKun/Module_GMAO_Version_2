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
    public class EquipeService
    {
        private readonly EquipeRepository _repo;

        public EquipeService()
        {
            _repo = new EquipeRepository();
        }

        public List<TeamInfo> GetAllEquipes()
        {
            return _repo.GetAllEquipesAvecChef()
                .Select(t => new TeamInfo
                {
                    Id = t.teamId,
                    Nom = t.nom,
                    Statut = t.statut,
                    ChefEquipeId = t.chefEquipeId,
                    NomChef = t.chefEquipe != null ? t.chefEquipe.nom + " " + t.chefEquipe.prenom : "Aucun chef"
                })
                .ToList();
        }

        public EquipeDTO GetEquipeById(int id)
        {
            var equipe = _repo.GetEquipeDetailsById(id);
            if (equipe == null) return null;

            return new EquipeDTO
            {
                IdEquipe = equipe.teamId,
                NomEquipe = equipe.nom,
                Statut = equipe.statut,
                IdChefEquipe = equipe.chefEquipeId,
                ChefEquipeNom = equipe.chefEquipe != null ? $"{equipe.chefEquipe.nom} {equipe.chefEquipe.prenom}" : "Non assigné",
                Membres = equipe.membres.Select(m => new UserDTO2
                {
                    idUser = m.user.idUser,
                    nom = m.user.nom,
                    prenom = m.user.prenom,
                    email = m.user.email,
                    tel = m.user.tel,
                    dateAjout = m.dateAjout
                }).ToList()
            };
        }

        public void AjouterEquipe(string nom, int chefId, bool state, List<int> membresIds)
        {
            var equipe = new Maintenance_team
            {
                nom = nom,
                chefEquipeId = chefId,
                statut = state,
                membres = membresIds.Select(id => new Team_Users
                {
                    idUser = id,
                    dateAjout = DateTime.Now
                }).ToList()
            };

            _repo.AjouterEquipe(equipe);
        }

        public void ModifierEquipe(int id, string nom, int chefId, bool state, List<int> membresIds)
        {
            var equipe = _repo.GetEquipeAvecMembres(id);
            if (equipe == null)
                throw new Exception("Équipe introuvable");

            equipe.nom = nom;
            equipe.chefEquipeId = chefId;
            equipe.statut = state;

            var anciensMembres = _repo.GetAnciensMembres(id);
            _repo.SupprimerMembres(anciensMembres);

            var nouveauxMembres = membresIds.Select(m => new Team_Users
            {
                teamId = id,
                idUser = m,
                dateAjout = DateTime.Now
            }).ToList();

            _repo.AjouterMembres(nouveauxMembres);
        }

        public void SupprimerEquipe(int id)
        {
            var equipe = _repo.GetEquipePourSuppression(id);
            if (equipe != null)
            {
                _repo.SupprimerMembres(equipe.membres.ToList());
                _repo.SupprimerEquipe(equipe);
            }
        }

        public List<UserDTO> GetAllUtilisateurs()
        {
            return _repo.GetTechniciensActifs()
                .Select(u => new UserDTO
                {
                    idUser = u.idUser,
                    nom = u.nom,
                    prenom = u.prenom,
                    email = u.email,
                    tel = u.tel
                }).ToList();
        }

        public List<UserDTO2> GetAllUtilisateursDate(int teamId)
        {
            return _repo.GetTechniciensActifs()
                .Select(u => new UserDTO2
                {
                    idUser = u.idUser,
                    nom = u.nom,
                    prenom = u.prenom,
                    email = u.email,
                    tel = u.tel,
                    dateAjout = _repo.GetDateAjoutMembre(teamId, u.idUser)
                }).ToList();
        }

        public void SupprimerMembreEquipe(int equipeId, int utilisateurId)
        {
            _repo.SupprimerMembre(equipeId, utilisateurId);
        }
    }
}
