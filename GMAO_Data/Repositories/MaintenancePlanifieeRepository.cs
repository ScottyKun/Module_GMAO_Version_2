using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
   public  class MaintenancePlanifieeRepository
    {
        private readonly DbManager _context;

        public MaintenancePlanifieeRepository()
        {
            _context = new DbManager();
        }

        public void Add(MaintenancePlanifiee maintenance)
        {
            _context.MaintenancesPlanifiees.Add(maintenance);
            _context.SaveChanges();
        }

        public List<MaintenancePlanifiee> GetAllByResponsable(int responsableId)
        {
            return _context.MaintenancesPlanifiees
                           .Include("Equipement")
                           .Include("Responsable")
                           .Where(m => m.ResponsableId == responsableId)
                           .ToList();
        }

        public MaintenancePlanifiee GetById(int id)
        {
            return _context.MaintenancesPlanifiees
                           .Include("Equipement")
                           .Include("Responsable")
                           .Include("Interventions.WorkOrders")
                           .FirstOrDefault(m => m.MaintenanceId == id);
        }

        public void Update(MaintenancePlanifiee modif)
        {
            var m = _context.MaintenancesPlanifiees
                            .Include("Interventions")
                            .FirstOrDefault(x => x.MaintenanceId == modif.MaintenanceId);

            if (m == null)
                throw new InvalidOperationException("Maintenance introuvable.");

            if (m.Statut != "Nouvelle")
                throw new InvalidOperationException("Seules les maintenances avec le statut 'Nouvelle' peuvent être modifiées.");

            if (m.Interventions.Any(i => i.Etat != "New"))
                throw new InvalidOperationException("Impossible de modifier une maintenance avec interventions en cours.");

            m.Description = modif.Description;
            m.DateDebut = modif.DateDebut;
            m.DateFin = modif.DateFin;
            m.RecurrenceJours = modif.RecurrenceJours;
            m.NbInterventions = modif.NbInterventions;

            _context.SaveChanges();
        }

        public void Delete(int maintenanceId)
        {
            var m = _context.MaintenancesPlanifiees
                            .Include("Interventions")
                            .FirstOrDefault(x => x.MaintenanceId == maintenanceId);

            if (m == null) return;

            if (m.Interventions.Any(i => i.Etat == "Pending" || i.Etat == "New" || i.Etat == "Terminee"))
                throw new InvalidOperationException("Impossible de supprimer une maintenance avec des interventions actives.");

            _context.MaintenancesPlanifiees.Remove(m);
            _context.SaveChanges();
        }

        public List<MaintenancePlanifiee> GetAllDisponiblesPourIntervention()
        {
            var today = DateTime.Today;

            var maintenances = _context.MaintenancesPlanifiees
                                       .Include("Interventions")
                                       .Where(m =>
                                           m.Statut != "Terminee" &&
                                           m.Statut != "Echec" &&
                                           m.DateDebut <= today &&
                                           m.DateFin >= today
                                       )
                                       .ToList();

            return maintenances
                   .Where(m =>
                       ((today - m.DateDebut).TotalDays % m.RecurrenceJours == 0) &&
                       !m.Interventions.Any(i => i.DatePrevue.Date == today)
                   )
                   .ToList();
        }

        public void RecalculerCouts(int maintenanceId)
        {
            var m = _context.MaintenancesPlanifiees.FirstOrDefault(x => x.MaintenanceId == maintenanceId);
            if (m == null) return;

            var interventions = _context.Interventions
                                        .Where(i => i.MaintenancePlanifieeId == maintenanceId)
                                        .ToList();

            foreach (var i in interventions)
            {
                _context.Entry(i).Collection(x => x.PiecesReservees).Load();
                foreach (var r in i.PiecesReservees)
                {
                    _context.Entry(r).Reference(p => p.Piece).Load();
                }

                _context.Entry(i).Collection(x => x.WorkOrders).Load();
                foreach (var wo in i.WorkOrders)
                {
                    _context.Entry(wo).Collection(w => w.PiecesUtilisees).Load();
                    foreach (var pu in wo.PiecesUtilisees)
                    {
                        _context.Entry(pu).Reference(x => x.Piece).Load();
                    }
                }
            }

            m.CoutPrevu = interventions
                          .SelectMany(i => i.PiecesReservees)
                          .Sum(p => p.Piece.prix * p.Quantite);

            m.CoutReel = interventions
                          .SelectMany(i => i.WorkOrders.Where(w => w.Terminee))
                          .Sum(w => w.Cout);

            m.NbInterventionsFinish = interventions.Count(i => i.Etat == "Terminee");

            if (m.NbInterventionsFinish >= m.NbInterventions)
                m.Statut = "Terminee";
            else if (interventions.Any())
                m.Statut = "En cours";

            _context.SaveChanges();
        }
    }
}

