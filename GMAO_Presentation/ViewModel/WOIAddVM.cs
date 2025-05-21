using GMAO_Business.DTOs;
using GMAO_Business.Services;
using GMAO_Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class WOIAddVM : INotifyPropertyChanged
    {
        private readonly WorkOrderPlanifieeService _woService;
        private readonly InterventionService _interventionService;

        public List<InterventionLightDTO> InterventionsDispo { get; set; }

        public int SelectedInterventionId { get; set; }

        public string Nom { get; set; } = "";
        public DateTime DateExecution { get; set; } = DateTime.Now;
        public string Rapport { get; set; } = "";

        public string DescriptionIntervention { get; set; } = "";

        public RelayCommand AjouterCommand { get; }

        public event Action OnClose;
        public event Action<string> OnError;
        public event PropertyChangedEventHandler PropertyChanged;

        public WOIAddVM(int idResponsable, int? interventionId = null, string desc = null)
        {
            _woService = new WorkOrderPlanifieeService();
            _interventionService = new InterventionService();

            InterventionsDispo = _interventionService.GetInterventionsDisponiblesPourWO(idResponsable);

            if (interventionId.HasValue)
            {
                SelectedInterventionId = interventionId.Value;
                DescriptionIntervention = desc ?? "";
            }
            else if (InterventionsDispo.Any())
            {
                SelectedInterventionId = InterventionsDispo.First().Id;
                MiseAJourDescription();
            }

            AjouterCommand = new RelayCommand(Creer, PeutAjouter);
        }

        public void MiseAJourDescription()
        {
            var i = InterventionsDispo.FirstOrDefault(x => x.Id == SelectedInterventionId);
            DescriptionIntervention = i?.Description ?? "";
            OnPropertyChanged(nameof(DescriptionIntervention));
        }

        private void Creer()
        {
            try
            {
                _woService.Creer(SelectedInterventionId, Nom, DateExecution, Rapport);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private bool PeutAjouter()
        {
            return SelectedInterventionId > 0 && !string.IsNullOrWhiteSpace(Nom);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
