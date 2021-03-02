using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "NameRequired")] 
        [Display(Name = "Name")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "DescriptionRequired")] 
        [Display(Name = "Description")] 
        public string Description { get; set; }
            
        public List<EmployeeTask> EmployeeTasks { get; set; } 

        public Task()
        {
            EmployeeTasks = new List<EmployeeTask>();
        }
    }
}
