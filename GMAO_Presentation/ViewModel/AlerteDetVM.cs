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
    public class AlerteDetVM : INotifyPropertyChanged
    {
        private readonly AlerteService _service = new AlerteService();

        public int AlerteId { get; set; }
        public string Libelle { get; set; }
        public string Message { get; set; }
        public string Priorite { get; set; }
        public DateTime DateCreation { get; set; }
        public bool Terminee { get; set; }

        public RelayCommand EnregistrerCommand { get; }
        public RelayCommand SupprimerCommand { get; }

        public event Action OnClose;
        public event Action<string> OnError;
        public event PropertyChangedEventHandler PropertyChanged;

        public AlerteDetVM(int alerteId)
        {
            var a = _service.GetById(alerteId);
            if (a == null)
            {
                OnError?.Invoke("Alerte introuvable.");
                return;
            }

            AlerteId = a.Id;
            Libelle = a.Libelle;
            Message = a.Message;
            Priorite = a.Priorite;
            DateCreation = a.DateCreation;
            Terminee = a.Terminee;

            EnregistrerCommand = new RelayCommand(Enregistrer);
            SupprimerCommand = new RelayCommand(Supprimer);
        }

        private void Enregistrer()
        {
            try
            {
                _service.MettreAJourStatut(AlerteId, Terminee);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void Supprimer()
        {
            _service.Supprimer(AlerteId);
            OnClose?.Invoke();
        }
    }
}
