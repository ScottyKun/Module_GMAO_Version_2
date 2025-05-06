using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class Alerte
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Libelle { get; set; } // ex: "Alerte Stock", "Alerte Maintenance"

        [Required]
        public string Message { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public string Priorite { get; set; }

        public bool Terminee { get; set; }

        public int? ResponsableId { get; set; }
    }
}
