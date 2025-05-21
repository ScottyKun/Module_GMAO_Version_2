using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.KPI
{
    public class KpiPlanificationResult
    {
        public string Responsable { get; set; }
        public int NbPrevues { get; set; }
        public int NbRealisees { get; set; }
        public double TauxRealisation => NbPrevues == 0 ? 0 : Math.Round((double)NbRealisees / NbPrevues * 100, 2);
    }

}
