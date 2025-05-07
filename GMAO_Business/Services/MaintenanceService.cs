using GMAO_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Business.Services
{
    public class MaintenanceService
    {
        private readonly MaintenanceRepository repository;

        public MaintenanceService()
        {
            repository = new MaintenanceRepository();
        }

        public bool PeutCreerMaintenance(int equipementId, DateTime dateDebut, DateTime dateFin)
        {
            dateDebut = dateDebut.Date;
            dateFin = dateFin.Date;

            bool conflitPlanifiee = repository.ExisteConflitPlanifiee(equipementId, dateDebut, dateFin);
            bool conflitCorrective = repository.ExisteConflitCorrective(equipementId, dateDebut, dateFin);

            return !(conflitPlanifiee || conflitCorrective);
        }
    }
}
