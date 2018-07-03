using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_Management_System.Models
{
    public class ClassRoomAllocation
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public int DayId { get; set; }
        public virtual Day Day { get; set; }
        
        public double StartTime { get; set; }

        public double EndTime { get; set; }



           


    }
}