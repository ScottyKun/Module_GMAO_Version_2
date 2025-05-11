using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class WorkOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nom { get; set; }

        public DateTime DateExecution { get; set; }

        public bool Terminee { get; set; }

        public decimal Cout { get; set; }

        public string Rapport { get; set; }

        public virtual MaintenanceCorrective MaintenanceCorrective { get; set; }

        public int? InterventionId { get; set; }
        public virtual Intervention Intervention { get; set; }

        public virtual ICollection<WorkOrder_Piece> PiecesUtilisees { get; set; } = new List<WorkOrder_Piece>();
    }
}
