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
    public class MaintenanceCoAccueilViewModel : INotifyPropertyChanged
    {
        private readonly MaintenanceCorrectiveService _service;
        private int _idResponsable;

        public event PropertyChangedEventHandler PropertyChanged;

        public BindingList<MaintenanceCorrectiveDTO> Maintenances { get; set; } = new BindingList<MaintenanceCorrectiveDTO>();

        public RelayCommand AjouterCommand { get; }

        public MaintenanceCoAccueilViewModel(int idResponsable)
        {
            _idResponsable = idResponsable;
            _service = new MaintenanceCorrectiveService();

            ChargerMaintenances();
            AjouterCommand = new RelayCommand(() => OnDemandeAjout?.Invoke());
        }

        public void ChargerMaintenances()
        {
            Maintenances.Clear();
            var liste = _service.GetAllDTOByResponsable(_idResponsable);
            foreach (var m in liste)
                Maintenances.Add(m);
        }

        public event Action OnDemandeAjout;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
