using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.KPI
{
    public class KpiBudgetMoisResult
    {
        public int Annee { get; set; }
        public int Mois { get; set; }
        public decimal CoutPrevu { get; set; }
        public decimal CoutReel { get; set; }
    }
}
