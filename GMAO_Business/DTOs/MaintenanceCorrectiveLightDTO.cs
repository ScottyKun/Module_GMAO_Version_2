using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class MaintenanceCorrectiveLightDTO
    {
        public int MaintenanceId { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"#{MaintenanceId} - {Description}";
    }

    public class MaintenanceCorrectiveDTO2
    {
        public int MaintenanceId { get; set; }
        public string Description { get; set; }
        public int EquipementId { get; set; }
        public int ResponsableId { get; set; }
        public string Statut { get; set; }
        public DateTime DateCreation { get; set; }
        public int EquipeId { get; set; }

        public string ResponsableNom { get; set; }
        public string EquipeMaintenanceNom { get; set; }

        public List<PieceReservationDTO> PiecesReservees { get; set; }
    }

}
