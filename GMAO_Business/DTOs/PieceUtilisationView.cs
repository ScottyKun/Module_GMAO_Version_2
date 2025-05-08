using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class PieceUtilisationView : INotifyPropertyChanged
    {
        public int PieceId { get; set; }
        public string Nom { get; set; }

        private int _quantite;
        public int Quantite
        {
            get => _quantite;
            set
            {
                _quantite = value < 0 ? 0 : value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantite)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
