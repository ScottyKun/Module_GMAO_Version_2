using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var teamIds = _context.Team_Users
                            .Where(t => t.idUser == technicienId)
                            .Select(t => t.teamId)
                            .ToList();

            var responsables = _context.Equipements
                                 .Where(e => teamIds.Contains(e.maintenanceTeamId))
                                 .Select(e => e.responsableId)
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


        public void Update(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void AddAlert(Alerte alert)
        {
            _context.Alertes.Add(alert);
            _context.SaveChanges();
        }



        public void RemoveUserTeams(int userId)
        {
            var liaisons = _context.Team_Users.Where(t => t.idUser == userId).ToList();
            _context.Team_Users.RemoveRange(liaisons);
            _context.SaveChanges();
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
