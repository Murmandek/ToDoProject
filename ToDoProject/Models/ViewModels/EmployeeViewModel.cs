using System.Collections.Generic;

namespace ToDoProject.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string SearchString { get; set; }
    }
}
