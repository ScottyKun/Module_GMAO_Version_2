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
    public class GstUserUpVM : INotifyPropertyChanged
    {
        private readonly UserService _userService;

        public int IdUser { get; private set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Fonction { get; set; }
        public string Username { get; set; }
        public bool Statut { get; set; }

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }

        public event Action<string> OnError;
        public event Action OnClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public GstUserUpVM(UserDTO3 user)
        {
            _userService = new UserService();

            IdUser = user.IdUser;
            Nom = user.Nom;
            Prenom = user.Prenom;
            Email = user.Email;
            Tel = user.Tel;
            Fonction = user.Fonction;
            Username = user.Username;
            Statut = user.Statut;

            ModifierCommand = new RelayCommand(Modifier, PeutModifier);
            SupprimerCommand = new RelayCommand(Supprimer);
        }

        private bool PeutModifier()
        {
            return !string.IsNullOrWhiteSpace(Nom) &&
                   !string.IsNullOrWhiteSpace(Prenom) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Username);
        }

        private void Modifier()
        {
            try
            {
                _userService.ModifierInfos(new UserDTO3
                {
                    IdUser = IdUser,
                    Nom = Nom,
                    Prenom = Prenom,
                    Email = Email,
                    Tel = Tel,
                    Fonction = Fonction,
                    Username = Username,
                    Statut = Statut
                });

                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void Supprimer()
        {
            try
            {
                _userService.Supprimer(IdUser);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
