using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    // ici c'est notre table de jointure entre users et maintenance_team
    public class Team_Users
    {
        public int teamId { get; set; }
        public Maintenance_team equipe { get; set; }

        public int idUser { get; set; }
        public User user { get; set; }

        public DateTime dateAjout { get; set; } = DateTime.Now;
    }
}
