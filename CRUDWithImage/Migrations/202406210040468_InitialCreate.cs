namespace CRUDWithImage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {

        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    FirstName = c.Int(nullable: false),
                    LastName = c.Int(nullable: false),
                    Age = c.Int(nullable: false),
                    Email = c.String(),
                    MyProperty = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}