using System.ComponentModel.DataAnnotations;

namespace HotelManagementApp.Models
{
    public class Employee
    {
        [Key]
        public int employeeId { get; set; }
        [Required]

        public string employeeName { get; set; }

        public string employeePassword { get; set; }

        public int employeeAge { get; set; }

        public string employeeDesignation { get; set; }
       
        public double employeeSalary { get; set; }
        
        public string employeeMail { get; set; }
        
        public int employeeContact { get; set; }
        
        public string employeeAddress { get; set; }
    }
}
