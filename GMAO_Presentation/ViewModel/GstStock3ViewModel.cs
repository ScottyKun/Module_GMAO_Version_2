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
    public class GstStock3ViewModel : INotifyPropertyChanged
    {
        private readonly StockService _service;
        public StockDTO Stock { get; set; }

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _nouveauNom;
        public string NouveauNom
        {
            get => _nouveauNom;
            set
            {
                _nouveauNom = value;
                OnPropertyChanged(nameof(NouveauNom));
            }
        }

        public GstStock3ViewModel(StockDTO stock)
        {
            _service = new StockService();
            Stock = stock;
            NouveauNom = stock.Nom;

            ModifierCommand = new RelayCommand(Modifier);
            SupprimerCommand = new RelayCommand(Supprimer);
        }

        private void Modifier()
        {
            Stock.Nom = NouveauNom;
            _service.Update(Stock);
        }

        private void Supprimer()
        {
            _service.Delete(Stock.Id);
        }

        protected void OnPropertyChanged(string name) =>
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
