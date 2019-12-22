namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateMovies : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies ( Name, Genre, ReleaseDate, DateAdded, Stock) VALUES('Gladiator','Historical','11-jan-2000','11-jan-2013',5)");
            Sql("INSERT INTO Movies ( Name, Genre, ReleaseDate, DateAdded, Stock) VALUES('The Patriot','Action','13-mar-2000','11-jan-2014',2)");
            Sql("INSERT INTO Movies ( Name, Genre, ReleaseDate, DateAdded, Stock) VALUES('Charlies Angels','Popular','1-feb-2000','11-jan-2015',20)");
            Sql("INSERT INTO Movies ( Name, Genre, ReleaseDate, DateAdded, Stock) VALUES('Pitch Black','Sci-Fi','26-jun-2000','11-jan-2016',1)");
            Sql("INSERT INTO Movies ( Name, Genre, ReleaseDate, DateAdded, Stock) VALUES('Scary Movie','Comedy','14-nov-2000','11-jan-2010',40)");
        }
        
        public override void Down()
        {
        }
    }
}
