using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class Corrective_Piece
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MaintenanceCorrectiveId { get; set; }
        public virtual MaintenanceCorrective Maintenance { get; set; }

        public int PieceId { get; set; }
        public virtual PieceDeRechange Piece { get; set; }

        public int Quantite { get; set; }
    }
}
