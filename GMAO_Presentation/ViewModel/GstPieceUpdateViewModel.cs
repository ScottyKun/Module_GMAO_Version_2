
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
    public class GstPieceUpdateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly PieceRechangeService _service;
        private readonly int _id;

        public List<StockDTO> Stocks { get; set; }
        public List<EquipementLightDTO> Equipements { get; set; }

        public string Nom { get; set; }
        public string Reference { get; set; }
        public decimal Prix { get; set; }
        public DateTime DateAjout { get; set; }
        public int Quantite { get; set; }

        public int? StockId { get; set; }
        public List<int> EquipementIds { get; set; } = new List<int>();

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }

        public GstPieceUpdateViewModel(int pieceId)
        {
            _service = new PieceRechangeService();
            _id = pieceId;

            Stocks = _service.GetStocks();
            Equipements = _service.GetEquipements();

            var piece = _service.GetAllPieces().FirstOrDefault(p => p.PieceId == pieceId);
            if (piece != null)
            {
                Nom = piece.Nom;
                Reference = piece.Reference;
                Prix = piece.Prix;
                DateAjout = piece.DateAjout;
                Quantite = piece.Quantite;
                StockId = Stocks.FirstOrDefault(s => s.Nom == piece.StockNom)?.Id;
                EquipementIds = _service.GetByEquipementIds(pieceId);  
            }

            ModifierCommand = new RelayCommand(Modifier, PeutModifier);
            SupprimerCommand = new RelayCommand(Supprimer);
        }

        private bool PeutModifier()
        {
            return !string.IsNullOrWhiteSpace(Nom)
                && !string.IsNullOrWhiteSpace(Reference)
                && Prix > 0
                && Quantite > 0
                && StockId.HasValue
                && EquipementIds.Any();
        }

        private void Modifier()
        {
            var dto = new PieceDeRechangeDTO
            {
                PieceId = _id,
                Nom = Nom,
                Reference = Reference,
                Prix = Prix,
                DateAjout = DateAjout,
                Quantite = Quantite,
                StockId = StockId.Value,
                EquipementIds = EquipementIds
            };

            _service.UpdatePiece(dto);

        }
        private void Supprimer()
        {
            _service.DeletePiece(_id);
        }

        protected void OnPropertyChanged(string nom)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nom));
    }
}
