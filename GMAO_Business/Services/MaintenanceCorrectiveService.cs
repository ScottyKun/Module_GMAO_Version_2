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
    public class MaintenanceCorrectiveService
    {
        private readonly MaintenanceCorrectiveRepository repo;
        private readonly MaintenanceService maintenanceService;

        public MaintenanceCorrectiveService()
        {
            repo = new MaintenanceCorrectiveRepository();
            maintenanceService = new MaintenanceService();
        }

        public void CreerMaintenanceCorrective(MaintenanceCorrective maintenance, List<PieceReservationDTO> pieces)
        {
            if (!maintenanceService.PeutCreerMaintenance(maintenance.EquipementId, maintenance.DateCreation.Date, maintenance.DateCreation.Date))
                throw new InvalidOperationException("Une maintenance existe déjà pour cette période.");

            foreach (var p in pieces)
            {
                if (!repo.ExisteStockSuffisant(p.PieceId, p.Quantite))
                    throw new InvalidOperationException($"Stock insuffisant pour la pièce {p.PieceId}.");
            }

            repo.AjouterMaintenance(maintenance);

            foreach (var p in pieces)
                repo.AjouterReservationPiece(maintenance.MaintenanceId, p.PieceId, p.Quantite);

            maintenance.Cout = repo.CalculerCoutTotal(maintenance.MaintenanceId);
        }

        public decimal CalculerCoutPrevu(int maintenanceId)
        {
            return repo.CalculerCoutTotal(maintenanceId);
        }

        public void ChangerStatut(int maintenanceId, string statut)
        {
            var maintenance = repo.FindById(maintenanceId);
            if (maintenance == null) return;

            maintenance.Statut = statut;
            repo.ModifierMaintenance(maintenance);

            if (statut == "Echec")
            {
                var equip = repo.GetEquipement(maintenance.EquipementId);
                if (equip != null)
                {
                    equip.statut = false;
                    repo.ModifierMaintenance(maintenance); // Sauvegarde de l'état de l'équipement
                }
            }
        }

        public MaintenanceCorrective GetById(int id)
        {
            return repo.GetById(id);
        }

        public List<MaintenanceCorrectiveDTO> GetAllDTOByResponsable(int idUser)
        {
            var list = repo.GetAllByResponsable(idUser);

            return list.Select(m => new MaintenanceCorrectiveDTO
            {
                MaintenanceId = m.MaintenanceId,
                Description = m.Description,
                DateCreation = m.DateCreation,
                Statut = m.Statut,
                EquipementNom = m.Equipement?.nom ?? "",
                ResponsableNom = m.Responsable?.nom ?? "",
                CoutPrevu = repo.CalculerCoutTotal(m.MaintenanceId)
            }).ToList();
        }

        public void Modifier(MaintenanceCorrective modifiee, List<PieceReservationDTO> nouvellesPieces)
        {
            var maintenance = repo.FindById(modifiee.MaintenanceId);
            if (maintenance == null || maintenance.Statut == "Terminée")
                throw new InvalidOperationException("Impossible de modifier une maintenance terminée ou inexistante.");

            maintenance.Description = modifiee.Description;
            maintenance.EquipementId = modifiee.EquipementId;

            var equipement = repo.GetEquipement(modifiee.EquipementId);
            if (equipement == null)
                throw new InvalidOperationException("Équipement introuvable.");
            maintenance.ResponsableId = equipement.responsableId;

            repo.SupprimerReservations(modifiee.MaintenanceId);

            foreach (var p in nouvellesPieces)
            {
                if (!repo.ExisteStockSuffisant(p.PieceId, p.Quantite))
                    throw new InvalidOperationException($"Stock insuffisant pour la pièce {p.PieceId}.");

                repo.AjouterReservationPiece(modifiee.MaintenanceId, p.PieceId, p.Quantite);
            }

            maintenance.Cout = repo.CalculerCoutTotal(modifiee.MaintenanceId);
            repo.ModifierMaintenance(maintenance);
        }

        public void Supprimer(int maintenanceId)
        {
            var maintenance = repo.GetById(maintenanceId);
            if (maintenance == null)
                return;

            if (maintenance.Statut == "Terminée")
                throw new InvalidOperationException("Impossible de supprimer une maintenance terminée.");

            repo.SupprimerMaintenance(maintenance);
        }
    }
}
