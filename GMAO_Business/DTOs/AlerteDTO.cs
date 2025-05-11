using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class AlerteDTO
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Message { get; set; }
        public string Priorite { get; set; }
        public DateTime DateCreation { get; set; }
        public bool Terminee { get; set; }
    }

}
