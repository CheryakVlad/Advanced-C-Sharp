namespace Northwind.EntityFramework.Model.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class Version131 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FoundationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "FoundationDate");
        }
    }
}
