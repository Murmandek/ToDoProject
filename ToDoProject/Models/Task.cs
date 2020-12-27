using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Task name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Task description")]
        public string Description { get; set; }
            
        public List<EmployeeTask> EmployeeTasks { get; set; } 

        public Task()
        {
            EmployeeTasks = new List<EmployeeTask>();
        }
    }
}
