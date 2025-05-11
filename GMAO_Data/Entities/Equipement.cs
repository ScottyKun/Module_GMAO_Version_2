using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class Equipement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string nom { get; set; }
        public DateTime dateAchat { get; set; }
        public bool statut { get; set; }
        public DateTime dateFinGarantie { get; set; }
        public string commentaires { get; set; }

        public int responsableId { get; set; }
        public virtual User responsable { get; set; }
        public int CategorieId { get; set; }
        public virtual Category Categorie { get; set; }
        public int maintenanceTeamId { get; set; }
        public virtual Maintenance_team maintenanceTeam { get; set; }

        public virtual ICollection<Equipement_Pieces> LiaisonsPieces { get; set; } = new List<Equipement_Pieces>();

        public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();
    }
}
