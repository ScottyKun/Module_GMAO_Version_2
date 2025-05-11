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
    public class MaintenancePlanAccVM : INotifyPropertyChanged
    {
        private readonly MaintenancePlanService _service;
        private int _idResponsable;

        public event PropertyChangedEventHandler PropertyChanged;

        public BindingList<MaintenancePlanifieeDTO> Maintenances { get; set; } = new BindingList<MaintenancePlanifieeDTO>();
        public RelayCommand AjouterCommand { get; }

        public event Action OnDemandeAjout;

        public MaintenancePlanAccVM(int idResponsable)
        {
            _idResponsable = idResponsable;
            _service = new MaintenancePlanService();
            AjouterCommand = new RelayCommand(() => OnDemandeAjout?.Invoke());
            ChargerMaintenances();
        }

        public void ChargerMaintenances()
        {
            Maintenances.Clear();
            var liste = _service.GetAllDTOByResponsable(_idResponsable);
            foreach (var m in liste)
                Maintenances.Add(m);
        }

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
