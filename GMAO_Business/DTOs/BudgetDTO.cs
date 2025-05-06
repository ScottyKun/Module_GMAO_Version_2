using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class BudgetDTO
    {
        public int BudgetId { get; set; }
        public int Annee { get; set; }
        public decimal Montant { get; set; }
        public string Nom { get; set; }
        public DateTime DateCreation { get; set; }
        public int ResponsableId { get; set; }
    }
}
