namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAgainBirthDate : DbMigration
    {
        public override void Up()
        {
            Sql("update Customers set BirthDate = cast('12/03/1978 12:27:13' as datetime) where Id = 8");
            Sql("update Customers set BirthDate = cast('05/07/1985 12:02:00' as datetime) where Id = 9");
            Sql("update Customers set BirthDate = cast('02/19/1990 12:03:00' as datetime) where Id = 10");
            Sql("update Customers set BirthDate = cast('11/27/1991 12:04:00' as datetime) where Id = 11");
        }
        
        public override void Down()
        {
        }
    }
}
