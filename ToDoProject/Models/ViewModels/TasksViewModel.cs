using System.Collections.Generic;

namespace ToDoProject.Models.ViewModels
{
    public class TasksViewModel
    {
        public IEnumerable<Tasks> Tasks{ get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string SearchString { get; set; }
    }
}
