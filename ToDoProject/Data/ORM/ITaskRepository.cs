using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Data.ORM
{
    public interface ITaskRepository
    {
        Task<List<Models.Task>> GetAllTasksAsync();
        System.Threading.Tasks.Task CreateAsync(Models.Task task, int employeeId);
        System.Threading.Tasks.Task CreateEmployeeTaskAsync(int taskId, int employeeId);
        System.Threading.Tasks.Task DeleteAsync(int id);
        Task<Models.Task> GetTaskAsync(int id);
        Task<List<Models.Task>> GetTasksAsync(string searchString);
        System.Threading.Tasks.Task UpdateTaskAsync(Models.Task task);
    }
}
