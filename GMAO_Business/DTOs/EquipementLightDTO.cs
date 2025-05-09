using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class EquipementLightDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
    }

    public class Equipement2DTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public EquipeDTO2 MaintenanceTeam { get; set; }
    }

    public class EquipeDTO2
    {
        public int Id { get; set; }
        public string Nom { get; set; }
    }

}
