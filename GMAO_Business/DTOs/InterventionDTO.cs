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

    public class InterventionDTO2
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Etat { get; set; }
        public DateTime DatePrevue { get; set; }
        public string DescriptionMaintenance { get; set; }
        public string EquipementNom { get; set; }
        public int EquipementId { get; set; }
        public int ResponsableId { get; set; }
        public List<PieceReservationDTO> PiecesReservees { get; set; }
    }

    public class InterventionCreationDTO
    {
        public string Nom { get; set; }
        public DateTime DatePrevue { get; set; }
        public int MaintenancePlanifieeId { get; set; }
        public string Etat { get; set; } = "New"; 
    }

    public class InterventionModificationDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime DatePrevue { get; set; }
        public List<PieceReservationDTO> PiecesReservees { get; set; }
    }


}
