using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Please enter employee name")]
        [Display(Name = "Employee name")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Please enter employee age")]
        [Range(1, 130)]
        [RegularExpression(@"^[0-9]*$")]
        public int Age { get; set; }

        [Required (ErrorMessage = "Please enter employee address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter employee position")]
        public string Position { get; set; }

        public List<EmployeeTask> EmployeeTasks { get; set; }

        public Employee()
        {
            EmployeeTasks = new List<EmployeeTask>();
        }

        public Image Image { get; set; }
    }
}
