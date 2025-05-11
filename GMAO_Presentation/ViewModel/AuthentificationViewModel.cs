using GMAO_Business.DTOs;
using GMAO_Business.Entities;
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
    public class AuthentificationVM : INotifyPropertyChanged
    {
        private readonly AuthentificationService _auth;
        private string _username;
        private string _password;
        private int _id;

        // Événements pour la Vue (Notifications et Redirections)
        public event Action<string, string> OnShowNotification;
        public event Action<int, string> OnNavigateToDashboard;

        public event PropertyChangedEventHandler PropertyChanged;

        // variables pour contenir les informations de notre vue
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                AuthCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                AuthCommand.RaiseCanExecuteChanged();
            }
        }

        public int Id
        {
            get => _id;
            private set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public bool CanRegister => !string.IsNullOrWhiteSpace(Username) &&
              !string.IsNullOrWhiteSpace(Password);

        public RelayCommand AuthCommand { get; }

        public AuthentificationVM()
        {
            _auth = new AuthentificationService();
            AuthCommand = new RelayCommand(Login, () => CanRegister);
        }

        //utilisation de notre logique d'authentification
        private void Login()
        {
            try
            {
                UserDTO4 user = _auth.Authentifier(Username, Password);


                if (user != null)
                {
                     UserContext.IdUser = user.IdUser;
                    UserContext.Role = user.Fonction;

                    Id = user.IdUser;
                    OnShowNotification?.Invoke("Succès", "Connexion réussie !");
                    OnNavigateToDashboard?.Invoke(Id, user.Fonction);
                }
                else
                {
                    OnShowNotification?.Invoke("Erreur", "Nom d'utilisateur ou mot de passe incorrect.");
                }
            }
            catch (InvalidOperationException ex)
            {
                OnShowNotification?.Invoke("Erreur", ex.Message);
            }
            catch (Exception ex)
            {
                OnShowNotification?.Invoke("Erreur", "Une erreur est survenue : " + ex.Message);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            AuthCommand.EvaluateCanExecute();
        }



    }
}
