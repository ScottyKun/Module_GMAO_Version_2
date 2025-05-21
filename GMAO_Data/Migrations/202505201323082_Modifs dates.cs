namespace GMAO_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifsdates : DbMigration
    {
        public override void Up()
        {
            Sql(@"
        UPDATE workOrders
        SET DateExecution = CASE
            WHEN Id = 2 AND DATE(DateExecution) = '2025-04-16' THEN DATE_ADD(DateExecution, INTERVAL '16:15:30' HOUR_SECOND)
            WHEN Id = 3 AND DATE(DateExecution) = '2025-04-16' THEN DATE_ADD(DateExecution, INTERVAL '18:15:10' HOUR_SECOND)
            WHEN Id = 12 AND DATE(DateExecution) = '2025-05-03' THEN DATE_ADD(DateExecution, INTERVAL '20:40:05' HOUR_SECOND)
             WHEN Id = 14 AND DATE(DateExecution) = '2025-05-10' THEN DATE_ADD(DateExecution, INTERVAL '00:15:30' HOUR_SECOND)
            WHEN Id = 15 AND DATE(DateExecution) = '2025-05-10' THEN DATE_ADD(DateExecution, INTERVAL '01:30:40' HOUR_SECOND)
            WHEN Id = 16 AND DATE(DateExecution) = '2025-05-10' THEN DATE_ADD(DateExecution, INTERVAL '02:45:55' HOUR_SECOND)
            ELSE DateExecution
        END
        WHERE TIME(DateExecution) = '02:00:00';
    ");




            Sql(@"
        UPDATE interventions
        SET DatePrevue = CASE
            WHEN Id = 2 AND DATE(DatePrevue) = '2025-04-21' THEN DATE_ADD(DatePrevue, INTERVAL '12:26:37' HOUR_SECOND)
            WHEN Id = 6 AND DATE(DatePrevue) = '2025-04-21' THEN DATE_ADD(DatePrevue, INTERVAL '13:30:00' HOUR_SECOND)
            WHEN Id = 8 AND DATE(DatePrevue) = '2025-04-23' THEN DATE_ADD(DatePrevue, INTERVAL '14:30:05' HOUR_SECOND)
            WHEN Id = 10 AND DATE(DatePrevue) = '2025-04-23' THEN DATE_ADD(DatePrevue, INTERVAL '08:50:00' HOUR_SECOND)
            WHEN Id = 14 AND DATE(DatePrevue) = '2025-05-10' THEN DATE_ADD(DatePrevue, INTERVAL '04:10:15' HOUR_SECOND)
            WHEN Id = 15 AND DATE(DatePrevue) = '2025-05-10' THEN DATE_ADD(DatePrevue, INTERVAL '11:30:00' HOUR_SECOND)
            WHEN Id = 16 AND DATE(DatePrevue) = '2025-05-10' THEN DATE_ADD(DatePrevue, INTERVAL '09:45:40' HOUR_SECOND)
            WHEN Id = 26 AND DATE(DatePrevue) = '2025-05-10' THEN DATE_ADD(DatePrevue, INTERVAL '17:30:45' HOUR_SECOND)
            WHEN Id = 27 AND DATE(DatePrevue) = '2025-05-12' THEN DATE_ADD(DatePrevue, INTERVAL '09:00:00' HOUR_SECOND)
            ELSE DatePrevue
        END
        WHERE TIME(DatePrevue) = '02:00:00';
    ");

        }

        public override void Down()
        {
            Sql(@"
    UPDATE interventions
    SET DatePrevue = CASE
        WHEN Id = 2 AND DATE(DatePrevue) = '2025-04-21' THEN DATE_SUB(DatePrevue, INTERVAL '12:26:37' HOUR_SECOND)
        WHEN Id = 6 AND DATE(DatePrevue) = '2025-04-21' THEN DATE_SUB(DatePrevue, INTERVAL '13:30:00' HOUR_SECOND)
        WHEN Id = 8 AND DATE(DatePrevue) = '2025-04-23' THEN DATE_SUB(DatePrevue, INTERVAL '14:30:05' HOUR_SECOND)
        WHEN Id = 10 AND DATE(DatePrevue) = '2025-04-23' THEN DATE_SUB(DatePrevue, INTERVAL '08:50:00' HOUR_SECOND)
        WHEN Id = 14 AND DATE(DatePrevue) = '2025-05-10' THEN DATE_SUB(DatePrevue, INTERVAL '04:10:15' HOUR_SECOND)
        WHEN Id = 15 AND DATE(DatePrevue) = '2025-05-10' THEN DATE_SUB(DatePrevue, INTERVAL '11:30:00' HOUR_SECOND)
        WHEN Id = 16 AND DATE(DatePrevue) = '2025-05-10' THEN DATE_SUB(DatePrevue, INTERVAL '09:45:40' HOUR_SECOND)
        WHEN Id = 26 AND DATE(DatePrevue) = '2025-05-10' THEN DATE_SUB(DatePrevue, INTERVAL '17:30:45' HOUR_SECOND)
        WHEN Id = 27 AND DATE(DatePrevue) = '2025-05-12' THEN DATE_SUB(DatePrevue, INTERVAL '09:00:00' HOUR_SECOND)
        ELSE DatePrevue
    END
    WHERE TIME(DatePrevue) IN (
        '12:26:37', '13:30:00', '14:30:05', '08:50:00',
        '04:10:15', '11:30:00', '09:45:40', '17:30:45', '09:00:00'
    );
");


            Sql(@"
        UPDATE workOrders
        SET DateExecution = CASE
            WHEN Id = 2 AND DATE(DateExecution) = '2025-04-16' THEN DATE_SUB(DateExecution, INTERVAL '16:15:30' HOUR_SECOND)
            WHEN Id = 3 AND DATE(DateExecution) = '2025-04-16' THEN DATE_SUB(DateExecution, INTERVAL '18:15:10' HOUR_SECOND)
            WHEN Id = 12 AND DATE(DateExecution) = '2025-05-03' THEN DATE_SUB(DateExecution, INTERVAL '20:40:05' HOUR_SECOND)
            WHEN Id = 14 AND DATE(DateExecution) = '2025-05-10' THEN DATE_SUB(DateExecution, INTERVAL '00:15:30' HOUR_SECOND)
            WHEN Id = 15 AND DATE(DateExecution) = '2025-05-10' THEN DATE_SUB(DateExecution, INTERVAL '01:30:40' HOUR_SECOND)
            WHEN Id = 16 AND DATE(DateExecution) = '2025-05-10' THEN DATE_SUB(DateExecution, INTERVAL '02:45:55' HOUR_SECOND)
            ELSE DateExecution
        END
        WHERE TIME(DateExecution) IN ('16:15:30', '18:15:10', '20:40:05', '00:15:30', '01:30:40', '02:45:55');
    ");


        }
    }
}
