namespace GMAO_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testChanges : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                UPDATE workOrders
                SET DateExecution = DATE_ADD(DateExecution, INTERVAL 2 HOUR)
                WHERE TIME(DateExecution) = '00:00:00';
            ");

            Sql(@"
                UPDATE interventions
                SET DatePrevue = DATE_ADD(DatePrevue, INTERVAL 2 HOUR)
                WHERE TIME(DatePrevue) = '00:00:00';
            ");
        }
        
        public override void Down()
        {
            Sql(@"
                UPDATE interventions
                SET DatePrevue = DATE_SUB(DatePrevue, INTERVAL 2 HOUR)
                WHERE TIME(DatePrevue) = '02:00:00';
            ");


            Sql(@"
                UPDATE workOrders
                SET DateExecution = DATE_SUB(DateExecution, INTERVAL 2 HOUR)
                WHERE TIME(DateExecution) = '02:00:00';
            ");
        }
    }
}
