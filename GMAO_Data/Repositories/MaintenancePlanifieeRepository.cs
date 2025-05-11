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

        public MaintenancePlanifiee GetById2(int id)
        {
            return _context.MaintenancesPlanifiees
                           .FirstOrDefault(m => m.MaintenanceId == id);
        }

        public void Update(MaintenancePlanifiee modif)
        {
            var m = _context.MaintenancesPlanifiees
                            .Include("Interventions")
                            .FirstOrDefault(x => x.MaintenanceId == modif.MaintenanceId);

            if (m == null)
                throw new InvalidOperationException("Maintenance introuvable.");

            if (m.Statut != "Nouvelle" && m.NbInterventions!=m.NbInterventionsFinish)
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

        public void ModifierStatut(int maintenanceId, string nouveauStatut)
        {
            var m = _context.MaintenancesPlanifiees.Find(maintenanceId);
            if (m == null)
                throw new InvalidOperationException("Maintenance introuvable.");

            m.Statut = nouveauStatut;
            _context.SaveChanges();
        }


        public void Delete(int maintenanceId)
        {
            var m = _context.MaintenancesPlanifiees
                            .Include("Interventions")
                            .FirstOrDefault(x => x.MaintenanceId == maintenanceId);

            if (m == null) return;

            if (m.Interventions.Any(i => i.Etat == "Pending" || i.Etat == "New" || i.Etat == "Terminee"|| i.Etat=="Echec"))
                throw new InvalidOperationException("Impossible de supprimer une maintenance avec des interventions actives.");

            _context.MaintenancesPlanifiees.Remove(m);
            _context.SaveChanges();
        }

        public List<MaintenancePlanifiee> GetAllDisponiblesPourIntervention(int userId, string fonction)
        {
            var today = DateTime.Today;

            var maintenancesQuery = _context.MaintenancesPlanifiees
                                            .Include(m => m.Interventions)
                                            .Where(m =>
                                                m.Statut != "Terminee" &&
                                                m.Statut != "Echec" &&
                                                m.DateDebut <= today);

            if (fonction == "Responsable")
            {
                maintenancesQuery = maintenancesQuery
                                    .Where(m => m.ResponsableId == userId);
            }
            else if (fonction == "Technicien")
            {
                // Récupération des IDs des responsables liés au technicien
                var teamIds = _context.Team_Users
                                      .Where(t => t.idUser == userId)
                                      .Select(t => t.teamId)
                                      .ToList();

                var responsableIds = _context.Equipements
                                             .Where(e => teamIds.Contains(e.maintenanceTeamId))
                                             .Select(e => e.responsableId)
                                             .Distinct()
                                             .ToList();

                maintenancesQuery = maintenancesQuery
                                    .Where(m => responsableIds.Contains(m.ResponsableId));
            }
            else
            {
                return new List<MaintenancePlanifiee>(); // Non concerné
            }

            var maintenances = maintenancesQuery.ToList();

            return maintenances
                   .Where(m =>
                   {
                       bool dansPeriode = m.DateFin >= today;
                       double joursEcoules = (today - m.DateDebut).TotalDays;
                       bool jourRecurrence = joursEcoules % m.RecurrenceJours == 0;

                       bool interventionAujourdhuiExiste = m.Interventions.Any(i => i.DatePrevue.Date == today);
                       var datesRecurrenceManquantes = GetDatesRecurrenceManquantes(m, today);

                       return dansPeriode &&
                              (
                                  (jourRecurrence && !interventionAujourdhuiExiste) ||
                                  datesRecurrenceManquantes.Any()
                              );
                   })
                   .ToList();
        }


        private List<DateTime> GetDatesRecurrenceManquantes(MaintenancePlanifiee m, DateTime today)
        {
            var datesManquantes = new List<DateTime>();

            // Date de début arrondie au jour de récurrence suivant
            DateTime dateCourante = m.DateDebut;

            while (dateCourante <= today && dateCourante <= m.DateFin)
            {
                // Vérifie si une intervention existe pour cette date
                bool interventionExistante = m.Interventions.Any(i => i.DatePrevue.Date == dateCourante.Date);

                if (!interventionExistante && dateCourante < today)
                {
                    datesManquantes.Add(dateCourante);
                }

                // Passe à la prochaine date de récurrence
                dateCourante = dateCourante.AddDays(m.RecurrenceJours);
            }

            return datesManquantes;
        }

        public void RecalculerCouts(int maintenanceId)
        {
            var m = _context.MaintenancesPlanifiees
             .FirstOrDefault(x => x.MaintenanceId == maintenanceId);

            if (m == null) return;

            var interventions = _context.Interventions
                                     .Where(i => i.MaintenancePlanifieeId == maintenanceId)
                                     .ToList();

            if (interventions.Count == 0)
            {
                m.CoutPrevu = 0;
                m.CoutReel = 0;
                m.NbInterventionsFinish = 0;
                m.Statut = "Nouvelle";
                _context.SaveChanges();
                return;
            }

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

            m.NbInterventionsFinish = interventions.Count(i => i.Etat == "Terminee" || i.Etat == "Echec");

            if (m.NbInterventionsFinish >= m.NbInterventions)
                m.Statut = "Terminee";

            if (interventions.Any(i => i.Etat == "Echec"))
            {
                m.Statut = "Echec";
            }

            _context.SaveChanges();
        }
    }
}

