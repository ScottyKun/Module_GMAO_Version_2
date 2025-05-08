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
    public class WOIAccVM : INotifyPropertyChanged
    {
        private readonly WorkOrderPlanifieeService _service;
        //private readonly int _idResponsable;

        public BindingList<WorkOrderDTO2> WorkOrders { get; set; } = new BindingList<WorkOrderDTO2>();

        public RelayCommand AjouterCommand { get; }

        public event Action OnDemandeAjout;
        public event PropertyChangedEventHandler PropertyChanged;

        public WOIAccVM()
        {
            //_idResponsable = idResponsable;
            _service = new WorkOrderPlanifieeService();

            ChargerWorkOrders();
            AjouterCommand = new RelayCommand(() => OnDemandeAjout?.Invoke());
        }

        public void ChargerWorkOrders()
        {
            WorkOrders.Clear();
            var liste = _service.GetAllDTOByUtilisateur();
            foreach (var w in liste)
                WorkOrders.Add(w);
        }

        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
