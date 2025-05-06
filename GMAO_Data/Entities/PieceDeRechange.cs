using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class PieceDeRechange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pieceId { get; set; }

        [Required]
        public string nom { get; set; }

        [Required]
        public string reference { get; set; }

        public decimal prix { get; set; }
        public DateTime dateAjout { get; set; }
        public int quantite { get; set; }

        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }

        public virtual ICollection<Equipement_Pieces> LiaisonsEquipements { get; set; } = new List<Equipement_Pieces>();

    }
}
