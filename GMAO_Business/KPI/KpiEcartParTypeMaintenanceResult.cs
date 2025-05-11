using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.KPI
{
    public class KpiEcartParTypeMaintenanceResult
    {
        public string Equipement { get; set; }
        public string TypeMaintenance { get; set; }
        public decimal CoutPrevu { get; set; }
        public decimal CoutReel { get; set; }
        public decimal Ecart => CoutReel - CoutPrevu;
    }

}
