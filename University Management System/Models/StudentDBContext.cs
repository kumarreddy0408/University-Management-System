using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace University_Management_System.Models
{
    public class StudentDBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public StudentDBContext() : base("name=StudentDBContext")
        {
        }


        public System.Data.Entity.DbSet<University_Management_System.Models.Course> Courses { get; set; }

    

        public System.Data.Entity.DbSet<University_Management_System.Models.Department>Deparments { get; set; }

        public System.Data.Entity.DbSet<University_Management_System.Models.Teacher> Teachers { get; set; }
        public System.Data.Entity.DbSet<University_Management_System.Models.Student> Students { get; set; }
        public System.Data.Entity.DbSet<University_Management_System.Models.CourseAssignToTeacher> CourseAssignToTeachers { get; set; }

        public System.Data.Entity.DbSet<University_Management_System.Models.Designation> Designations { get; set; }
        public System.Data.Entity.DbSet<University_Management_System.Models.Semester> Semesters { get; set; }

        public System.Data.Entity.DbSet<University_Management_System.Models.EnrollCourse> EnrollCourses { get; set; }
        public System.Data.Entity.DbSet<University_Management_System.Models.Grade> Grades { get; set; }
        public System.Data.Entity.DbSet<University_Management_System.Models.Room> Rooms { get; set; }
        public System.Data.Entity.DbSet<University_Management_System.Models.ClassRoomAllocation> ClassRoomAllocations { get; set; }
        public System.Data.Entity.DbSet<University_Management_System.Models.Day> Days { get; set; }
        public System.Data.Entity.DbSet<University_Management_System.Models.Time> Times{ get; set; }

    }



}
  



