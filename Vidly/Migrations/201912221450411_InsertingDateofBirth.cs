namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertingDateofBirth : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Customers SET Birthdate = '14-nov-1990' WHERE Id = 5");
        }
        
        public override void Down()
        {
        }
    }
}
