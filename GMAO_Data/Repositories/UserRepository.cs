using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class UserRepository
    {
        private readonly DbManager _context;

        public UserRepository()
        {
            _context = new DbManager();
        }

        public List<int> GetResponsablesPourTechnicien(int technicienId)
        {
            var responsables = _context.Maintenance_Teams
                .Where(t => t.membres.Any(m => m.idUser == technicienId) && t.chefEquipe.fonction == "Responsable")
                .Select(t => t.chefEquipe.idUser)
                .Distinct()
                .ToList();

            return responsables;
        }


        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }


        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.idUser == id);
        }


        public void Update(User user) { _context.SaveChanges(); }

        public void AddAlert(Alerte alert) { _context.Alertes.Add(alert); }

        public void RemoveUserTeams(int userId)
        {
            var liaisons = _context.Team_Users.Where(t => t.idUser == userId).ToList();
            _context.Team_Users.RemoveRange(liaisons);
        }

        public bool ProfilIncomplet(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.idUser == userId);
            return user == null || string.IsNullOrWhiteSpace(user.nom)
                                || string.IsNullOrWhiteSpace(user.prenom)
                                || string.IsNullOrWhiteSpace(user.email)
                                || string.IsNullOrWhiteSpace(user.tel);
        }
    }

}
