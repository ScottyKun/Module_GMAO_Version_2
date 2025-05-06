using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class MaintenanceCorrective : Maintenance
    {
        public string Statut { get; set; } = "Nouvelle"; // Nouvelle, En cours, Terminee

        public decimal Cout { get; set; }

        public virtual ICollection<Corrective_Piece> PiecesReservees { get; set; } = new List<Corrective_Piece>();

        public virtual WorkOrder WorkOrder { get; set; }
    }
}
