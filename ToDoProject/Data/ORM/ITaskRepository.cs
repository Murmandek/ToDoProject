using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoProject.Data.ORM
{
    public interface ITaskRepository
    {
        Task<List<Models.Task>> GetAllTasksAsync();
        Task CreateAsync(Models.Task task, int employeeId);
        Task CreateEmployeeTaskAsync(int taskId, int employeeId);
        Task DeleteAsync(int id);
        Task<Models.Task> GetTaskAsync(int id);
        Task<List<Models.Task>> GetTasksAsync(string searchString);
        Task UpdateTaskAsync(Models.Task task);
    }
}
