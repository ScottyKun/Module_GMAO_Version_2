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
    public class  InterventionService
    {
        private readonly InterventionRepository repo;
        private readonly MaintenancePlanService _maintenancePlanService;

        /*public InterventionService()
        {
            repo = new InterventionRepository();
        }

        public void Creer(Intervention i)
        {
            if (i.MaintenancePlanifieeId !=null)
            {
                var maintenance = _maintenancePlanService.GetById(i.MaintenancePlanifieeId);
                if (maintenance == null)
                    throw new InvalidOperationException("Maintenance planifiée non trouvée.");
            }

            i.Etat = "New";
            repo.Add(i);

            repo.SaveChanges();
        }

        public Intervention GetById(int id) => repo.GetById(id);

        public List<Intervention> GetByMaintenanceId(int maintenancePlanifieeId) =>
            repo.GetByMaintenanceId(maintenancePlanifieeId);

        public void Supprimer(int id)
        {
            var i = repo.GetForDeletion(id);
            if (i == null) return;

            if (i.WorkOrders.Any(wo => wo.Terminee))
                throw new InvalidOperationException("Impossible de supprimer une intervention avec des WorkOrders terminés.");

            repo.RemovePieces(i.PiecesReservees);
            repo.Remove(i);
            repo.SaveChanges();

            var maintenance = i.MaintenancePlanifiee;
            new MaintenanceRepository().RechargerInterventions(maintenance);

            if (!maintenance.Interventions.Any())
            {
                maintenance.Statut = "Nouvelle";
                maintenance.CoutPrevu = 0;
                maintenance.CoutReel = 0;
                maintenance.NbInterventionsFinish = 0;
            }
            else
            {
                new MaintenancePlanService().RecalculerCouts(maintenance.MaintenanceId);
            }

            new MaintenanceRepository().Update(maintenance);
            repo.SaveChanges();
        }

        public void ModifierComplet(Intervention modif, List<PieceReservationDTO> nouvellesPieces)
        {
            var i = repo.GetById(modif.Id);

            if (i == null || i.Etat != "New")
                throw new InvalidOperationException("Impossible de modifier une intervention en cours.");

            i.Nom = modif.Nom;
            i.DatePrevue = modif.DatePrevue;

            repo.RemovePieces(i.PiecesReservees);

            decimal totalPrevu = 0;
            List<Intervention_Piece> piecesAAjouter = new();

            foreach (var p in nouvellesPieces)
            {
                var stock = repo.GetPieceById(p.PieceId);
                if (stock == null)
                    throw new InvalidOperationException($"Pièce {p.PieceId} introuvable.");

                if (stock.quantite < p.Quantite)
                    throw new InvalidOperationException($"Stock insuffisant pour la pièce \"{stock.nom}\".");

                totalPrevu += p.Quantite * stock.prix;

                piecesAAjouter.Add(new Intervention_Piece
                {
                    InterventionId = i.Id,
                    PieceId = p.PieceId,
                    Quantite = p.Quantite
                });
            }

            repo.AddPieces(piecesAAjouter);
            i.Cout = totalPrevu;
            repo.SaveChanges();

            new MaintenancePlanService().RecalculerCouts(i.MaintenancePlanifieeId);
        }

        public void RecalculerCout(int interventionId)
        {
            var i = repo.GetById(interventionId);
            if (i == null) return;

            var coutPrevu = i.PiecesReservees.Sum(p => p.Quantite * p.Piece.prix);
            var coutReel = i.WorkOrders.Where(w => w.Terminee).Sum(w => w.Cout);

            i.Cout = coutPrevu;
            repo.SaveChanges();

            new MaintenancePlanService().RecalculerCouts(i.MaintenancePlanifieeId);
        }

        public List<InterventionDTO> GetAllDTOByResponsable(int idResponsable)
        {
            var list = repo.GetAllWithResponsable(idResponsable);

            return list.Select(i => new InterventionDTO
            {
                Id = i.Id,
                Nom = i.Nom,
                Etat = i.Etat,
                DatePrevue = i.DatePrevue,
                Cout = i.Cout,
                MaintenanceId = i.MaintenancePlanifieeId,
                MaintenanceDescription = i.MaintenancePlanifiee.Description,
                EquipementNom = i.MaintenancePlanifiee.Equipement.nom
            }).ToList();
        }

        public bool ExisteInterventionPour(int maintenanceId, DateTime date)
        {
            DateTime dateDebut = date.Date;
            DateTime dateFin = dateDebut.AddDays(1);
            return repo.ExistsForDate(maintenanceId, dateDebut, dateFin);
        }

        public List<InterventionLightDTO> GetInterventionsDisponiblesPourWO(int idResponsable)
        {
            return repo.GetInterventionsDisponiblesPourWO(idResponsable)
                       .Select(i => new InterventionLightDTO
                       {
                           Id = i.Id,
                           Description = i.Nom
                       }).ToList();
        }

        public List<InterventionDTO> GetAllPourUtilisateur()
        {
            if (UserContext.Role == "Responsable")
            {
                return GetAllDTOByResponsable(UserContext.IdUser);
            }
            else
            {
                var responsables = new UserService().GetResponsablesPourTechnicien(UserContext.IdUser);
                var list = repo.GetAll()
                               .Where(i => responsables.Contains(i.MaintenancePlanifiee.ResponsableId))
                               .ToList();

                return list.Select(i => new InterventionDTO
                {
                    Id = i.Id,
                    Nom = i.Nom,
                    Etat = i.Etat,
                    DatePrevue = i.DatePrevue,
                    Cout = i.Cout,
                    MaintenanceId = i.MaintenancePlanifieeId,
                    MaintenanceDescription = i.MaintenancePlanifiee.Description,
                    EquipementNom = i.MaintenancePlanifiee.Equipement.nom
                }).ToList();
            }*/
    }
}
