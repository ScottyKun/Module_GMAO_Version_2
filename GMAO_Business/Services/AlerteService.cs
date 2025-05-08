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
    public class AlerteService
    {
        private readonly AlerteRepository repo = new AlerteRepository();

        public void Ajouter(Alerte a) => repo.Ajouter(a);

        public List<Alerte> GetAll() => repo.GetAll();

        public AlerteDTO GetById(int id)
        {
            var a = repo.GetById(id);
            return a == null ? null : ToDTO(a);
        }

        public List<AlerteDTO> GetByPriorite(string priorite)
        {
            var list = repo.GetByPriorite(priorite);
            return list.Select(ToDTO).ToList();
        }
        public void Supprimer(int id) => repo.Supprimer(id);

        public void SupprimerToutesNonLues() => repo.SupprimerToutesNonLues();

        public void MettreAJourStatut(int id, bool terminee) => repo.MettreAJourStatut(id, terminee);

        public List<AlerteDTO> GetRecentesEtNonTraitees()
        {
            int userId = UserContext.IdUser;
            string role = UserContext.Role;

            List<Alerte> alertes;

            if (role == "Responsable")
            {
                alertes = repo.GetAlertesResponsable(userId);
            }
            else if (role == "Technicien")
            {
                var responsables = new UserService().GetResponsablesPourTechnicien(userId);
                alertes = repo.GetAlertesPourTechnicien(responsables);
            }
            else
            {
                alertes = repo.GetAlertesGlobales();
            }

            return alertes.Select(ToDTO).ToList();
        }


        private AlerteDTO ToDTO(Alerte a)
        {
            return new AlerteDTO
            {
                Id = a.Id,
                Libelle = a.Libelle,
                Message = a.Message,
                Priorite = a.Priorite,
                DateCreation = a.DateCreation,
                Terminee = a.Terminee
            };
        }
    }
}
