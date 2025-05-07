using GMAO_Business.DTOs;
using GMAO_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.Services
{
    public class AuthentificationService
    {
        private readonly AuthentificationRepository _repo;

        public AuthentificationService()
        {
            _repo = new AuthentificationRepository();
        }

        public UserDTO4 Authentifier(string username, string pwd)
        {
            var user = _repo.GetByUsername(username);
            if (user == null) return null;

            if (!user.Actif)
                throw new InvalidOperationException("Compte désactivé. Veuillez contacter un administrateur.");

            if (!BCrypt.Net.BCrypt.Verify(pwd, user.password))
                return null;

            return new UserDTO4
            {
                IdUser = user.idUser,
                Username = user.username,
                Fonction = user.fonction,
                Actif = user.Actif
            };
        }
    }
}


