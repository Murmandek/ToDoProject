﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models.ViewModels
{
    public class EmployeeTaskViewModel
    {
        public int EmployeeSelectedValue { get; set; }
        public virtual Employee Employee { get; set; }

        public int TaskSelectedValue { get; set; }
        public virtual Task Task { get; set; }

        [Display(Name = "Employee")]
        public virtual ICollection<Employee> Employees { get; set; }

        [Display(Name = "Task")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
