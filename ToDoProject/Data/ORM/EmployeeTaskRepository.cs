using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoProject.Data.ORM;

namespace ToDoProject.Models
{
    public class EmployeeTaskRepository : IEmployeeTaskRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeTaskRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<EmployeeTask> GetAsync(int employeeId, int taskId)
        {
            return await _db.EmployeeTask.FirstOrDefaultAsync(t => t.EmployeeId == employeeId && t.TaskId == taskId);
        }

        public async Task<List<Tasks>> GetEmployeeTasksAsync()
        {
            var tasks = await _db.Tasks.Include(c => c.EmployeeTasks)
                                .ThenInclude(sc => sc.Employee)
                                .ToListAsync();
            return tasks;
        }

        public async Task DeleteAsync(int employeeId, int taskId)
        {
            _db.EmployeeTask.Remove(await GetAsync(employeeId, taskId));
            await _db.SaveChangesAsync();
        }
    }
}
