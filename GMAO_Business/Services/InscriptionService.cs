using GMAO_Data.Entities;
using GMAO_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.Services
{
    public class InscriptionService
    {
        private readonly InscriptionRepository _repo;

        public InscriptionService()
        {
            _repo = new InscriptionRepository();
        }

        public string Register(string username, string password, string email, string role = "Technicien")
        {
            if (_repo.UsernameExists(username))
                return "Nom d'utilisateur déjà pris !";

            var hashpwd = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                username = username,
                password = hashpwd,
                fonction = role,
                email = email
            };

            _repo.AddUser(user);
            return "SUCCESS";
        }
    }

}
