using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class WorkOrderDetailsDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime DateExecution { get; set; }
        public bool Terminee { get; set; }
        public string Rapport { get; set; }

        public int InterventionId { get; set; }
        public string DescriptionIntervention { get; set; }
        public string EquipementNom { get; set; }

        public List<PieceReservationView> PiecesReservees { get; set; }
        public List<PieceUtilisationView> PiecesUtilisees { get; set; }
    }

}
