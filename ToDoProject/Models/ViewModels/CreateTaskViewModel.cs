using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models.ViewModels
{
    public class CreateTaskViewModel
    {
        [Required(ErrorMessage = "NameRequired")] 
        [Display(Name = "Name")] 
        public string TaskName { get; set; }

        [Required(ErrorMessage = "DescriptionRequired")] 
        [Display(Name = "Description")] 
        public string TaskDescription { get; set; }

        public int SelectedValue { get; set; }
        public virtual Employee Employee { get; set; }

        [DisplayName("Employee")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
