using GMAO_Data.Entities;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.DataBaseManager
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DbManager : DbContext
    {
        public DbManager() : base("MySqlContext") { }

        //Implementation des datasets de notre module
        public DbSet<User> Users { get; set; }
        public DbSet<Maintenance_team> Maintenance_Teams { get; set; }
        public DbSet<Team_Users> Team_Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Equipement> Equipements { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PieceDeRechange> PiecesDeRechanges { get; set; }
        public DbSet<Equipement_Pieces> Equipement_PieceDeRechanges { get; set; }
        public DbSet<DemandeAchat> DemandesAchat { get; set; }

        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<MaintenanceCorrective> MaintenancesCorrectives { get; set; }
        public DbSet<MaintenancePlanifiee> MaintenancesPlanifiees { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }

        public DbSet<Corrective_Piece> Corrective_Pieces { get; set; }
        public DbSet<Intervention_Piece> Intervention_Pieces { get; set; }
        public DbSet<WorkOrder_Piece> WorkOrder_Pieces { get; set; }

        public DbSet<Alerte> Alertes { get; set; }


        public DbSet<Budget> Budgets { get; set; }


        public DbSet<Rapport> Rapports { get; set; }


        //Gestion des relations entre les tables
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //mapping la relation one to many: chef d'equipe et equipe
            modelBuilder.Entity<Maintenance_team>()
                .HasRequired(e => e.chefEquipe)
                .WithMany(u => u.teams_chief)
                .HasForeignKey(e => e.chefEquipeId)
                .WillCascadeOnDelete(false);


            //many to many: table de jointure
            modelBuilder.Entity<Team_Users>()
                .HasKey(em => new { em.idUser, em.teamId });

            //many to many: users et maintenance_team
            modelBuilder.Entity<Team_Users>()
                .HasRequired(em => em.equipe)
                .WithMany(e => e.membres)
                .HasForeignKey(em => em.teamId);

            modelBuilder.Entity<Team_Users>()
               .HasRequired(em => em.user)
               .WithMany(u => u.equipes)
               .HasForeignKey(em => em.idUser);

            //One-to-Many : catégorie et équipement
            modelBuilder.Entity<Equipement>()
                .HasRequired(e => e.Categorie)
                .WithMany(c => c.Equipements)
                .HasForeignKey(e => e.CategorieId)
                .WillCascadeOnDelete(false);

            //One-to-Many : user et équipement
            modelBuilder.Entity<Equipement>()
            .HasRequired(e => e.responsable)
            .WithMany(u => u.equipementsResponsable)
            .HasForeignKey(e => e.responsableId)
            .WillCascadeOnDelete(false);

            //One-to-Many : maintenance_team et équipement
            modelBuilder.Entity<Equipement>()
            .HasRequired(e => e.maintenanceTeam)
            .WithMany(t => t.Equipements)
            .HasForeignKey(e => e.maintenanceTeamId)
            .WillCascadeOnDelete(false);

            //one to many: stock et piece de rechange
            modelBuilder.Entity<PieceDeRechange>()
           .HasRequired(p => p.Stock)
           .WithMany(s => s.Pieces)
           .HasForeignKey(p => p.StockId)
           .WillCascadeOnDelete(false); //check


            modelBuilder.Entity<Equipement_Pieces>()
            .HasKey(ep => new { ep.EquipementId, ep.PieceDeRechangeId });

            modelBuilder.Entity<Equipement_Pieces>()
                .HasRequired(ep => ep.Equipement)
                .WithMany(e => e.LiaisonsPieces)
                .HasForeignKey(ep => ep.EquipementId);

            modelBuilder.Entity<Equipement_Pieces>()
                .HasRequired(ep => ep.PieceDeRechange)
                .WithMany(p => p.LiaisonsEquipements)
                .HasForeignKey(ep => ep.PieceDeRechangeId);

            //
            //heritage
            modelBuilder.Entity<Maintenance>()
               .ToTable("Maintenances");

            modelBuilder.Entity<MaintenanceCorrective>()
                .ToTable("Maintenances");

            modelBuilder.Entity<MaintenancePlanifiee>()
                .ToTable("Maintenances");

            // === Maintenance → Equipement
            modelBuilder.Entity<Maintenance>()
                           .HasRequired(m => m.Equipement)
                           .WithMany(e => e.Maintenances)
                           .HasForeignKey(m => m.EquipementId)
                           .WillCascadeOnDelete(false);


            // === Maintenance → Responsable
            modelBuilder.Entity<Maintenance>()
                .HasRequired(m => m.Responsable)
                .WithMany()
                .HasForeignKey(m => m.ResponsableId)
                .WillCascadeOnDelete(false);



            /* === MaintenanceCorrective → WorkOrder(1:1)
            modelBuilder.Entity<WorkOrder>()
             .HasOptional(wo => wo.MaintenanceCorrective)
             .WithOptionalPrincipal(mc => mc.WorkOrder)
             .Map(m => m.MapKey("MaintenanceCorrectiveId"))
             .WillCascadeOnDelete(false);
            */


            // === MaintenanceCorrective → WorkOrder(1:1)
            modelBuilder.Entity<MaintenanceCorrective>()
                .HasOptional(mc => mc.WorkOrder)
                .WithOptionalPrincipal(wo => wo.MaintenanceCorrective)
                .WillCascadeOnDelete(false);


            // === MaintenanceCorrective → Corrective_Piece
            modelBuilder.Entity<Corrective_Piece>()
                .HasRequired(cp => cp.Maintenance)
                .WithMany(m => m.PiecesReservees)
                .HasForeignKey(cp => cp.MaintenanceCorrectiveId);

            modelBuilder.Entity<Corrective_Piece>()
                .HasRequired(cp => cp.Piece)
                .WithMany()
                .HasForeignKey(cp => cp.PieceId);

            // === MaintenancePlanifiee → Intervention
            modelBuilder.Entity<MaintenancePlanifiee>()
                .HasMany(mp => mp.Interventions)
                .WithRequired(i => i.MaintenancePlanifiee)
                .HasForeignKey(i => i.MaintenancePlanifieeId)
                .WillCascadeOnDelete(false);


            // === Intervention → Intervention_Piece
            modelBuilder.Entity<Intervention_Piece>()
                .HasRequired(ip => ip.Intervention)
                .WithMany(i => i.PiecesReservees)
                .HasForeignKey(ip => ip.InterventionId);

            modelBuilder.Entity<Intervention_Piece>()
                .HasRequired(ip => ip.Piece)
                .WithMany()
                .HasForeignKey(ip => ip.PieceId);

            // === Intervention → WorkOrders (1:N
            modelBuilder.Entity<WorkOrder>()
                .HasOptional(wo => wo.Intervention)
                .WithMany(i => i.WorkOrders)
                .HasForeignKey(wo => wo.InterventionId)
                .WillCascadeOnDelete(false);

            // === WorkOrder → WorkOrder_Piece
            modelBuilder.Entity<WorkOrder_Piece>()
                .HasRequired(wp => wp.WorkOrder)
                .WithMany(wo => wo.PiecesUtilisees)
                .HasForeignKey(wp => wp.WorkOrderId);

            modelBuilder.Entity<WorkOrder_Piece>()
                .HasRequired(wp => wp.Piece)
                .WithMany()
                .HasForeignKey(wp => wp.PieceId);


            //budget 
            modelBuilder.Entity<Budget>()
                .HasRequired(b => b.Responsable)
                .WithMany()
                .HasForeignKey(b => b.ResponsableId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Budget>()
                .HasIndex(b => b.Annee)
                .IsUnique(); // Une seule entrée par an

            //equipe et maintenance
            /*modelBuilder.Entity<Maintenance>()
            .HasRequired(m => m.Equipe)
            .WithMany()
            .HasForeignKey(m => m.EquipeId)
            .WillCascadeOnDelete(false);
           */

        }
    }
}
