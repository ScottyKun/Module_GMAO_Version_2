using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class PieceReservationView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int PieceId { get; set; }

        public string Nom { get; set; }

        public int QuantiteStock { get; set; }

        private int _quantiteAReserver;
        public int QuantiteAReserver
        {
            get => _quantiteAReserver;
            set
            {
                if (value < 0)
                    _quantiteAReserver = 0;
                else if (value > QuantiteStock)
                    _quantiteAReserver = QuantiteStock;
                else
                    _quantiteAReserver = value;

                OnPropertyChanged(nameof(QuantiteAReserver));
            }
        }

        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
