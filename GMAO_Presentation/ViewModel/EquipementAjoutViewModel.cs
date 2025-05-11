using GMAO_Business.DTOs;
using GMAO_Business.Services;
using GMAO_Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAO_Presentation.ViewModel
{
    public class EquipementAjoutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly EquipementService _service;
        public List<CategoryDTO> Categories { get; set; }
        public List<UserDTO> Responsables { get; set; }
        public List<TeamInfo> Equipes { get; set; }

        private string _nom;
        private string _commentaires;

        public string Nom
        {
            get => _nom;
            set
            {
                _nom = value;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        private int? _categorieId;
        public int? CategorieId
        {
            get => _categorieId;
            set
            {
                _categorieId = value;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        private int? _responsableId;
        public int? ResponsableId
        {
            get => _responsableId;
            set
            {
                _responsableId = value;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        private int? _maintenanceTeamId;
        public int? MaintenanceTeamId
        {
            get => _maintenanceTeamId;
            set
            {
                _maintenanceTeamId = value;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        private CategoryDTO _selectedCategorie;
        public CategoryDTO SelectedCategorie
        {
            get => _selectedCategorie;
            set
            {
                _selectedCategorie = value;
                CategorieId = value?.id;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        private UserDTO _selectedResponsable;
        public UserDTO SelectedResponsable
        {
            get => _selectedResponsable;
            set
            {
                _selectedResponsable = value;
                ResponsableId = value?.idUser;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        private TeamInfo _selectedEquipe;
        public TeamInfo SelectedEquipe
        {
            get => _selectedEquipe;
            set
            {
                _selectedEquipe = value;
                MaintenanceTeamId = value?.Id;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }


        public DateTime DateAchat { get; set; } = DateTime.Today;
        public bool Statut { get; set; }
        public DateTime DateFinGarantie { get; set; } = DateTime.Today;

        public string Commentaires
        {
            get => _commentaires;
            set
            {
                _commentaires = value;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand EnregistrerCommand { get; }

        public EquipementAjoutViewModel()
        {
            _service = new EquipementService();
            Categories = _service.GetCategories();
            Responsables = _service.GetResponsable();
            Equipes = _service.GetTeams();

            EnregistrerCommand = new RelayCommand(Ajouter, PeutAjouter);
        }

        private bool PeutAjouter()
        {
            return !string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Commentaires)
           && CategorieId.HasValue
           && ResponsableId.HasValue
           && MaintenanceTeamId.HasValue;
        }

        private void Ajouter()
        {
            if (!CategorieId.HasValue || !ResponsableId.HasValue || !MaintenanceTeamId.HasValue)
            {
                MessageBox.Show("Veuillez sélectionner une catégorie, un responsable et une équipe.", "Champs manquants", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var equipementDto = new EquipementDTO3
            {
                Nom = Nom,
                CategorieId = CategorieId.Value,
                ResponsableId = ResponsableId.Value,
                MaintenanceTeamId = MaintenanceTeamId.Value,
                DateAchat = DateAchat,
                DateFinGarantie = DateFinGarantie,
                Statut = Statut,
                Commentaires = Commentaires
            };
            _service.Add(equipementDto);

        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //EnregistrerCommand.EvaluateCanExecute();
        }
    }
}
