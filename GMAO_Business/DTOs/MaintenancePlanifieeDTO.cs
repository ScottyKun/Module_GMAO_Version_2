using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class MaintenancePlanifieeDTO
    {
        public int MaintenanceId { get; set; }
        public string Description { get; set; }
        public string Statut { get; set; }

        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int RecurrenceJours { get; set; }

        public int NbInterventions { get; set; }
        public int NbInterventionsFinish { get; set; }

        public decimal CoutPrevu { get; set; }
        public decimal CoutReel { get; set; }

        public string EquipementNom { get; set; }

        public string ResponsableNom { get; set; }
    }

    public class MaintenancePlanifieeDTO2
    {
        public int MaintenanceId { get; set; } // Nullable pour différencier création / modification
        public string Description { get; set; }
        public string Statut { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int RecurrenceJours { get; set; }
        public int ResponsableId { get; set; }
        public int EquipementId { get; set; }
        public int EquipeId { get; set; }
    }

}
