namespace University_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "CreditTaken", c => c.Double(nullable: false));
            AddColumn("dbo.Teachers", "CreditLeft", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "CreditLeft");
            DropColumn("dbo.Teachers", "CreditTaken");
        }
    }
}
