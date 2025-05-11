using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class MaintenancePlanifiee : Maintenance
    {
        public string Statut { get; set; } = "Nouvelle";

        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        public int RecurrenceJours { get; set; }

        public int NbInterventions { get; set; } = 0;
        public int NbInterventionsFinish { get; set; } = 0;

        public decimal CoutPrevu { get; set; }

        public decimal CoutReel { get; set; }

        public virtual ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
    }
}
