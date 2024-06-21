namespace CRUDWithImage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Image", c => c.String(nullable: false));
            DropColumn("dbo.Employees", "MyProperty");
        }

        public override void Down()
        {
            AddColumn("dbo.Employees", "MyProperty", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "Image");
        }
    }
}
