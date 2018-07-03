using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University_Management_System.Models
{
    public class Teacher
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
       
        public string Address { get; set; }
       
       
        public string Email { get; set; }
    
        [DisplayName("Contact No.")]
        public string Contact { get; set; }

        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        public virtual Designation Designation { get; set; }

        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }



        
        [Display(Name = "Credit to be Taken")]
        public double CreditTaken { get; set; }
        public double CreditLeft { get; set; }






    }
}