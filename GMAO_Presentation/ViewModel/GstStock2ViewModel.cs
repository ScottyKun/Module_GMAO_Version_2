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
    public class GstStock2ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly StockService _service;
        private string _nom;

        public string Nom
        {
            get => _nom;
            set { _nom = value; OnPropertyChanged(nameof(Nom)); EnregistrerCommand.EvaluateCanExecute(); }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            EnregistrerCommand.EvaluateCanExecute();
        }


        public RelayCommand EnregistrerCommand { get; }

        public GstStock2ViewModel()
        {
            _service = new StockService();
            EnregistrerCommand = new RelayCommand(Enregistrer, CanEnregistrer);
        }

        private bool CanEnregistrer() => !string.IsNullOrWhiteSpace(Nom);


        private void Enregistrer()
        {
            var dto = new StockDTO { Nom = Nom };
            _service.Add(dto);
        }


    }
}
