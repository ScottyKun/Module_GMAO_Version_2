using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class ProfilRepository
    {
        private readonly DbManager db;

        public ProfilRepository()
        {
            db = new DbManager();
        }

        // Récupère un utilisateur par ID
        public User GetById(int id)
        {
            return db.Users.FirstOrDefault(u => u.idUser == id);
        }

        // Met à jour les informations d'un utilisateur
        public void Update(User user)
        {
            var existingUser = db.Users.FirstOrDefault(u => u.idUser == user.idUser);
            if (existingUser != null)
            {
                existingUser.nom = user.nom;
                existingUser.prenom = user.prenom;
                existingUser.tel = user.tel;
                existingUser.email = user.email;
                existingUser.username = user.username;

                db.SaveChanges();
            }
        }
    }
}
