namespace CRUDWithImage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thirdone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String());
            AlterColumn("dbo.Employees", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "LastName", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "FirstName", c => c.Int(nullable: false));
        }
    }
}
