using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class Budget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BudgetId { get; set; }

        [Required]
        public int Annee { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Montant { get; set; }

        public string Nom { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public int ResponsableId { get; set; }
        public virtual User Responsable { get; set; }
    }
}
