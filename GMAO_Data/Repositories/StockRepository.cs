using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class StockRepository
    {
        private readonly DbManager db = new DbManager();

        public List<Stock> GetAll()
        {
            return db.Stocks.ToList();
        }

        public Stock GetById(int id)
        {
            return db.Stocks.Find(id);
        }

        public void Add(Stock stock)
        {
            db.Stocks.Add(stock);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }

        public void Delete(Stock stock)
        {
            db.Stocks.Remove(stock);
            db.SaveChanges();
        }
    }
}
