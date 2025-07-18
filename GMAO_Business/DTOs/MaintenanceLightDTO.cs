﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.DTOs
{
    public class MaintenanceLightDTO
    {
        public int MaintenanceId { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{MaintenanceId} - {Description}";
        }
    }
}
