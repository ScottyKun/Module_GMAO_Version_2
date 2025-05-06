using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class MaintenanceCorrectiveDTO
    {
        public int MaintenanceId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public string Statut { get; set; }

        public string EquipementNom { get; set; }
        public string ResponsableNom { get; set; }

        public decimal CoutPrevu { get; set; }
    }
}
