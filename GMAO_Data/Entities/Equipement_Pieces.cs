using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Entities
{
    public class Equipement_Pieces
    {
        public int EquipementId { get; set; }
        public virtual Equipement Equipement { get; set; }

        public int PieceDeRechangeId { get; set; }
        public virtual PieceDeRechange PieceDeRechange { get; set; }
    }
}
