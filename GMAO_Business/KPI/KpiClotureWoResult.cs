using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.KPI
{
    public class KpiClotureWoResult
    {
        public string Responsable { get; set; }
        public int TotalWO { get; set; }
        public int Termines { get; set; }
        public double TauxCloture => TotalWO == 0 ? 0 : Math.Round((double)Termines / TotalWO * 100, 2);
    }

}
