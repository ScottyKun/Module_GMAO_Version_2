using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class TeamInfo
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public bool Statut { get; set; }
        public int ChefEquipeId { get; set; }
        public string NomChef { get; set; }
    }
}
