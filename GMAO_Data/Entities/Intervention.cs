using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class Intervention
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Etat { get; set; } = "New"; // New → Pending → Terminee

        public DateTime DatePrevue { get; set; }

        public string Nom { get; set; }

        public decimal Cout { get; set; }

        public int MaintenancePlanifieeId { get; set; }
        public virtual MaintenancePlanifiee MaintenancePlanifiee { get; set; }

        public virtual ICollection<Intervention_Piece> PiecesReservees { get; set; } = new List<Intervention_Piece>();
        public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
    }
}
