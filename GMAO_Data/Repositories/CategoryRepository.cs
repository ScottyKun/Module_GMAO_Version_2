using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class CategoryRepository
    {
        private readonly DbManager db = new DbManager();

        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return db.Categories.Find(id);
        }

        public void Add(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void Update(Category category)
        {
            db.SaveChanges();
        }

        public void Delete(Category category)
        {
            db.Categories.Remove(category);
            db.SaveChanges();
        }
    }
}
