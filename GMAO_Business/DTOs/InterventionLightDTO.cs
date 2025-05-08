using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class InterventionLightDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class InterventionLightDTO2
    {
        public int Id { get; set; }
        public DateTime DatePrevue { get; set; }
    }

}
