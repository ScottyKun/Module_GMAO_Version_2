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

        public void CreerMaintenanceCorrective(MaintenanceCorrectiveDTO2 dto, List<PieceReservationDTO> pieces)
        {
            if (!maintenanceService.PeutCreerMaintenance(dto.EquipementId, dto.DateCreation.Date, dto.DateCreation.Date))
                throw new InvalidOperationException("Une maintenance existe déjà pour cette période.");

            foreach (var p in pieces)
            {
                if (!repo.ExisteStockSuffisant(p.PieceId, p.Quantite))
                    throw new InvalidOperationException($"Stock insuffisant pour la pièce {p.PieceId}.");
            }

            var maintenance = new MaintenanceCorrective
            {
                Description = dto.Description,
                DateCreation = dto.DateCreation,
                EquipementId = dto.EquipementId,
                ResponsableId = dto.ResponsableId,
                Statut = dto.Statut,
                EquipeId = dto.EquipeId
            };

            repo.AjouterMaintenance(maintenance);

            foreach (var p in pieces)
                repo.AjouterReservationPiece(maintenance.MaintenanceId, p.PieceId, p.Quantite);

            maintenance.Cout = CalculerCoutPrevu(maintenance.MaintenanceId);
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

        public MaintenanceCorrectiveDTO2 GetById(int id)
        {
            var m = repo.GetById2(id);
            if (m == null) return null;

            return new MaintenanceCorrectiveDTO2
            {
                MaintenanceId = m.MaintenanceId,
                Description = m.Description,
                Statut = m.Statut,
                EquipementId = m.EquipementId,
                ResponsableId = m.ResponsableId,
                DateCreation = m.DateCreation,
                EquipeId = m.EquipeId,
                ResponsableNom = m.Responsable?.nom ?? "",
                EquipeMaintenanceNom = m.Equipement?.maintenanceTeam?.nom ?? "-",
                PiecesReservees = m.PiecesReservees?.Select(p => new PieceReservationDTO
                {
                    PieceId = p.PieceId,
                    Quantite = p.Quantite
                }).ToList()
            };
        }

        public MaintenanceCorrectiveDTO GetById2(int id)
        {
            var m = repo.GetById2(id);
            if (m == null) return null;

            return new MaintenanceCorrectiveDTO
            {
                MaintenanceId = m.MaintenanceId,
                Description = m.Description,
                Statut = m.Statut,
                DateCreation = m.DateCreation,
                ResponsableNom = m.Responsable?.nom ?? "",
                EquipementNom = m.Equipement?.nom ?? "",
                CoutPrevu = CalculerCoutPrevu(m.MaintenanceId)

            };
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
                CoutPrevu = CalculerCoutPrevu(m.MaintenanceId)
            }).ToList();
        }

        public void Modifier(MaintenanceCorrectiveDTO2 dto, List<PieceReservationDTO> nouvellesPieces)
        {
            var maintenance = repo.FindById(dto.MaintenanceId);
            if (maintenance == null || maintenance.Statut == "Terminée" || maintenance.Statut=="Echec")
                throw new InvalidOperationException("Impossible de modifier une maintenance terminée ou inexistante.");

            maintenance.Description = dto.Description;
            maintenance.EquipementId = dto.EquipementId;

            var equipement = repo.GetEquipement(dto.EquipementId);
            if (equipement == null)
                throw new InvalidOperationException("Équipement introuvable.");

            maintenance.ResponsableId = equipement.responsableId;

            repo.SupprimerReservations(dto.MaintenanceId);

            foreach (var p in nouvellesPieces)
            {
                if (!repo.ExisteStockSuffisant(p.PieceId, p.Quantite))
                    throw new InvalidOperationException($"Stock insuffisant pour la pièce {p.PieceId}.");

                repo.AjouterReservationPiece(dto.MaintenanceId, p.PieceId, p.Quantite);
            }

            maintenance.Cout = CalculerCoutPrevu(dto.MaintenanceId);
            repo.ModifierMaintenance(maintenance);
        }

        public void Supprimer(int maintenanceId)
        {
            var maintenance = repo.GetById3(maintenanceId);
            if (maintenance == null)
                return;

            if (maintenance.Statut == "Terminée" || maintenance.Statut=="Echec")
                throw new InvalidOperationException("Impossible de supprimer une maintenance terminée.");

            repo.SupprimerMaintenance(maintenance);
        }
    }
}
