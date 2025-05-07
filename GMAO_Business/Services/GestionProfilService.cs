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

        public User GetUserById(int id)
        {
            return userRepository.GetById(id);
        }

        public void Update(User user)
        {
            userRepository.Update(user);
        }
    }
}
