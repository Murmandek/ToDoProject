using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class CreateTaskViewModel
    {
        [Display(Name = "Task name")]
        public string TaskName { get; set; }
        [Display(Name = "Task description")]
        public string TaskDescription { get; set; }

        public int SelectedValue { get; set; }
        public virtual Employee Employee { get; set; }

        [DisplayName("Employee")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
