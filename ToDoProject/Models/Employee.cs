using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "NameRequired")] 
        [Display(Name = "Name")] 
        public string Name { get; set; }

        [Required (ErrorMessage = "AgeRequired")] 
        [Range(1, 130, ErrorMessage = "AgeRange")] 
        [RegularExpression(@"^[0-9]*$")]
        [Display(Name = "Age")] 
        public int Age { get; set; }

        [Required (ErrorMessage = "AddressRequired")] 
        [Display(Name = "Address")] 
        public string Address { get; set; }

        [Required(ErrorMessage = "PositionRequired")] 
        [Display(Name = "Position")] 
        public string Position { get; set; }

        public List<EmployeeTask> EmployeeTasks { get; set; }

        public Employee()
        {
            EmployeeTasks = new List<EmployeeTask>();
        }

        public virtual Image Image { get; set; }
    }
}
