using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public abstract class Maintenance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaintenanceId { get; set; }

        [Required]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [Required]
        public string Description { get; set; }

        public int EquipementId { get; set; }
        public virtual Equipement Equipement { get; set; }

        public int ResponsableId { get; set; }
        public virtual User Responsable { get; set; }

        public int EquipeId { get; set; }
        // public virtual Maintenance_team Equipe { get; set; }
    }
}
