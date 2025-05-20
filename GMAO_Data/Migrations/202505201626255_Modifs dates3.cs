namespace GMAO_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifsdates3 : DbMigration
    {
        public override void Up()
        {
            Sql(@"
        UPDATE workOrders SET DateExecution = '2025-04-16 23:15:30' WHERE Id = 12;
        
    ");
        }
        
        public override void Down()
        {
        }
    }
}
