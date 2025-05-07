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
    public class UserService
    {
        private readonly UserRepository _repo;

        public UserService()
        {
            _repo = new UserRepository();
        }

        public List<int> GetResponsablesPourTechnicien(int id) => _repo.GetResponsablesPourTechnicien(id);

        public List<UserDTO3> Lister()
        {
            return _repo.GetAllUsers()
                        .Select(u => new UserDTO3
                        {
                            IdUser = u.idUser,
                            Nom = u.nom,
                            Prenom = u.prenom,
                            Email = u.email,
                            Tel = u.tel,
                            Fonction = u.fonction,
                            Username = u.username,
                            Statut = u.Actif
                        })
                .OrderBy(u => u.Nom)
                .ToList();
        }

        public void Activer(int id)
        {
            var user = _repo.GetById(id) ?? throw new Exception("Utilisateur introuvable.");
            user.Actif = true;
            _repo.Update(user);
        }

        public void ModifierMotDePasse(int id, string newPassword)
        {
            var user = _repo.GetById(id) ?? throw new Exception("Utilisateur introuvable.");
            user.password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _repo.Update(user);
        }

        public void Supprimer(int id)
        {
            var user = _repo.GetById(id) ?? throw new Exception("Utilisateur introuvable.");
            if (user.teams_chief.Any())
            {
                var nomsEquipes = string.Join(", ", user.teams_chief.Select(e => $"« {e.nom} »"));
                var responsables = _repo.GetAllUsers().Where(u => u.fonction == "Responsable" && u.Actif).ToList();
                foreach (var res in responsables)
                {
                    _repo.AddAlert(new Alerte
                    {
                        Libelle = "Alerte Suppression Chef",
                        Message = $"Suppression de {user.nom}, chef de {nomsEquipes}",
                        ResponsableId = res.idUser,
                        Priorite = "Élevée",
                        DateCreation = DateTime.Now,
                        Terminee = false
                    });
                }
                throw new InvalidOperationException($"Chef d'équipe de {nomsEquipes}");
            }

            _repo.RemoveUserTeams(id);
            user.Actif = false;
            _repo.Update(user);
        }

        public List<UserDTO3> Rechercher(string terme) {
            if (string.IsNullOrWhiteSpace(terme))
                return new List<UserDTO3>();

            terme = terme.Trim().ToLower();
            return _repo.GetAllUsers()
                .Where(u => u.nom.ToLower().Contains(terme)
                         || u.prenom.ToLower().Contains(terme)
                         || u.username.ToLower().Contains(terme)
                         || u.email.ToLower().Contains(terme))
                .Select(u => new UserDTO3
                {
                    IdUser = u.idUser,
                    Nom = u.nom,
                    Prenom = u.prenom,
                    Email = u.email,
                    Tel = u.tel,
                    Fonction = u.fonction,
                    Username = u.username,
                    Statut = u.Actif
                })
                .OrderBy(u => u.Nom)
                .ToList();
        }

        public void ModifierInfos(UserDTO3 dto)
        {
            var user = _repo.GetById(dto.IdUser);
            if (user == null)
                throw new Exception("Utilisateur introuvable.");

            user.nom = dto.Nom;
            user.prenom = dto.Prenom;
            user.email = dto.Email;
            user.tel = dto.Tel;
            user.fonction = dto.Fonction;
            user.Actif = dto.Statut;

            _repo.Update(user);
        }

        public bool ProfilIncomplet(int id) => _repo.ProfilIncomplet(id);
    }

}
