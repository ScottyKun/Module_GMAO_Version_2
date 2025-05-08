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
    public class InterventionAccVM : INotifyPropertyChanged
    {
        private readonly InterventionService _service;
        // private readonly int _idResponsable;

        public event PropertyChangedEventHandler PropertyChanged;

        public BindingList<InterventionDTO> Interventions { get; set; } = new BindingList<InterventionDTO>();

        public RelayCommand AjouterCommand { get; }

        public event Action OnDemandeAjout;

        public InterventionAccVM()
        {
            // _idResponsable = idResponsable;
            _service = new InterventionService();

            AjouterCommand = new RelayCommand(() => OnDemandeAjout?.Invoke());

            ChargerInterventions();
        }

        public void ChargerInterventions()
        {
            Interventions.Clear();
            var data = _service.GetAllPourUtilisateur();
            foreach (var i in data)
                Interventions.Add(i);
        }

        private void OnPropertyChanged(string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
