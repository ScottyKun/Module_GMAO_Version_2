using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.KPI
{
    public class KpiMonthlyEvolutionResult
    {
        public int Mois { get; set; }
        public decimal CoutCorrective { get; set; }
        public decimal CoutPlanifiee { get; set; }
    }
}
