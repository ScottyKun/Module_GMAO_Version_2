namespace GMAO_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifsdates2 : DbMigration
    {
        public override void Up()
        {
            Sql(@"
        UPDATE workOrders
        SET DateExecution = CASE
            WHEN Id = 7 AND DATE(DateExecution) = '2025-04-22' THEN DATE_ADD(DateExecution, INTERVAL '13:25:30' HOUR_SECOND)
            WHEN Id = 9 AND DATE(DateExecution) = '2025-04-22' THEN DATE_ADD(DateExecution, INTERVAL '14:15:10' HOUR_SECOND)
            WHEN Id = 10 AND DATE(DateExecution) = '2025-04-23' THEN DATE_ADD(DateExecution, INTERVAL '14:50:25' HOUR_SECOND)
            WHEN Id = 11 AND DATE(DateExecution) = '2025-04-27' THEN DATE_ADD(DateExecution, INTERVAL '00:15:30' HOUR_SECOND)
            WHEN Id = 21 AND DATE(DateExecution) = '2025-05-10' THEN DATE_ADD(DateExecution, INTERVAL '05:45:55' HOUR_SECOND)
            WHEN Id = 22 AND DATE(DateExecution) = '2025-05-10' THEN DATE_ADD(DateExecution, INTERVAL '11:35:30' HOUR_SECOND)
            WHEN Id = 27 AND DATE(DateExecution) = '2025-05-10' THEN DATE_ADD(DateExecution, INTERVAL '18:30:40' HOUR_SECOND)
            WHEN Id = 28 AND DATE(DateExecution) = '2025-05-12' THEN DATE_ADD(DateExecution, INTERVAL '09:45:55' HOUR_SECOND)
            ELSE DateExecution
        END
        WHERE TIME(DateExecution) = '02:00:00';
    ");

        }

        public override void Down()
        {
            Sql(@"
        UPDATE workOrders
        SET DateExecution = CASE
            WHEN Id = 7 AND DATE(DateExecution) = '2025-04-22' THEN DATE_SUB(DateExecution, INTERVAL '13:25:30' HOUR_SECOND)
            WHEN Id = 9 AND DATE(DateExecution) = '2025-04-22' THEN DATE_SUB(DateExecution, INTERVAL '14:15:10' HOUR_SECOND)
            WHEN Id = 10 AND DATE(DateExecution) = '2025-04-23' THEN DATE_SUB(DateExecution, INTERVAL '14:50:25' HOUR_SECOND)
            WHEN Id = 11 AND DATE(DateExecution) = '2025-04-27' THEN DATE_SUB(DateExecution, INTERVAL '00:15:30' HOUR_SECOND)
            WHEN Id = 21 AND DATE(DateExecution) = '2025-05-10' THEN DATE_SUB(DateExecution, INTERVAL '05:45:55' HOUR_SECOND)
            WHEN Id = 22 AND DATE(DateExecution) = '2025-05-10' THEN DATE_SUB(DateExecution, INTERVAL '11:35:30' HOUR_SECOND)
            WHEN Id = 27 AND DATE(DateExecution) = '2025-05-10' THEN DATE_SUB(DateExecution, INTERVAL '18:30:40' HOUR_SECOND)
            WHEN Id = 28 AND DATE(DateExecution) = '2025-05-12' THEN DATE_SUB(DateExecution, INTERVAL '09:45:55' HOUR_SECOND)
            ELSE DateExecution
        END
        WHERE TIME(DateExecution) IN (
            '13:25:30', '14:15:10', '14:50:25', '00:15:30',
            '05:45:55', '11:35:30', '18:30:40', '09:45:55'
        );
    ");
        }
    }
}
