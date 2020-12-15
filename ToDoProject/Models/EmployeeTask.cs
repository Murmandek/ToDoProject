using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class EmployeeTask
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int TaskId { get; set; }
        public Tasks Tasks { get; set; }

        public int Estemate { get; set; }      
        [Display(Name = "Task start")]
        public DateTime AppointmentDate { get; set; } 
    }
}
