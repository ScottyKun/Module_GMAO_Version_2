using GMAO_Business.DTOs;
using GMAO_Business.Services;
using GMAO_Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class EquipementModifViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly EquipementService _service;

        public EquipementDTO3 Equipement { get; private set; }

        public List<CategoryDTO> Categories { get; private set; }
        public List<UserDTO> Responsables { get; private set; }
        public List<TeamInfo> Equipes { get; private set; }

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }

        public EquipementModifViewModel(int idEquipement)
        {
            _service = new EquipementService();
            Equipement = _service.GetById3(idEquipement);

            Categories = _service.GetCategories();
            Responsables = _service.GetResponsable();
            Equipes = _service.GetTeams();

            ModifierCommand = new RelayCommand(Modifier);
            SupprimerCommand = new RelayCommand(Supprimer);
        }

        private void Supprimer()
        {
            _service.Delete(Equipement.Id);
        }

        private void Modifier()
        {
            _service.Update(new EquipementDTO3
            {
                Id = Equipement.Id,
                Nom = Equipement.Nom,
                CategorieId = Equipement.CategorieId,
                ResponsableId = Equipement.ResponsableId,
                MaintenanceTeamId = Equipement.MaintenanceTeamId,
                DateAchat = Equipement.DateAchat,
                DateFinGarantie = Equipement.DateFinGarantie,
                Statut = Equipement.Statut,
                Commentaires = Equipement.Commentaires
            });

        }
    }
}
