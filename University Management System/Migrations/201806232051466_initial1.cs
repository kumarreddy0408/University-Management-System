namespace University_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CourseAssignToTeachers", new[] { "CourseId" });
            AddColumn("dbo.CourseAssignToTeachers", "CreditTaken", c => c.Double(nullable: false));
            AddColumn("dbo.CourseAssignToTeachers", "CreditLeft", c => c.Double(nullable: false));
            AddColumn("dbo.CourseAssignToTeachers", "Name", c => c.String());
            AddColumn("dbo.CourseAssignToTeachers", "Credit", c => c.Double(nullable: false));
            CreateIndex("dbo.CourseAssignToTeachers", "CourseID");
            DropColumn("dbo.CourseAssignToTeachers", "CreditTobeTaken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseAssignToTeachers", "CreditTobeTaken", c => c.Single(nullable: false));
            DropIndex("dbo.CourseAssignToTeachers", new[] { "CourseID" });
            DropColumn("dbo.CourseAssignToTeachers", "Credit");
            DropColumn("dbo.CourseAssignToTeachers", "Name");
            DropColumn("dbo.CourseAssignToTeachers", "CreditLeft");
            DropColumn("dbo.CourseAssignToTeachers", "CreditTaken");
            CreateIndex("dbo.CourseAssignToTeachers", "CourseId");
        }
    }
}
