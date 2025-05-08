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
    public class GestionProfilViewModel
    {
        private readonly GestionProfilService gps;
        private UserDTO5 _user;
        public event Action<string, string> OnShowNotification;

        public event PropertyChangedEventHandler PropertyChanged;
        //les variables pour les elts de notre vue
        public string Nom
        {
            get => _user.Nom;
            set { _user.Nom = value; OnPropertyChanged(); UpdateCommand.RaiseCanExecuteChanged(); }
        }

        public string Prenom
        {
            get => _user.Prenom;
            set { _user.Prenom = value; OnPropertyChanged(); UpdateCommand.RaiseCanExecuteChanged(); }
        }

        public string Telephone
        {
            get => _user.Telephone;
            set { _user.Telephone = value; OnPropertyChanged(); UpdateCommand.RaiseCanExecuteChanged(); }
        }

        public string Email
        {
            get => _user.Email;
            set { _user.Email = value; OnPropertyChanged(); UpdateCommand.RaiseCanExecuteChanged(); }
        }

        public string Username
        {
            get => _user.Username;
            set { _user.Username = value; OnPropertyChanged(); UpdateCommand.RaiseCanExecuteChanged(); }
        }


        public bool CanRegister => !string.IsNullOrWhiteSpace(Username) &&
              !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Prenom) && !string.IsNullOrWhiteSpace(Telephone) &&
            !string.IsNullOrWhiteSpace(Fonction);


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //UpdateCommand.EvaluateCanExecute();
        }

        public string Fonction => _user.Fonction;

        public RelayCommand UpdateCommand { get; }

        public GestionProfilViewModel(int userId)
        {
            gps = new GestionProfilService();
            _user = new UserDTO5();
            LoadUser(userId);
            UpdateCommand = new RelayCommand(SaveChanges, () => _user != null && CanRegister);
        }

        private void SaveChanges()
        {
            gps.Update(_user);
            OnShowNotification?.Invoke("Succès", "Mise à jour effectuée!");
            OnPropertyChanged(nameof(UpdateCommand));
        }

        private void LoadUser(int userId)
        {
            _user = gps.GetUserById(userId) ?? new UserDTO5();
            OnPropertyChanged(nameof(Nom));
            OnPropertyChanged(nameof(Prenom));
            OnPropertyChanged(nameof(Telephone));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Fonction));

        }
    }
}
