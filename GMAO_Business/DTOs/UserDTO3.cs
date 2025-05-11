using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GMAO_Business.DTOs
{
   

    public class UserDTO3 : INotifyPropertyChanged
    {
        private int _idUser;
        public int IdUser
        {
            get => _idUser;
            set
            {
                if (_idUser != value)
                {
                    _idUser = value;
                    OnPropertyChanged(nameof(IdUser));
                }
            }
        }

        private string _nom;
        public string Nom
        {
            get => _nom;
            set
            {
                if (_nom != value)
                {
                    _nom = value;
                    OnPropertyChanged(nameof(Nom));
                }
            }
        }

        private string _prenom;
        public string Prenom
        {
            get => _prenom;
            set
            {
                if (_prenom != value)
                {
                    _prenom = value;
                    OnPropertyChanged(nameof(Prenom));
                }
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private string _tel;
        public string Tel
        {
            get => _tel;
            set
            {
                if (_tel != value)
                {
                    _tel = value;
                    OnPropertyChanged(nameof(Tel));
                }
            }
        }

        private string _fonction;
        public string Fonction
        {
            get => _fonction;
            set
            {
                if (_fonction != value)
                {
                    _fonction = value;
                    OnPropertyChanged(nameof(Fonction));
                }
            }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private bool _statut;
        public bool Statut
        {
            get => _statut;
            set
            {
                if (_statut != value)
                {
                    _statut = value;
                    OnPropertyChanged(nameof(Statut));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string nomProp) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
    }


    public class UserDTO5
    {
        public int IdUser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Fonction { get; set; }
    }

}
