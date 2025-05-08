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
    public class InterventionUpVM : INotifyPropertyChanged
    {
        private readonly InterventionService _service;
        private readonly EquipementService _equipementService;

        public event PropertyChangedEventHandler PropertyChanged;

        public int InterventionId { get; set; }
        public int ResponsableId { get; set; }

        public string Nom { get; set; }
        public string Statut { get; set; }
        public DateTime DatePrevue { get; set; }

        public string DescriptionMaintenance { get; set; }
        public string EquipementNom { get; set; }

        public BindingList<PieceReservationView> PiecesDisponibles { get; set; }

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }
        public RelayCommand ConvertirCommand { get; }

        public event Action<string> OnError;
        public event Action OnClose;
        public event Action<int, string, int> OnConvertToWorkOrder;

        //private Intervention intervention;

        public InterventionUpVM(int interventionId)
        {
            _service = new InterventionService();
            _equipementService = new EquipementService();
            InterventionId = interventionId;

            ModifierCommand = new RelayCommand(Modifier, PeutModifier);
            SupprimerCommand = new RelayCommand(Supprimer);
            ConvertirCommand = new RelayCommand(Convertir);

            Charger();
        }

        private void Charger()
        {
            var intervention = _service.GetById(InterventionId);
            if (intervention == null)
            {
                OnError?.Invoke("Intervention introuvable.");
                return;
            }

            Nom = intervention.Nom;
            Statut = intervention.Etat;
            DatePrevue = intervention.DatePrevue;
            DescriptionMaintenance = intervention.DescriptionMaintenance;
            EquipementNom = intervention.EquipementNom;
            ResponsableId = intervention.ResponsableId;

            var pieces = _equipementService.GetPiecesDeRechangeParEquipement(intervention.EquipementId);

            PiecesDisponibles = new BindingList<PieceReservationView>(
                pieces.Select(p =>
                {
                    var reservee = intervention.PiecesReservees.FirstOrDefault(r => r.PieceId == p.PieceId);
                    return new PieceReservationView
                    {
                        PieceId = p.PieceId,
                        Nom = p.Nom,
                        QuantiteStock = p.QuantiteStock,
                        QuantiteAReserver = reservee?.Quantite ?? 0
                    };
                }).ToList()
            );
        }


        private bool PeutModifier()
        {
            return Statut != "Pending" && !string.IsNullOrWhiteSpace(Nom);
        }

        private void Modifier()
        {
            try
            {
                var dto = new InterventionModificationDTO
                {
                    Id = this.InterventionId,
                    Nom = this.Nom,
                    DatePrevue = this.DatePrevue,
                    PiecesReservees = PiecesDisponibles
                                          .Where(p => p.QuantiteAReserver > 0)
                                          .Select(p => new PieceReservationDTO
                                          {
                                              PieceId = p.PieceId,
                                              Quantite = p.QuantiteAReserver
                                          }).ToList()
                };

                _service.ModifierComplet(dto);


                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                OnError?.Invoke(message);
            }
        }
        private void Supprimer()
        {
            try
            {
                _service.Supprimer(InterventionId);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void Convertir()
        {
            if (Statut == "Pending" || Statut == "Terminee")
            {
                OnError?.Invoke("Intervention déjà en cours ou terminée.");
                return;
            }

            if (!PiecesDisponibles.Any(p => p.QuantiteAReserver > 0))
            {
                OnError?.Invoke("Aucune pièce réservée pour cette intervention.");
                return;
            }

            OnConvertToWorkOrder?.Invoke(ResponsableId, DescriptionMaintenance, InterventionId);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ModifierCommand.RaiseCanExecuteChanged();
        }
    }
}
