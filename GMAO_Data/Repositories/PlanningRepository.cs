using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class PlanningRepository
    {
        private readonly DbManager _db;

        public PlanningRepository()
        {
            _db = new DbManager();
        }

        public MaintenancePlanifiee GetMaintenanceWithInterventions(int maintenanceId)
        {
            return _db.MaintenancesPlanifiees
                     .Include("Interventions")
                     .FirstOrDefault(m => m.MaintenanceId == maintenanceId);
        }

        public List<int> GetNonTermineesMaintenanceIds()
        {
            return _db.MaintenancesPlanifiees
                      .Where(m => m.Statut != "Terminee")
                      .Select(m => m.MaintenanceId)
                      .ToList();
        }

        public List<int> GetMaintenanceIdsForResponsable(int responsableId)
        {
            return _db.MaintenancesPlanifiees
                      .Where(m => m.ResponsableId == responsableId && m.Statut != "Terminee")
                      .Select(m => m.MaintenanceId)
                      .ToList();
        }

        public List<Alerte> GetOldAlertes(int delaiJours)
        {
            var seuil = DateTime.Now.AddDays(-delaiJours);
            return _db.Alertes.Where(a => a.DateCreation < seuil).ToList();
        }

        public List<PieceDeRechange> GetPiecesCritiques()
        {
            return _db.PiecesDeRechanges
                      .Where(p => p.quantite < 5)
                      .ToList();
        }

        public List<string> GetMessagesAlertesStockDuJour()
        {
            return _db.Alertes
                      .Where(a => a.Libelle == "Alerte Stock" && a.DateCreation.Date == DateTime.Today)
                      .Select(a => a.Message)
                      .ToList();
        }

        public List<MaintenanceCorrective> GetCorrectivesSansWorkOrder(int responsableId)
        {
            var deuxJoursAvant = DateTime.Today.AddDays(-2);
            return _db.MaintenancesCorrectives
                      .Where(m =>
                          m.ResponsableId == responsableId &&
                          m.WorkOrder == null &&
                          m.Statut != "Terminee" &&
                          m.DateCreation <= deuxJoursAvant)
                      .ToList();
        }

        public List<string> GetMessagesAlertesCorrectivesDuJour()
        {
            return _db.Alertes
                      .Where(a => a.Libelle == "Alerte Corrective" && a.DateCreation.Date == DateTime.Today)
                      .Select(a => a.Message)
                      .ToList();
        }

        public void AjouterAlerte(Alerte alerte)
        {
            _db.Alertes.Add(alerte);
        }

        public void SupprimerAlerte(Alerte alerte)
        {
            _db.Alertes.Remove(alerte);
        }

        public void Save() => _db.SaveChanges();
    }
}
