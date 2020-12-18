using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoProject.Data.ORM;

namespace ToDoProject.Models
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _db;

        public TaskRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<List<Tasks>> GetAllTasksAsync()
        {
            return await _db.Tasks.OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<List<Tasks>> GetTasksAsync(string searchString)
        {
            if (searchString != null)
            {
                return await _db.Tasks.OrderBy(e => e.Name).Where(e => e.Name.Contains(searchString)
                                                                || e.Description.Contains(searchString)).ToListAsync();
            }
            else
            {
                return await _db.Tasks.OrderBy(e => e.Name).ToListAsync();
            }
        }

        public async Task<Tasks> GetAsync(int id)
        {
            return await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateAsync(Tasks task, int employeeId)
        {
            int et = 0;
            et = await _db.EmployeeTask.Where(e => e.EmployeeId == employeeId && e.Tasks.Name == task.Name).CountAsync();
            if (et == 0)
            {
                Tasks taskId;
                int taskCount = 0;
                taskCount = await _db.Tasks.Where(t => t.Name == task.Name).CountAsync();
                if (taskCount == 0)
                {
                    await _db.Tasks.AddAsync(task);
                    await _db.SaveChangesAsync();

                    taskId = await _db.Tasks.OrderBy(t => t.Id).LastOrDefaultAsync();
                }
                else
                {
                    taskId = await _db.Tasks.Where(t => t.Name == task.Name).LastOrDefaultAsync();
                }

                var employeeTask = new EmployeeTask
                {
                    EmployeeId = employeeId,
                    TaskId = taskId.Id,
                    AppointmentDate = DateTime.UtcNow,
                    Estemate = 4
                };
                await _db.EmployeeTask.AddAsync(employeeTask);
                await _db.SaveChangesAsync();
            }
        }

        public async Task CreateEmployeeTaskAsync(int taskId, int employeeId)
        {
            int et = 0;
            et = await _db.EmployeeTask.Where(e => e.EmployeeId == employeeId && e.TaskId == taskId).CountAsync();
            if (et == 0)
            {
                var employeeTask = new EmployeeTask
                {
                    EmployeeId = employeeId,
                    TaskId = taskId,
                    AppointmentDate = DateTime.UtcNow,
                    Estemate = 4
                };
                await _db.EmployeeTask.AddAsync(employeeTask);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateTaskAsync(Tasks newTask)
        {
            var oldTask = await _db.Tasks.Where(t => t.Id == newTask.Id).FirstOrDefaultAsync();
            oldTask.Name = newTask.Name;
            oldTask.Description = newTask.Description;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _db.Tasks.Remove(await GetAsync(id));
            await _db.SaveChangesAsync();
        }
    }
}
