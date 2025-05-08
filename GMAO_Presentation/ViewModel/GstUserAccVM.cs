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
    public class GstUserAccVM : INotifyPropertyChanged
    {
        private readonly UserService _userService;

        private UserDTO3 _selectedUser;
        private string _recherche;

        public BindingList<UserDTO3> Users { get; set; } = new BindingList<UserDTO3>();

        public RelayCommand RechercherCommand { get; }
        public RelayCommand ActualiserCommand { get; }
        public RelayCommand ModifierMotDePasseCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action<UserDTO3> OnDemandeModifierUser;
        public event Action<UserDTO3> OnDemandeModifierMotDePasse;



        public UserDTO3 SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                ModifierMotDePasseCommand.RaiseCanExecuteChanged();
            }
        }

        public string Recherche
        {
            get => _recherche;
            set
            {
                _recherche = value;
                OnPropertyChanged();
                RechercherCommand.RaiseCanExecuteChanged();
            }
        }

        public GstUserAccVM()
        {
            _userService = new UserService();

            RechercherCommand = new RelayCommand(Rechercher, PeutRechercher);
            ActualiserCommand = new RelayCommand(Actualiser);
            ModifierMotDePasseCommand = new RelayCommand(ModifierMotDePasse, PeutModifierMotDePasse);

            Actualiser();
        }

        private void Rechercher()
        {
            if (!string.IsNullOrWhiteSpace(Recherche))
            {
                var resultats = _userService.Rechercher(Recherche);
                Users.Clear();
                foreach (var user in resultats)
                    Users.Add(user);
            }
        }

        private bool PeutRechercher() => !string.IsNullOrWhiteSpace(Recherche);

        public void Actualiser()
        {
            Recherche = string.Empty;
            Users.Clear();
            var allUsers = _userService.Lister();
            foreach (var user in allUsers)
                Users.Add(user);
        }

        private void ModifierMotDePasse()
        {
            if (SelectedUser != null)
            {
                OnDemandeModifierMotDePasse?.Invoke(SelectedUser);
            }
        }

        private bool PeutModifierMotDePasse() => SelectedUser != null;

        public void DemanderModifierUser()
        {
            if (SelectedUser != null)
                OnDemandeModifierUser?.Invoke(SelectedUser);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
