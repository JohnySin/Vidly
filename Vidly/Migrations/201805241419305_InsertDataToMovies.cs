namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertDataToMovies : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Genres(Id, Name) Values(1, 'Action')");
            Sql("insert into Genres(Id, Name) Values(2, 'Comedy')");
            Sql("insert into Genres(Id, Name) Values(3, 'Drama')");
            Sql("insert into Genres(Id, Name) Values(4, 'Science Fiction')");

            Sql("insert into Movies(Name, DateAdded, DateReleased, NumberInStock, Genre_Id) Values('Matrix', cast('12/03/1978 12:27:13' as datetime), cast('12/03/2000 12:27:13' as datetime), 12, 4)");
            Sql("insert into Movies(Name, DateAdded, DateReleased, NumberInStock, Genre_Id) Values('Die Hard', cast('12/03/1978 12:27:13' as datetime), cast('12/03/1996 12:27:13' as datetime), 12, 1)");
            Sql("insert into Movies(Name, DateAdded, DateReleased, NumberInStock, Genre_Id) Values('The Terminator', cast('12/03/1978 12:27:13' as datetime), cast('12/03/1988 12:27:13' as datetime), 12, 1)");
        }

        public override void Down()
        {
        }
    }
}
