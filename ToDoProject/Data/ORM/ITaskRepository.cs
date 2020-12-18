using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Data.ORM
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetAllTasksAsync();
        Task CreateAsync(Tasks task, int employeeId);
        Task CreateEmployeeTaskAsync(int taskId, int employeeId);
        Task DeleteAsync(int id);
        Task<Tasks> GetAsync(int id);
        Task<List<Tasks>> GetTasksAsync(string searchString);
        Task UpdateTaskAsync(Tasks task);
    }
}
