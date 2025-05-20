using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.KPI
{
    public class KpiMTBFResult
    {
        public int EquipementId { get; set; }
        public string NomEquipement { get; set; }
        public double MTBFenJours { get; set; }

        public double Debut => 0;
    }

}
