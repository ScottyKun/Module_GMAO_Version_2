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
    public class EquipementRepository
    {
        private readonly DbManager _context;

        public EquipementRepository()
        {
            _context = new DbManager();
        }

        public List<Equipement> GetAllByResponsableId(int responsableId)
        {
            return _context.Equipements
                .Include("Categorie")
                .Include("responsable")
                .Include("maintenanceTeam")
                .Where(e => e.responsableId == responsableId)
                .ToList();
        }

        public Equipement GetById(int id)
        {
            return _context.Equipements
                 .Include("Categorie")
                 .Include("responsable")
                 .Include("maintenanceTeam")
                 .FirstOrDefault(e => e.id == id);
        }

        public void Add(Equipement equipement)
        {
            _context.Equipements.Add(equipement);
            _context.SaveChanges();
        }

        public void Update(Equipement equipement)
        {
            _context.Entry(equipement).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var equip = _context.Equipements.Find(id);
            if (equip != null)
            {
                _context.Equipements.Remove(equip);
                _context.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public List<User> GetResponsables()
        {
            return _context.Users
                           .Where(u => u.fonction == "Responsable")
                           .ToList();
        }

        public List<Maintenance_team> GetTeamsActives()
        {
            return _context.Maintenance_Teams
                           .Include(t => t.chefEquipe)
                           .Where(t => t.statut == true)
                           .ToList();
        }

        public List<Equipement> GetEquipementsLightByResponsable(int userId)
        {
            return _context.Equipements
                           .Where(e => e.responsableId == userId && e.statut == true)
                           .ToList();
        }

        public List<Equipement_Pieces> GetPiecesByEquipementId(int equipementId)
        {
            return _context.Equipement_PieceDeRechanges
                           .Include(p => p.PieceDeRechange)
                           .Where(p => p.EquipementId == equipementId)
                           .ToList();
        }
    }
}
