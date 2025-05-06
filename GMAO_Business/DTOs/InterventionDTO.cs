using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class InterventionDTO
    {
        public int Id { get; set; }

        public string Nom { get; set; }
        public string Etat { get; set; }
        public DateTime DatePrevue { get; set; }
        public decimal Cout { get; set; }

        public int MaintenanceId { get; set; }
        public string MaintenanceDescription { get; set; }
        public string EquipementNom { get; set; }

    }
}
