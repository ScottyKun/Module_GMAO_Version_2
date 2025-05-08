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
    public class GestionProfilService
    {
        private readonly UserRepository userRepository;

        public GestionProfilService()
        {
            userRepository = new UserRepository();
        }

        public UserDTO5 GetUserById(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null) return null;

            return new UserDTO5
            {
                IdUser = user.idUser,
                Nom = user.nom,
                Prenom = user.prenom,
                Telephone = user.tel,
                Email = user.email,
                Username = user.username,
                Fonction = user.fonction
            };
        }


        public void Update(UserDTO5 dto)
        {
            var user = userRepository.GetById(dto.IdUser);
            if (user == null) throw new Exception("Utilisateur introuvable.");

            user.nom = dto.Nom;
            user.prenom = dto.Prenom;
            user.tel = dto.Telephone;
            user.email = dto.Email;
            user.username = dto.Username;

            userRepository.Update(user);
        }
    }
}
