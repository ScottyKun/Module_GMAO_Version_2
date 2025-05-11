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
    public class WOCoUpdateVM : INotifyPropertyChanged
    {
        private readonly WorkOrderCoService _woService;
        private readonly MaintenanceCorrectiveService _maintenanceService;

        public event PropertyChangedEventHandler PropertyChanged;

        public WorkOrderDTO GetById(int id)
        {
            return _woService.GetById2(id);
        }

        public int WorkOrderId { get; set; }

        public string Nom { get; set; }
        public DateTime DateExecution { get; set; }
        public string Rapport { get; set; }

        public int MaintenanceId { get; set; }
        public string MaintenanceDescription { get; set; }

        public BindingList<PieceReservationView> PiecesReservees { get; set; }
        public BindingList<PieceUtilisationView> PiecesUtilisees { get; set; }

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }
        public RelayCommand TerminerCommand { get; }
        public RelayCommand ImpossibleCommand { get; }

        public event Action<string> OnError;
        public event Action OnClose;

        private bool _terminee;

        public WOCoUpdateVM(int workOrderId)
        {
            _woService = new WorkOrderCoService();
            _maintenanceService = new MaintenanceCorrectiveService();
            WorkOrderId = workOrderId;

            ModifierCommand = new RelayCommand(Modifier, PeutModifier);
            SupprimerCommand = new RelayCommand(Supprimer);
            TerminerCommand = new RelayCommand(Terminer);
            ImpossibleCommand = new RelayCommand(MarquerImpossible);

            Charger();
        }

        private void Charger()
        {
            var workOrder = _woService.GetById(WorkOrderId);
            if (workOrder == null)
            {
                OnError?.Invoke("WorkOrder introuvable.");
                return;
            }

            _terminee = workOrder.Terminee;
            Nom = workOrder.Nom;
            DateExecution = workOrder.DateExecution;
            Rapport = workOrder.MaintenanceDescription ?? "";

            MaintenanceId = workOrder.MaintenanceId;
            MaintenanceDescription = workOrder.MaintenanceDescription ?? "";

            PiecesReservees = new BindingList<PieceReservationView>(workOrder.PiecesReservees);
            PiecesUtilisees = new BindingList<PieceUtilisationView>(workOrder.PiecesUtilisees);
         
        }

        private bool PeutModifier()
        {
            return !_terminee && !string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Rapport);
        }

        private void Modifier()
        {
            try
            {
                var pieces = PiecesUtilisees
                             .Where(p => p.Quantite > 0)
                             .Select(p => new PieceUtilisationDTO
                             {
                                 PieceId = p.PieceId,
                                 Quantite = p.Quantite
                             }).ToList();

                _woService.ModifierWorkOrder(WorkOrderId, Nom, DateExecution, Rapport, pieces);
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
                _woService.Supprimer(WorkOrderId);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void Terminer()
        {
            try
            {
                var pieces = PiecesUtilisees
                             .Where(p => p.Quantite > 0)
                             .Select(p => new PieceUtilisationDTO
                             {
                                 PieceId = p.PieceId,
                                 Quantite = p.Quantite
                             }).ToList();

                _woService.TerminerWorkOrder(WorkOrderId, pieces);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void MarquerImpossible()
        {
            try
            {
                _woService.MarquerWorkOrderCommeImpossible(WorkOrderId);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string nom = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nom));
            ModifierCommand.RaiseCanExecuteChanged();
        }

        public void AjouterPieceUtiliseeDepuisReservation(int pieceId)
        {
            var reservation = PiecesReservees.FirstOrDefault(p => p.PieceId == pieceId);
            if (reservation == null)
            {
                OnError?.Invoke("Aucune pièce réservée trouvée avec cet ID.");
                return;
            }

            // Vérifie si déjà présente dans les pièces utilisées
            var existante = PiecesUtilisees.FirstOrDefault(p => p.PieceId == pieceId);
            if (existante != null)
            {
                OnError?.Invoke("Cette pièce est déjà utilisée.");
                return;
            }

            PiecesUtilisees.Add(new PieceUtilisationView
            {
                PieceId = reservation.PieceId,
                Nom = reservation.Nom,
                Quantite = 0
            });
        }



    }
}
