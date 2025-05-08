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
    public class MaintenanceCoAddViewModel : INotifyPropertyChanged
    {
        private readonly MaintenanceCorrectiveService _service;
        private readonly EquipementService _equipementService;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Description { get; set; } = "";
        public string Statut { get; } = "Nouvelle";

        public List<EquipementLightDTO> Equipements { get; set; }
        public int? EquipementId { get; set; }

        public string NomResponsable { get; set; }
        public string NomEquipe { get; set; }

        public BindingList<PieceReservationView> PiecesDisponibles { get; set; } = new BindingList<PieceReservationView>();

        public RelayCommand EnregistrerCommand { get; }

        private readonly int _idResponsable;

        private int idE;

        private int equiId;


        public event Action OnClose;
        public event Action<string> OnError;

        public MaintenanceCoAddViewModel(int idResponsable)
        {
            _service = new MaintenanceCorrectiveService();
            _equipementService = new EquipementService();
            _idResponsable = idResponsable;

            Equipements = _equipementService.GetEquipementsLightByResponsable(idResponsable);

            EnregistrerCommand = new RelayCommand(Enregistrer, PeutEnregistrer);
        }

        public void ChargerPiecesEtInfos(int equipementId)
        {
            var pieces = _equipementService.GetPiecesDeRechangeParEquipement(equipementId);

            PiecesDisponibles.Clear();
            foreach (var p in pieces)
                PiecesDisponibles.Add(new PieceReservationView
                {
                    PieceId = p.PieceId,
                    Nom = p.Nom,
                    QuantiteStock = p.QuantiteStock,
                    QuantiteAReserver = 0
                });

            var equipementDTO = _equipementService.GetById(equipementId);
            if (equipementDTO != null)
            {
                NomResponsable = equipementDTO.NomResponsable;
                NomEquipe = equipementDTO.NomEquipe;
                idE = equipementDTO.MaintenanceTeamId;
                equiId = equipementDTO.Id;
            }
            else
            {
                NomResponsable = "-";
                NomEquipe = "-";
                idE = 0;
                equiId = 0;
            }

            OnPropertyChanged(nameof(NomResponsable));
            OnPropertyChanged(nameof(NomEquipe));
        }

        private bool PeutEnregistrer()
        {
            return !string.IsNullOrWhiteSpace(Description) && PiecesDisponibles.Any(p => p.QuantiteAReserver > 0);
        }

        private void Enregistrer()
        {
            var aujourdHui = DateTime.Today;

            if (!new MaintenanceService().PeutCreerMaintenance(equiId, aujourdHui, aujourdHui))
            {

                string msg = "Impossible : une maintenance est déjà active pour aujourd'hui sur cet équipement.";
                OnError?.Invoke(msg);
                return;
            }

            try
            {
                var dto = new MaintenanceCorrectiveDTO2
                {
                    Description = Description,
                    DateCreation = DateTime.Now,
                    EquipementId = EquipementId.Value,
                    ResponsableId = _idResponsable,
                    Statut = Statut,
                    EquipeId = idE
                };

                var reservations = PiecesDisponibles
                    .Where(p => p.QuantiteAReserver > 0)
                    .Select(p => new PieceReservationDTO
                    {
                        PieceId = p.PieceId,
                        Quantite = p.QuantiteAReserver
                    }).ToList();

                _service.CreerMaintenanceCorrective(dto, reservations);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }

        }

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }   
}
