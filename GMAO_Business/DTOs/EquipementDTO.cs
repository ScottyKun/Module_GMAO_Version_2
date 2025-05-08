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

    public class EquipementDTO2
    {
        public int Id { get; set; }
        public string NomResponsable { get; set; }
        public string NomEquipe { get; set; }
        public int MaintenanceTeamId { get; set; }
    }

    public class EquipementDTO3
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int CategorieId { get; set; }
        public int ResponsableId { get; set; }
        public int MaintenanceTeamId { get; set; }
        public DateTime DateAchat { get; set; }
        public DateTime DateFinGarantie { get; set; }
        public bool Statut { get; set; }
        public string Commentaires { get; set; }
    }


}
