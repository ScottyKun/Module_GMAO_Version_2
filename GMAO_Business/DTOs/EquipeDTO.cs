using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class EquipeDTO
    {
        public int IdEquipe { get; set; }
        public string NomEquipe { get; set; }
        public bool Statut { get; set; }
        public int IdChefEquipe { get; set; }
        public string ChefEquipeNom { get; set; } // Affichage du nom du chef
        public List<UserDTO2> Membres { get; set; } = new List<UserDTO2>(); // Liste des membres avec leur date d'ajout
    }
}
