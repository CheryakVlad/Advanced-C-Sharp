namespace Northwind.EntityFramework.Model.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Version13 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Region", newName: "Regions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Regions", newName: "Region");
        }
    }
}
