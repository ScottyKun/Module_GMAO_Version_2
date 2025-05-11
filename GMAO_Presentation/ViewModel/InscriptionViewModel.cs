using GMAO_Business.Services;
using GMAO_Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class InscriptionViewModel : INotifyPropertyChanged
    {
        private readonly InscriptionService insService;
        private string _username;
        private string _password;
        private string _role;
        private string _email;

        //evenements pour les notifiactions et la redirection
        public event Action<string, string> OnShowNotification;
        public event Action OnNavigate;


        //instanciation des variables pour stocker nos donnees
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                Inscomand.RaiseCanExecuteChanged();
            }
        }



        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                Inscomand.RaiseCanExecuteChanged();
            }
        }
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged();
                Inscomand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                Inscomand.RaiseCanExecuteChanged();
            }
        }

        // la validation des champs
        public bool CanRegister => !string.IsNullOrWhiteSpace(Username) &&
              !string.IsNullOrWhiteSpace(Password) &&
              !string.IsNullOrWhiteSpace(Email);


        //Liste pour remplir notre combox avec les roles
        public ObservableCollection<string> Roles { get; } = new ObservableCollection<string> { "Technicien", "Responsable", "Administrateur" };

        //commande pour gerer l'inscription
        public RelayCommand Inscomand { get; }

        //commande por la navigation
        public RelayCommand navCommand;

        public InscriptionViewModel(InscriptionService service)
        {
            insService = service;
            Inscomand = new RelayCommand(ExecuteInscription, () => CanRegister);
        }


        //Methode pour la gestion de la logique d'inscription
        private void ExecuteInscription()
        {
            var result = insService.Register(Username, Password, Email, Role);

            if (result == "SUCCESS")
            {
                OnShowNotification?.Invoke("Succès", "Inscription réussie !");
                OnNavigate?.Invoke();
            }
            else
            {
                OnShowNotification?.Invoke("Erreur", result);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Inscomand.EvaluateCanExecute();
        }



    }
}
