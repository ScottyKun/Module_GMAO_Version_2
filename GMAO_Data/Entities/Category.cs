using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{

    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; }

        [Required]
        public string nom { get; set; }

        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();
    }

}
