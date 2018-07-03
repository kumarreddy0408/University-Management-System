namespace University_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseAssignToTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        CreditTobeTaken = c.Single(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: false)
                .Index(t => t.DepartmentId)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Course_Code = c.String(),
                        Course_Name = c.String(),
                        Course_Credit = c.Double(nullable: false),
                        CourseAssignTo = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeptCode = c.String(),
                        DeptName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Semester_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Contact = c.String(),
                        DesignationId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.DesignationId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EnrollCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNo = c.String(nullable: false),
                        CourseId = c.Int(nullable: false),
                        EnrollDate = c.DateTime(nullable: false),
                        CourseGrade = c.String(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.CourseId)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentRegNo = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Contact = c.String(),
                        RegDate = c.DateTime(nullable: false),
                        Address = c.String(),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GradeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnrollCourses", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.EnrollCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseAssignToTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Teachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseAssignToTeachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseAssignToTeachers", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropIndex("dbo.EnrollCourses", new[] { "Student_Id" });
            DropIndex("dbo.EnrollCourses", new[] { "CourseId" });
            DropIndex("dbo.Teachers", new[] { "DepartmentId" });
            DropIndex("dbo.Teachers", new[] { "DesignationId" });
            DropIndex("dbo.Courses", new[] { "SemesterId" });
            DropIndex("dbo.Courses", new[] { "DepartmentId" });
            DropIndex("dbo.CourseAssignToTeachers", new[] { "CourseId" });
            DropIndex("dbo.CourseAssignToTeachers", new[] { "TeacherId" });
            DropIndex("dbo.CourseAssignToTeachers", new[] { "DepartmentId" });
            DropTable("dbo.Grades");
            DropTable("dbo.Students");
            DropTable("dbo.EnrollCourses");
            DropTable("dbo.Designations");
            DropTable("dbo.Teachers");
            DropTable("dbo.Semesters");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseAssignToTeachers");
        }
    }
}
