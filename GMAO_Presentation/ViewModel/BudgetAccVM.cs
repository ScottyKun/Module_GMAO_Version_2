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
    public class BudgetAccVM : INotifyPropertyChanged
    {
        private readonly BudgetService _service;
        public BindingList<BudgetDTO> Budgets { get; set; } = new BindingList<BudgetDTO>();

        public RelayCommand AjouterCommand { get; }
        public event Action OnDemandeAjout;

        //public event Action<BudgetDTO> OnDemandeModification;
        public event PropertyChangedEventHandler PropertyChanged;

        public BudgetAccVM()
        {
            _service = new BudgetService();
            AjouterCommand = new RelayCommand(() => OnDemandeAjout?.Invoke());
            ChargerBudgets();
        }

        public void ChargerBudgets()
        {
            Budgets.Clear();
            var data = _service.GetBudgetsParAnnee();
            foreach (var b in data)
                Budgets.Add(b);
        }



        protected void OnPropertyChanged(string nom) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nom));
    }
}
