using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Data.ORM
{
    public interface IEmployeeTaskRepository
    {
        Task<List<Models.Task>> GetEmployeeTasksAsync(string searchString);
        System.Threading.Tasks.Task DeleteAsync(int employeeId, int taskId);
        Task<EmployeeTask> GetAsync(int employeeId, int taskId);
    }
}
