using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.KPI
{
    public class KpiBudgetResult
    {
        public int Annee { get; set; }
        public decimal BudgetPrevu { get; set; }
        public decimal CoutReel { get; set; }

        public decimal Ecart { get; set; }
    }
}
