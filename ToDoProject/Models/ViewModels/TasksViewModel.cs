using System.Collections.Generic;

namespace ToDoProject.Models.ViewModels
{
    public class TasksViewModel
    {
        public IEnumerable<Task> Tasks{ get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
        public string SearchString { get; set; }
    }
}
