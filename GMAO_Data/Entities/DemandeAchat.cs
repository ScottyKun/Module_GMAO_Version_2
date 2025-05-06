using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class DemandeAchat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Libelle { get; set; }

        [Required]
        public string NomPiece { get; set; }

        [Required]
        public string Reference { get; set; }

        public DateTime DateDemande { get; set; } = DateTime.Now;
    }
}
