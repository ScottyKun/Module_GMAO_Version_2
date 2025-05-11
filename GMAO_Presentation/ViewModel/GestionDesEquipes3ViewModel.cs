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

namespace GMAO_Presentation.ViewModel
{
    public class GestionDesEquipes3ViewModel : INotifyPropertyChanged
    {
        private readonly EquipeService _equipeService;

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnEquipeAjoutee;

        public string NomEquipe { get; set; }
        public bool Statut { get; set; }

        public List<UserDTO> MembresDisponibles { get; private set; }
        public List<UserDTO> MembresSelectionnes { get; private set; } = new List<UserDTO>();

        public List<UserDTO> ChefsDisponibles => MembresSelectionnes;

        private UserDTO _chefSelectionne;
        public UserDTO ChefSelectionne
        {
            get => _chefSelectionne;
            set
            {
                _chefSelectionne = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AjouterEquipeCommand { get; }

        public GestionDesEquipes3ViewModel()
        {
            _equipeService = new EquipeService();
            ChargerMembresDisponibles();

            AjouterEquipeCommand = new RelayCommand(AjouterEquipe, () => CanAjouterEquipe);
        }

        private bool CanAjouterEquipe => !string.IsNullOrWhiteSpace(NomEquipe) &&
            ChefSelectionne != null && MembresSelectionnes.Any();

        private void AjouterEquipe()
        {

            int id;
            id = ChefSelectionne.idUser;


            _equipeService.AjouterEquipe(NomEquipe, id, Statut, MembresSelectionnes.Select(u => u.idUser).ToList());
            OnEquipeAjoutee?.Invoke();
        }

        private void ChargerMembresDisponibles()
        {
            MembresDisponibles = _equipeService.GetAllUtilisateurs();
            OnPropertyChanged();
            //AjouterEquipeCommand.RaiseCanExecuteChanged();
        }

        public void MettreAJourMembresSelectionnes(List<int> idsSelectionnes)
        {
            MembresSelectionnes = MembresDisponibles.Where(u => idsSelectionnes.Contains(u.idUser)).ToList();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChefsDisponibles)));
            // AjouterEquipeCommand.RaiseCanExecuteChanged();

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //AjouterEquipeCommand.EvaluateCanExecute();
        }

    }
}
