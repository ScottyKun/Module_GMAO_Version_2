﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class UserDTO2
    {
        public int idUser { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public DateTime? dateAjout { get; set; }
    }
}
