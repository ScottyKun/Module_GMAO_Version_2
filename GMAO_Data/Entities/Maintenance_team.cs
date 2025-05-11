using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class Maintenance_team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int teamId { get; set; }

        [Required]
        public string nom { get; set; }

        [Required]
        public bool statut { get; set; }

        public int chefEquipeId { get; set; }
        public User chefEquipe { get; set; }

        //Liste des membres
        public ICollection<Team_Users> membres { get; set; } = new List<Team_Users>();

        // Équipements assignés à cette équipe
        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();

    }
}
