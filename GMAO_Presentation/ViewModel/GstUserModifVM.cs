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
    public class GstUserModifVM : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        public int IdUser { get; private set; }

        private string _nouveauMotDePasse;

        public string NouveauMotDePasse
        {
            get => _nouveauMotDePasse;
            set
            {
                _nouveauMotDePasse = value;
                OnPropertyChanged();
                ModifierCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand ModifierCommand { get; }

        public event Action<string> OnError;
        public event Action OnClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public GstUserModifVM(int idUser)
        {
            _userService = new UserService();
            IdUser = idUser;

            ModifierCommand = new RelayCommand(Modifier, PeutModifier);
        }

        private bool PeutModifier() => !string.IsNullOrWhiteSpace(NouveauMotDePasse);

        private void Modifier()
        {
            try
            {
                _userService.ModifierMotDePasse(IdUser, NouveauMotDePasse);
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
