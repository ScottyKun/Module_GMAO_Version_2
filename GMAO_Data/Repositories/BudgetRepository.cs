using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class BudgetRepository
    {
        private readonly DbManager db = new DbManager();

        public void Ajouter(Budget budget)
        {
            db.Budgets.Add(budget);
            db.SaveChanges();
        }

        public Budget GetById(int id)
        {
            return db.Budgets.Find(id);
        }

        public bool ExistePourAnnee(int annee, int? exclureId = null)
        {
            return db.Budgets.Any(b => b.Annee == annee && (exclureId == null || b.BudgetId != exclureId));
        }

        public List<Budget> GetAll()
        {
            return db.Budgets.OrderByDescending(b => b.Annee).ToList();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }


    }
}
