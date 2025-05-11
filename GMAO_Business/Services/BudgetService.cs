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
    public class BudgetService
    {
        private readonly BudgetRepository repo = new BudgetRepository();

        public void Ajouter(BudgetDTO dto)
        {
            if (repo.ExistePourAnnee(dto.Annee))
                throw new InvalidOperationException("Un budget pour cette année existe déjà.");

            var budget = new Budget
            {
                Nom = dto.Nom,
                Annee = dto.Annee,
                Montant = dto.Montant,
                DateCreation = dto.DateCreation,
                ResponsableId = dto.ResponsableId
            };

            repo.Ajouter(budget);
        }


        public void Modifier(BudgetDTO dto)
        {
            var b = repo.GetById(dto.BudgetId);
            if (b == null)
                throw new Exception("Budget introuvable.");

            if (repo.ExistePourAnnee(dto.Annee, dto.BudgetId))
                throw new Exception($"Un budget pour l'année {dto.Annee} existe déjà.");

            b.Annee = dto.Annee;
            b.Montant = dto.Montant;
            b.Nom = dto.Nom;

            repo.SaveChanges();
        }

        public List<BudgetDTO> GetBudgetsParAnnee()
        {
            return repo.GetAll()
                       .Select(b => new BudgetDTO
                       {
                           BudgetId = b.BudgetId,
                           Annee = b.Annee,
                           Montant = b.Montant,
                           Nom = b.Nom,
                           DateCreation = b.DateCreation,
                           ResponsableId = b.ResponsableId
                       }).ToList();
        }
    }
}
