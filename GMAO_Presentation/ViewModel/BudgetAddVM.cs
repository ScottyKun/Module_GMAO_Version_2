using GMAO_Business.DTOs;
using GMAO_Business.Entities;
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
    public class BudgetAddVM : INotifyPropertyChanged
    {
        private readonly BudgetService _service;

        public string Nom { get; set; }
        public int Annee { get; set; }
        public decimal Montant { get; set; }
        public DateTime DateAjout { get; set; } = DateTime.Today;

        public RelayCommand AjouterCommand { get; }

        public event Action<string> OnError;
        public event Action OnClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public BudgetAddVM()
        {
            _service = new BudgetService();
            AjouterCommand = new RelayCommand(Ajouter, PeutAjouter);
        }

        private bool PeutAjouter() =>
       !string.IsNullOrWhiteSpace(Nom) && Annee > 2000 && Montant > 0;


        private void Ajouter()
        {
            try
            {
                var budgetDto = new BudgetDTO
                {
                    Nom = Nom,
                    Annee = Annee,
                    Montant = Montant,
                    DateCreation = DateAjout,
                    ResponsableId = UserContext.IdUser
                };

                _service.Ajouter(budgetDto);
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
