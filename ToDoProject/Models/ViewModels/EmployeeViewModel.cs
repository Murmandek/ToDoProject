using System.Collections.Generic;

namespace ToDoProject.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
        public string SearchString { get; set; }
    }
}
