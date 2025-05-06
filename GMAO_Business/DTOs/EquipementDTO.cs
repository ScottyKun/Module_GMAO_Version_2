using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class EquipementDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Categorie { get; set; }
        public DateTime DateAchat { get; set; }
        public string Responsable { get; set; }
        public bool Statut { get; set; }
        public DateTime DateFinGarantie { get; set; }
        public string MaintenanceTeam { get; set; }
        public string Commentaires { get; set; }
    }
}
