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
    public class WOIUpVM : INotifyPropertyChanged
    {
        private readonly WorkOrderPlanifieeService _service;

        public int WorkOrderId { get; set; }

        public string Nom { get; set; }
        public DateTime DateExecution { get; set; }
        public string Rapport { get; set; }

        public string DescriptionIntervention { get; set; }
        public string EquipementNom { get; set; }

        public int InterventionId { get; set; }


        public BindingList<PieceReservationView> PiecesReservees { get; set; }
        public BindingList<PieceUtilisationView> PiecesUtilisees { get; set; }

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }
        public RelayCommand TerminerCommand { get; }
        public RelayCommand ImpossibleCommand { get; }

        public event Action OnClose;
        public event Action<string> OnError;

        private bool _terminee;

        public WOIUpVM(int workOrderId)
        {
            _service = new WorkOrderPlanifieeService();
            WorkOrderId = workOrderId;

            ModifierCommand = new RelayCommand(Modifier, PeutModifier);
            SupprimerCommand = new RelayCommand(Supprimer);
            TerminerCommand = new RelayCommand(Terminer);
            ImpossibleCommand = new RelayCommand(Impossible);

            Charger();
        }

        private void Charger()
        {
            var wo = _service.GetById(WorkOrderId);
            if (wo == null)
            {
                OnError?.Invoke("WorkOrder introuvable.");
                return;
            }

            _terminee = wo.Terminee;
            Nom = wo.Nom;
            DateExecution = wo.DateExecution;
            Rapport = wo.Rapport ?? "";

            DescriptionIntervention = wo.DescriptionIntervention;
            EquipementNom = wo.EquipementNom;
            InterventionId = wo.InterventionId;

            PiecesReservees = new BindingList<PieceReservationView>(wo.PiecesReservees);
            PiecesUtilisees = new BindingList<PieceUtilisationView>(wo.PiecesUtilisees);
        }


        private void Modifier()
        {
            try
            {
                var pieces = PiecesUtilisees.Select(p => new PieceUtilisationDTO
                {
                    PieceId = p.PieceId,
                    Quantite = p.Quantite
                }).ToList();

                _service.Modifier(WorkOrderId, Nom, DateExecution, Rapport, pieces);
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
                var pieces = PiecesUtilisees.Where(p => p.Quantite > 0).Select(p => new PieceUtilisationDTO
                {
                    PieceId = p.PieceId,
                    Quantite = p.Quantite
                }).ToList();

                if (!pieces.Any())
                {
                    OnError?.Invoke("Aucune pièce utilisée.");
                    return;
                }

                _service.Terminer(WorkOrderId, pieces);
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
                _service.Supprimer(WorkOrderId);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        private void Impossible()
        {
            try
            {
                _service.MarquerCommeImpossible(WorkOrderId);
                OnClose?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        public void AjouterPieceUtiliseeDepuisReservation(int pieceId)
        {
            var reservation = PiecesReservees.FirstOrDefault(p => p.PieceId == pieceId);
            if (reservation == null)
            {
                OnError?.Invoke("Aucune pièce réservée trouvée avec cet ID.");
                return;
            }

            var existante = PiecesUtilisees.FirstOrDefault(p => p.PieceId == pieceId);
            if (existante != null)
            {
                OnError?.Invoke("Cette pièce a déjà été ajoutée dans la liste des pièces utilisées.");
                return;
            }

            PiecesUtilisees.Add(new PieceUtilisationView
            {
                PieceId = reservation.PieceId,
                Nom = reservation.Nom,
                Quantite = 0
            });
        }

        private bool PeutModifier() =>
       !_terminee && !string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Rapport);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
