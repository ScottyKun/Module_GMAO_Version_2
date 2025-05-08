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
    public class WOCoAddVM : INotifyPropertyChanged
    {
        private readonly WorkOrderCoService _service;
        private readonly MaintenanceCorrectiveService _maintenanceService;
        private readonly int _idResponsable;

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand EnregistrerCommand { get; }

        public List<MaintenanceCorrectiveDTO> Maintenances { get; set; }

        private int _selectedMaintenanceId;
        public int SelectedMaintenanceId
        {
            get => _selectedMaintenanceId;
            set
            {
                _selectedMaintenanceId = value;
                MiseAJourDescription();
                OnPropertyChanged();
                //EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        private string _nom;
        public string Nom
        {
            get => _nom;
            set
            {
                _nom = value;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        private string _rapport;
        public string Rapport
        {
            get => _rapport;
            set
            {
                _rapport = value;
                OnPropertyChanged();
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateExecution { get; set; } = DateTime.Today;

        public event Action OnClose;
        public event Action<string> OnError;

        public WOCoAddVM(int idResponsable, int? maintenanceIdPreselectionnee = null, string descPreselectionnee = null)
        {
            _idResponsable = idResponsable;
            _service = new WorkOrderCoService();
            _maintenanceService = new MaintenanceCorrectiveService();

            Maintenances = _maintenanceService.GetAllDTOByResponsable(idResponsable)
                                           .Where(m => !_service.ExisteWorkOrderPourMaintenance(m.MaintenanceId))
                                           .ToList();


            if (maintenanceIdPreselectionnee.HasValue)
            {
                SelectedMaintenanceId = maintenanceIdPreselectionnee.Value;
                Description = descPreselectionnee ?? "";
            }
            else if (Maintenances.Any())
            {
                SelectedMaintenanceId = Maintenances.First().MaintenanceId;
            }

            EnregistrerCommand = new RelayCommand(Creer, PeutEnregistrer);
        }


        public void MiseAJourDescription()
        {
            if (SelectedMaintenanceId <= 0)
            {
                Description = "";
                return;
            }

            var maintenance = Maintenances.FirstOrDefault(m => m.MaintenanceId == SelectedMaintenanceId);
            Description = maintenance?.Description ?? "";
            OnPropertyChanged(nameof(Description));
        }




        private void Creer()
        {
            try
            {
                _service.CreerWorkOrderPourCorrective(SelectedMaintenanceId, Nom, DateExecution, Rapport);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private bool PeutEnregistrer()
        {
            return !string.IsNullOrWhiteSpace(Nom)
                && SelectedMaintenanceId > 0
                && !string.IsNullOrWhiteSpace(Rapport);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
