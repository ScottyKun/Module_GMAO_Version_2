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
    public class AlerteAccVM : INotifyPropertyChanged
    {
        private readonly AlerteService _service = new AlerteService();

        public event PropertyChangedEventHandler PropertyChanged;

        public BindingList<AlerteDTO> Alertes { get; set; }
        public RelayCommand RefreshCommand { get; }

        public AlerteAccVM()
        {
            Alertes = new BindingList<AlerteDTO>(_service.GetRecentesEtNonTraitees());

            RefreshCommand = new RelayCommand(ChargerAlertes);

        }

        public void ChargerAlertes()
        {
            Alertes.Clear();
            foreach (var a in _service.GetByPriorite("Élevée"))
                Alertes.Add(a);
        }
    }
}
