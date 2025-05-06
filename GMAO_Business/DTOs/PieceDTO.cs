using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class PieceDTO
    {
        public int PieceId { get; set; }
        public string Nom { get; set; }
        public string Reference { get; set; }
        public decimal Prix { get; set; }
        public DateTime DateAjout { get; set; }
        public int Quantite { get; set; }
        public string StockNom { get; set; }
        public List<string> Equipements { get; set; }
    }
}
