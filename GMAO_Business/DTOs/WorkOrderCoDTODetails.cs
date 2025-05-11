using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class WorkOrderCoDTODetails
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime DateExecution { get; set; }
        public decimal Cout { get; set; }
        public bool Terminee { get; set; }

        public int MaintenanceId { get; set; }
        public string MaintenanceDescription { get; set; }

        public List<PieceReservationView> PiecesReservees { get; set; }
        public List<PieceUtilisationView> PiecesUtilisees { get; set; }
    }
}
