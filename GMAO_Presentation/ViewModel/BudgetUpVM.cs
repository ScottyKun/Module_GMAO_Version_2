using GMAO_Business.DTOs;
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
    public class BudgetUpVM : INotifyPropertyChanged
    {
        private readonly BudgetService _service;
        public int BudgetId { get; private set; }

        public string Nom { get; set; }
        public int Annee { get; set; }
        public decimal Montant { get; set; }
        public DateTime DateAjout { get; private set; }

        public RelayCommand ModifierCommand { get; }

        public event Action<string> OnError;
        public event Action OnClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public BudgetUpVM(BudgetDTO dto)
        {
            _service = new BudgetService();

            BudgetId = dto.BudgetId;
            Nom = dto.Nom;
            Annee = dto.Annee;
            Montant = dto.Montant;
            DateAjout = dto.DateCreation;

            ModifierCommand = new RelayCommand(Modifier, PeutModifier);
        }

        private bool PeutModifier() =>
        !string.IsNullOrWhiteSpace(Nom) && Annee > 2000 && Montant > 0;

        private void Modifier()
        {
            try
            {
                _service.Modifier(new BudgetDTO
                {
                    BudgetId = this.BudgetId,
                    Annee = this.Annee,
                    Montant = this.Montant,
                    Nom = this.Nom
                });

                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
