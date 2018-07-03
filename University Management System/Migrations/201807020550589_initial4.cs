namespace University_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassRoomAllocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        TimeId = c.Int(nullable: false),
                        StartDate = c.Double(nullable: false),
                        EndDate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete:false )
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: false)
                .ForeignKey("dbo.Times", t => t.TimeId, cascadeDelete: false)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId)
                .Index(t => t.RoomId)
                .Index(t => t.TimeId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Times",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassRoomAllocations", "TimeId", "dbo.Times");
            DropForeignKey("dbo.ClassRoomAllocations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ClassRoomAllocations", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ClassRoomAllocations", "CourseId", "dbo.Courses");
            DropIndex("dbo.ClassRoomAllocations", new[] { "TimeId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "RoomId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "CourseId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "DepartmentId" });
            DropTable("dbo.Days");
            DropTable("dbo.Times");
            DropTable("dbo.Rooms");
            DropTable("dbo.ClassRoomAllocations");
        }
    }
}
