using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class Rapport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateTime DateCreation { get; set; }   // Date du rapport
        public int UserId { get; set; }              // User ayant généré le rapport
        public virtual User User { get; set; }       // Navigation

        public string Type { get; set; }             // Exemple: "TOP_N", "BUDGET", "FINANCIER"
        public string DonneesJson { get; set; }
    }
}
