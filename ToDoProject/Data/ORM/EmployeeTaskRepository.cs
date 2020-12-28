using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoProject.Data.ORM;
using System.Linq;
using Project = System.Threading.Tasks;

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
            return await _db.EmployeeTasks.FirstOrDefaultAsync(t => t.EmployeeId == employeeId && t.TaskId == taskId);
        }

        public async Task<List<Task>> GetEmployeeTasksAsync(string searchString)
        {
            if (searchString != null)
            {
                return await _db.Tasks.Where(t => t.Name.Contains(searchString))
                                    .Include(c => c.EmployeeTasks)
                                    .ThenInclude(sc => sc.Employee)
                                    .AsNoTracking()
                                    .ToListAsync();
            }
            else
            {
                return await _db.Tasks
                                    .Include(c => c.EmployeeTasks)
                                    .ThenInclude(sc => sc.Employee)
                                    .AsNoTracking()
                                    .ToListAsync();
            }
        }

        public async Project.Task DeleteAsync(int employeeId, int taskId)
        {
            _db.EmployeeTasks.Remove(await GetAsync(employeeId, taskId));
            await _db.SaveChangesAsync();
        }
    }
}
