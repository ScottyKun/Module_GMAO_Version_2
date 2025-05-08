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
    public class MaintenanceCoUpdateViewModel : INotifyPropertyChanged
    {
        private readonly MaintenanceCorrectiveService _service;
        private readonly EquipementService _equipementService;
        private readonly WorkOrderCoService _woService;

        public event PropertyChangedEventHandler PropertyChanged;

        public int MaintenanceId { get; set; }

        public string Description { get; set; }
        public string Statut { get; set; }

        public List<EquipementLightDTO> Equipements { get; set; }
        public int EquipementId { get; set; }
        public string ResponsableNom { get; set; }
        public int ResponsableId { get; set; }

        public string EquipeMaintenanceNom { get; set; }

        public BindingList<PieceReservationView> Pieces { get; set; } = new BindingList<PieceReservationView>();

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }
        public RelayCommand ConvertirCommand { get; }

        public event Action OnClose;
        public event Action<string> OnError;
        public event Action<int, int, string> OnConvertToWorkOrder;

        public MaintenanceCoUpdateViewModel(int maintenanceId)
        {
            _service = new MaintenanceCorrectiveService();
            _equipementService = new EquipementService();
            _woService = new WorkOrderCoService();

            MaintenanceId = maintenanceId;

            ModifierCommand = new RelayCommand(Modifier, PeutModifier);
            SupprimerCommand = new RelayCommand(Supprimer);
            ConvertirCommand = new RelayCommand(ConvertirEnWorkOrder);

            ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            var m = _service.GetById(MaintenanceId);
            if (m == null)
            {
                OnError?.Invoke("Maintenance introuvable.");
                return;
            }

            Description = m.Description;
            Statut = m.Statut;
            EquipementId = m.EquipementId;
            ResponsableNom = m.ResponsableNom;
            ResponsableId = m.ResponsableId;
            EquipeMaintenanceNom = m.EquipeMaintenanceNom;

            Equipements = _equipementService.GetEquipementsLightByResponsable(m.ResponsableId);

            Pieces = new BindingList<PieceReservationView>(
                _equipementService.GetPiecesDeRechangeParEquipement(m.EquipementId)
                    .Select(p =>
                    {
                        var qteReservee = m.PiecesReservees.FirstOrDefault(r => r.PieceId == p.PieceId)?.Quantite ?? 0;
                        return new PieceReservationView
                        {
                            PieceId = p.PieceId,
                            Nom = p.Nom,
                            QuantiteStock = p.QuantiteStock,
                            QuantiteAReserver = qteReservee
                        };
                    }).ToList()
            );
        }


        private void Modifier()
        {
            try
            {
                var dto = new MaintenanceCorrectiveDTO2
                {
                    MaintenanceId = MaintenanceId,
                    Description = Description,
                    EquipementId = EquipementId
                };

                var pieces = Pieces
                    .Where(p => p.QuantiteAReserver > 0)
                    .Select(p => new PieceReservationDTO
                    {
                        PieceId = p.PieceId,
                        Quantite = p.QuantiteAReserver
                    }).ToList();

                _service.Modifier(dto, pieces);
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

        private void ConvertirEnWorkOrder()
        {
            if (Statut == "Terminée")
            {
                OnError?.Invoke("Maintenance déjà terminée.");
                return;
            }

            if (!Pieces.Any(p => p.QuantiteAReserver > 0))
            {
                OnError?.Invoke("Aucune pièce réservée pour cette maintenance.");
                return;
            }

            OnConvertToWorkOrder?.Invoke(ResponsableId, MaintenanceId, Description);

        }

        private bool PeutModifier()
        {
            return !string.IsNullOrWhiteSpace(Description) && Statut != "Terminée";
        }

        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ModifierCommand.RaiseCanExecuteChanged();
        }

    }
}
