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
    public class MaintenancePlanAddVM : INotifyPropertyChanged
    {
        private readonly MaintenancePlanService _service;
        private readonly EquipementService _equipementService;
        private readonly int _idResponsable;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _description = "";
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }

        public string Statut { get; } = "Nouvelle";

        public DateTime DateDebut { get; set; } = DateTime.Today;
        public DateTime DateFin { get; set; } = DateTime.Today.AddDays(30);

        private int _recurrence = 7;
        public int Recurrence
        {
            get => _recurrence;
            set
            {
                _recurrence = value;
                OnPropertyChanged(nameof(Recurrence));
                EnregistrerCommand.RaiseCanExecuteChanged();
            }
        }


        public List<EquipementLightDTO> Equipements { get; set; }
        private int? _equipementId;
        public int? EquipementId
        {
            get => _equipementId;
            set
            {
                _equipementId = value;
                OnPropertyChanged(nameof(EquipementId));
                EnregistrerCommand.RaiseCanExecuteChanged();

                if (value.HasValue)
                    ChargerInfos(value.Value);
            }
        }


        public string NomResponsable { get; set; }
        public string NomEquipe { get; set; }

        public RelayCommand EnregistrerCommand { get; }

        public event Action OnClose;
        public event Action<string> OnError;

        private int idE;

        private int equiId;

        public MaintenancePlanAddVM(int idResponsable)
        {
            _idResponsable = idResponsable;
            _service = new MaintenancePlanService();
            _equipementService = new EquipementService();

            Equipements = _equipementService.GetEquipementsLightByResponsable(idResponsable);
            EnregistrerCommand = new RelayCommand(Enregistrer, PeutEnregistrer);
        }


        public void ChargerInfos(int equipementId)
        {
            var e = _equipementService.GetById(equipementId);

            if (e == null)
            {
                NomResponsable = "-";
                NomEquipe = "-";
                return;
            }

            NomResponsable = e.NomResponsable;
            NomEquipe = e.NomEquipe;
            idE = e.MaintenanceTeamId;
            equiId = e.Id;

            OnPropertyChanged(nameof(NomResponsable));
            OnPropertyChanged(nameof(NomEquipe));
        }


        private bool PeutEnregistrer()
        {
            return EquipementId != null
                && !string.IsNullOrWhiteSpace(Description)
                && Recurrence > 0;
        }


        private void Enregistrer()
        {
            if (!new MaintenanceService().PeutCreerMaintenance(equiId, DateDebut, DateFin))
            {
                OnError?.Invoke("Impossible : une maintenance existe déjà pour cette période.");
                return;
            }

            try
            {
                var dto = new MaintenancePlanifieeDTO2
                {
                    Description = Description,
                    Statut = Statut,
                    DateDebut = DateDebut,
                    DateFin = DateFin,
                    RecurrenceJours = Recurrence,
                    ResponsableId = _idResponsable,
                    EquipementId = EquipementId.Value,
                    EquipeId = idE
                };

                _service.Creer(dto);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }


        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
