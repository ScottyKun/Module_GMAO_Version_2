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
    public class CategoryService
    {
        private readonly CategoryRepository repo = new CategoryRepository();

        public List<CategoryDTO> GetAll()
        {
            return repo.GetAll()
                       .Select(c => new CategoryDTO
                       {
                           id = c.categoryId,
                           nom = c.nom
                       }).ToList();
        }

        public void Add(CategoryDTO dto)
        {
            var entity = new Category { nom = dto.nom };
            repo.Add(entity);
        }

        public void Update(CategoryDTO dto)
        {
            var entity = repo.GetById(dto.id);
            if (entity != null)
            {
                entity.nom = dto.nom;
                repo.Update(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = repo.GetById(id);
            if (entity != null)
                repo.Delete(entity);
        }
    }
}
