using GMAO_Business.DTOs;
using GMAO_Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class EquipementAccueilViewModel
    {
        private readonly EquipementService _service;

        public BindingList<EquipementDTO> Equipements { get; set; }

        public EquipementAccueilViewModel()
        {
            _service = new EquipementService();
            ChargerEquipements();
        }

        public void ChargerEquipements()
        {
            var data = _service.GetAll();
            Equipements = new BindingList<EquipementDTO>(data);
        }
    }
}
