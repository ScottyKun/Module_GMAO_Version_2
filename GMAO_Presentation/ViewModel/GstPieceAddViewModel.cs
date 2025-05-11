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
    public class GstPieceAddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly PieceRechangeService _service;

        public List<StockDTO> Stocks { get; set; }
        public List<EquipementLightDTO> Equipements { get; set; }

        public string Nom { get; set; }
        public string Reference { get; set; }
        public decimal Prix { get; set; }
        public DateTime DateAjout { get; set; } = DateTime.Today;
        public int Quantite { get; set; }

        public int? StockId { get; set; }
        public List<int> EquipementIds { get; set; } = new List<int>();

        public RelayCommand AjouterCommand { get; }

        public GstPieceAddViewModel()
        {
            _service = new PieceRechangeService();
            Stocks = _service.GetStocks();
            Equipements = _service.GetEquipements();

            AjouterCommand = new RelayCommand(Ajouter, PeutAjouter);
        }

        private bool PeutAjouter()
        {
            return !string.IsNullOrWhiteSpace(Nom)
                && !string.IsNullOrWhiteSpace(Reference)
                && Prix > 0
                && Quantite > 0
                && StockId.HasValue
                && EquipementIds.Any();
        }

        private void Ajouter()
        {
            var dto = new PieceDeRechangeDTO
            {
                Nom = Nom,
                Reference = Reference,
                Prix = Prix,
                DateAjout = DateAjout,
                Quantite = Quantite,
                StockId = StockId.Value,
                EquipementIds = EquipementIds
            };

            _service.AddPiece(dto);

        }

        protected void OnPropertyChanged(string nom)
       => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nom));
    }
}
