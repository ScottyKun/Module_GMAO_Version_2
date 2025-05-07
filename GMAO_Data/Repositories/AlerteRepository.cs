using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class AlerteRepository
    {
        private readonly DbManager db = new DbManager();

        public void Ajouter(Alerte a)
        {
            db.Alertes.Add(a);
            db.SaveChanges();
        }

        public List<Alerte> GetAll()
        {
            return db.Alertes.OrderByDescending(a => a.DateCreation).ToList();
        }

        public Alerte GetById(int id)
        {
            return db.Alertes.FirstOrDefault(a => a.Id == id);
        }

        public List<Alerte> GetByPriorite(string priorite)
        {
            return db.Alertes
                     .Where(a => a.Priorite == priorite && !a.Terminee)
                     .OrderByDescending(a => a.DateCreation)
                     .ToList();
        }

        public void Supprimer(int id)
        {
            var alerte = db.Alertes.FirstOrDefault(a => a.Id == id && !a.Terminee);
            if (alerte != null)
            {
                db.Alertes.Remove(alerte);
                db.SaveChanges();
            }
        }

        public void SupprimerToutesNonLues()
        {
            var nonLues = db.Alertes.Where(a => !a.Terminee).ToList();
            db.Alertes.RemoveRange(nonLues);
            db.SaveChanges();
        }

        public void MettreAJourStatut(int id, bool terminee)
        {
            var alerte = db.Alertes.FirstOrDefault(a => a.Id == id);
            if (alerte != null)
            {
                alerte.Terminee = terminee;
                db.SaveChanges();
            }
        }

        public IQueryable<Alerte> AsQueryable()
        {
            return db.Alertes.AsQueryable();
        }
    }
}
