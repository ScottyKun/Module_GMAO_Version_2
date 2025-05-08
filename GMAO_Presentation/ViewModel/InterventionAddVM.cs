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
    public class InterventionAddVM : INotifyPropertyChanged
    {
        private readonly InterventionService _service;
        private readonly MaintenancePlanService _maintenanceService;

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<string> OnError;
        public event Action OnClose;

        public RelayCommand EnregistrerCommand { get; }

        private string _nom;
        public string Name
        {
            get => _nom;
            set
            {
                if (_nom != value)
                {
                    _nom = value;
                    OnPropertyChanged(nameof(Name));
                    EnregistrerCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Statut { get; } = "New";
        public DateTime Date { get; set; } = DateTime.Today;

        public List<MaintenanceLightDTO> MaintenancesDispo { get; set; }
        private int? _selectedMaintenanceId;
        public int? SelectedMaintenanceId
        {
            get => _selectedMaintenanceId;
            set
            {
                if (_selectedMaintenanceId != value)
                {
                    _selectedMaintenanceId = value;
                    OnPropertyChanged(nameof(SelectedMaintenanceId));
                    //EnregistrerCommand.RaiseCanExecuteChanged();
                    MiseAJourDescription();
                }
            }
        }

        private string _description;
        public string DescriptionMaintenance
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        // private bool _initialisationAuto = true;

        public InterventionAddVM(int? maintenanceIdPreselectionnee = null, string description = null)
        {
            _service = new InterventionService();
            _maintenanceService = new MaintenancePlanService();

            EnregistrerCommand = new RelayCommand(Enregistrer, PeutEnregistrer);

            MaintenancesDispo = _maintenanceService.GetMaintenancesDisponiblesPourIntervention();

            if (maintenanceIdPreselectionnee.HasValue)
            {
                SelectedMaintenanceId = maintenanceIdPreselectionnee.Value;
                DescriptionMaintenance = description ?? "";
                MiseAJourDescription();
            }
            else if (MaintenancesDispo.Any())
            {
                SelectedMaintenanceId = MaintenancesDispo.First().MaintenanceId;
                MiseAJourDescription();
            }
            // _initialisationAuto = false;
        }

        private bool PeutEnregistrer()
        {
            return SelectedMaintenanceId != null && !string.IsNullOrWhiteSpace(Name);
        }

        public void MiseAJourDescription()
        {
            if (SelectedMaintenanceId == null || SelectedMaintenanceId <= 0)
            {
                DescriptionMaintenance = "";
                return;
            }


            var selected = MaintenancesDispo.FirstOrDefault(x => x.MaintenanceId == SelectedMaintenanceId);
            DescriptionMaintenance = selected?.Description ?? "";
            OnPropertyChanged(nameof(DescriptionMaintenance));
        }


        private void Enregistrer()
        {
            try
            {
                if (SelectedMaintenanceId == null)
                    throw new InvalidOperationException("Sélection de maintenance invalide");

                // Vérif si une intervention à cette date existe déjà
                if (_service.ExisteInterventionPour(SelectedMaintenanceId.Value, Date))
                {
                    OnError?.Invoke("Une intervention existe déjà pour cette date.");
                    return;
                }

                var dto = new InterventionCreationDTO
                {
                    Nom = Name,
                    DatePrevue = Date,
                    MaintenancePlanifieeId = SelectedMaintenanceId.Value
                };

                _service.Creer(dto);

                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                OnError?.Invoke("Erreur : " + message);
                Console.WriteLine($"{message}");
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
