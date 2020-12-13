﻿using System.Collections.Generic;
using System.ComponentModel;

namespace ToDoProject.Models
{
    public class EmployeeTaskViewModel
    {
        public int EmployeeSelectedValue { get; set; }
        public virtual Employee Employee { get; set; }

        public int TaskSelectedValue { get; set; }
        public virtual Tasks Tasks { get; set; }

        [DisplayName("Employee")]
        public virtual ICollection<Employee> Employees { get; set; }

        [DisplayName("Tasks")]
        public virtual ICollection<Tasks> Taskss { get; set; }
    }
}
