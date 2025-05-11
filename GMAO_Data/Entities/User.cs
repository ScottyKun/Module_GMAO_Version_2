using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUser { get; set; }

        [MaxLength(50)]
        public string nom { get; set; }

        [MaxLength(50)]
        public string prenom { get; set; }

        [Required, MaxLength(100)]
        [EmailAddress]
        public string email { get; set; }

        [MaxLength(10)]
        [Phone]
        public string tel { get; set; }

        [Required]
        public string fonction { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        public bool Actif { get; set; } = true;

        //liste des equipes ou user est chef
        public ICollection<Maintenance_team> teams_chief { get; set; } = new List<Maintenance_team>();

        //liste des equipes ou user est membre
        public ICollection<Team_Users> equipes { get; set; } = new List<Team_Users>();

        // Équipements qu’il supervise
        public virtual ICollection<Equipement> equipementsResponsable { get; set; } = new List<Equipement>();


    }
}
