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
    public class MaintenancePlanUpVM : INotifyPropertyChanged
    {
        private readonly MaintenancePlanService _service;
        private readonly EquipementService _equipementService;

        public event PropertyChangedEventHandler PropertyChanged;

        public int MaintenanceId { get; set; }

        public string Description { get; set; }
        public string Statut { get; set; }

        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int Recurrence { get; set; }

        public List<EquipementLightDTO> Equipements { get; set; }
        public int EquipementId { get; set; }

        public string NomResponsable { get; set; }
        public string NomEquipe { get; set; }

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }
        public RelayCommand ConvertirCommand { get; }

        public event Action OnClose;
        public event Action<string> OnError;
        public event Action<int, string> OnConvertToIntervention;


        public MaintenancePlanUpVM(int maintenanceId)
        {
            MaintenanceId = maintenanceId;
            _service = new MaintenancePlanService();
            _equipementService = new EquipementService();

            ModifierCommand = new RelayCommand(Modifier, PeutModifier);
            SupprimerCommand = new RelayCommand(Supprimer);
            ConvertirCommand = new RelayCommand(Convertir);

            Charger();
        }

        private void Charger()
        {
            var m = _service.GetById(MaintenanceId);
            if (m == null)
            {
                OnError?.Invoke("Maintenance introuvable.");
                return;
            }

            Description = m.Description;
            Statut = m.Statut;
            DateDebut = m.DateDebut;
            DateFin = m.DateFin;
            Recurrence = m.RecurrenceJours;
            EquipementId = m.EquipementId;

            // Charger nom du responsable (via un service utilisateur)
            var responsable = new UserService().GetById(m.ResponsableId);
            NomResponsable = responsable != null ? $"{responsable.Nom} {responsable.Prenom}" : "-";

            // Charger nom de l'équipe via l'équipement
            var equipement = new EquipementService().GetById2(m.EquipementId);
            NomEquipe = equipement?.MaintenanceTeam?.Nom ?? "-";

            // Charger les équipements du responsable
            Equipements = _equipementService.GetEquipementsLightByResponsable(m.ResponsableId);
        }


        private void Modifier()
        {
            try
            {
                var dto = new MaintenancePlanifieeDTO2
                {
                    MaintenanceId = MaintenanceId,
                    Description = Description,
                    DateDebut = DateDebut,
                    DateFin = DateFin,
                    RecurrenceJours = Recurrence,
                };

                _service.Modifier(dto);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }


        private void Supprimer()
        {
            try
            {
                _service.Supprimer(MaintenanceId);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void Convertir()
        {
            if (Statut == "Terminee")
            {
                OnError?.Invoke("Maintenance déjà terminée.");
                return;
            }

            if (DateTime.Today < DateDebut)
            {
                OnError?.Invoke("Impossible de convertir avant la date de début.");
                return;
            }

            // ✅ On autorise conversion uniquement si on est pile à une date de récurrence
            var joursDepuisDebut = (DateTime.Today - DateDebut).Days;
            if (joursDepuisDebut % Recurrence != 0)
            {
                OnError?.Invoke("La date d'aujourd'hui ne correspond pas à une date de récurrence.");
                return;
            }

            // 3. Vérifie si une intervention existe déjà aujourd'hui
            var interventions = new InterventionService().GetByMaintenanceId(MaintenanceId);
            if (interventions.Any(i => i.DatePrevue.Date == DateTime.Today))
            {
                OnError?.Invoke("Une intervention a déjà été créée pour aujourd'hui.");
                return;
            }

            OnConvertToIntervention?.Invoke(MaintenanceId, Description);
        }

        private bool PeutModifier()
        {
            return Statut != "Terminee" && !string.IsNullOrWhiteSpace(Description);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ModifierCommand.RaiseCanExecuteChanged();
        }
    }
}
