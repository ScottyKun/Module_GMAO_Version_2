using GMAO_Business.DTOs;
using GMAO_Data.Entities;
using GMAO_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.Services
{
    public class StockService
    {
        private readonly StockRepository repository = new StockRepository();

        public List<StockDTO> GetAllStocks()
        {
            return repository.GetAll()
                             .Select(s => new StockDTO
                             {
                                 Id = s.stockId,
                                 Nom = s.nom
                             }).ToList();
        }

        public void Add(StockDTO dto)
        {
            var stock = new Stock
            {
                nom = dto.Nom
            };
            repository.Add(stock);
        }

        public void Update(StockDTO dto)
        {
            var entity = repository.GetById(dto.Id);
            if (entity != null)
            {
                entity.nom = dto.Nom;
                repository.Update();
            }
        }

        public void Delete(int id)
        {
            var entity = repository.GetById(id);
            if (entity != null)
            {
                repository.Delete(entity);
            }
        }
    }
}
