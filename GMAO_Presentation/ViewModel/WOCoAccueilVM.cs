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
    public class WOCoAccueilVM : INotifyPropertyChanged
    {
        private readonly WorkOrderCoService _service;
      

        public BindingList<WorkOrderDTO> WorkOrders { get; set; } = new BindingList<WorkOrderDTO>();

        public RelayCommand AjouterCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnAjoutRequested;

        public WOCoAccueilVM()
        {
           
            _service = new WorkOrderCoService();

            AjouterCommand = new RelayCommand(() => OnAjoutRequested?.Invoke());

            ChargerWorkOrders();
        }

        public void ChargerWorkOrders()
        {
            WorkOrders.Clear();
            var liste = _service.GetAllByUtilisateur();
            foreach (var wo in liste)
                WorkOrders.Add(wo);
        }

        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
