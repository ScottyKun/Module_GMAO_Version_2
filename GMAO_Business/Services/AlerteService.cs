using GMAO_Data.Entities;
using GMAO_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.Services
{
    public class AlerteService
    {
        private readonly AlerteRepository repo = new AlerteRepository();

        public void Ajouter(Alerte a) => repo.Ajouter(a);

        public List<Alerte> GetAll() => repo.GetAll();

        public Alerte GetById(int id) => repo.GetById(id);

        public List<Alerte> GetByPriorite(string priorite) => repo.GetByPriorite(priorite);

        public void Supprimer(int id) => repo.Supprimer(id);

        public void SupprimerToutesNonLues() => repo.SupprimerToutesNonLues();

        public void MettreAJourStatut(int id, bool terminee) => repo.MettreAJourStatut(id, terminee);

        public List<Alerte> GetRecentesEtNonTraitees()
        {
            int userId = UserContext.IdUser;
            string role = UserContext.Role;
            var query = repo.AsQueryable();

            if (role == "Responsable")
            {
                return query
                    .Where(a =>
                        (a.Libelle == "Alerte Stock" ||
                        (a.ResponsableId != null && a.ResponsableId == userId)) &&
                        !a.Terminee)
                    .OrderByDescending(a => a.DateCreation)
                    .ToList();
            }
            else if (role == "Technicien")
            {
                var responsables = new UserService().GetResponsablesPourTechnicien(userId);
                return query
                    .Where(a =>
                        a.Libelle != "Alerte Stock" &&
                        a.ResponsableId != null &&
                        responsables.Contains(a.ResponsableId.Value) &&
                        !a.Terminee)
                    .OrderByDescending(a => a.DateCreation)
                    .ToList();
            }
            else
            {
                return query
                    .Where(a => !a.Terminee && a.DateCreation >= DateTime.Today.AddDays(-3))
                    .OrderByDescending(a => a.DateCreation)
                    .ToList();
            }
        }
    }
}
