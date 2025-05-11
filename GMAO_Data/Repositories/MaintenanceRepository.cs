using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class MaintenanceRepository
    {
        private readonly DbManager db;

        public MaintenanceRepository()
        {
            db = new DbManager();
        }

        public bool ExisteConflitPlanifiee(int equipementId, DateTime dateDebut, DateTime dateFin)
        {
            return db.Maintenances
                     .OfType<MaintenancePlanifiee>()
                     .Any(m => m.EquipementId == equipementId
                            && m.Statut != "Terminee"
                            && m.Statut != "Echec"
                            && m.DateDebut < dateFin
                            && m.DateFin > dateDebut);
        }

        public bool ExisteConflitCorrective(int equipementId, DateTime dateDebut, DateTime dateFin)
        {
            return db.Maintenances
                     .OfType<MaintenanceCorrective>()
                     .Where(m => m.EquipementId == equipementId
                              && m.Statut != "Terminee"
                              && m.Statut != "Echec")
                     .AsEnumerable() // nécessaire pour comparer les dates sans heure
                     .Any(m => m.DateCreation.Date >= dateDebut && m.DateCreation.Date <= dateFin);
        }
    }
}
