using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class EquipeRepository
    {
        private readonly DbManager db;

        public EquipeRepository()
        {
            db = new DbManager();
        }

        public List<Maintenance_team> GetAllEquipesAvecChef()
        {
            return db.Maintenance_Teams
                    .Include("chefEquipe")
                    .AsNoTracking()
                    .ToList();
        }

        public Maintenance_team GetEquipeDetailsById(int id)
        {
            return db.Maintenance_Teams
                   .Include("chefEquipe")
                   .Include("membres.user")
                   .FirstOrDefault(e => e.teamId == id);
        }

        public void AjouterEquipe(Maintenance_team equipe)
        {
            db.Maintenance_Teams.Add(equipe);
            db.SaveChanges();
        }

        public Maintenance_team GetEquipeAvecMembres(int id)
        {
            return db.Maintenance_Teams
                .Include("membres")
                .FirstOrDefault(e => e.teamId == id);
        }

        public List<Team_Users> GetAnciensMembres(int id)
        {
            return db.Team_Users.Where(t => t.teamId == id).ToList();
        }

        public void SupprimerMembres(List<Team_Users> membres)
        {
            db.Team_Users.RemoveRange(membres);
            db.SaveChanges();
        }

        public void AjouterMembres(List<Team_Users> membres)
        {
            db.Team_Users.AddRange(membres);
            db.SaveChanges();
        }

        public void SupprimerEquipe(Maintenance_team equipe)
        {
            db.Maintenance_Teams.Remove(equipe);
            db.SaveChanges();
        }

        public List<User> GetTechniciensActifs()
        {
            return db.Users
                .Where(u => u.fonction == "Technicien" && u.Actif)
                .ToList();
        }

        public DateTime? GetDateAjoutMembre(int teamId, int userId)
        {
            return db.Team_Users
                .Where(t => t.teamId == teamId && t.idUser == userId)
                .Select(t => (DateTime?)t.dateAjout)
                .FirstOrDefault();
        }

        public void SupprimerMembre(int equipeId, int userId)
        {
            var membre = db.Team_Users
                .FirstOrDefault(m => m.teamId == equipeId && m.idUser == userId);
            if (membre != null)
            {
                db.Team_Users.Remove(membre);
                db.SaveChanges();
            }
        }

        public Maintenance_team GetEquipePourSuppression(int id)
        {
            return db.Maintenance_Teams
                .Include("membres")
                .FirstOrDefault(e => e.teamId == id);
        }

    }
}
