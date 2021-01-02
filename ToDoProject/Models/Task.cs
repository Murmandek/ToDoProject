using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter task name")]
        [Display(Name = "Task name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter task description")]
        [Display(Name = "Task description")]
        public string Description { get; set; }
            
        public List<EmployeeTask> EmployeeTasks { get; set; } 

        public Task()
        {
            EmployeeTasks = new List<EmployeeTask>();
        }
    }
}
