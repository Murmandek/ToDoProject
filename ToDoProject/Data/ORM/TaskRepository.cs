using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoProject.Data.ORM;
using Project = System.Threading.Tasks;

namespace ToDoProject.Models
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _db;

        public TaskRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<List<Task>> GetAllTasksAsync()
        {
            return await _db.Tasks.OrderBy(t => t.Name).AsNoTracking().ToListAsync();
        }

        public async Task<List<Task>> GetTasksAsync(string searchString)
        {
            if (searchString != null)
            {
                return await _db.Tasks.OrderBy(e => e.Name)
                                        .Where(e => e.Name.Contains(searchString)
                                                || e.Description.Contains(searchString))
                                        .AsNoTracking()
                                        .ToListAsync();
            }
            else
            {
                return await _db.Tasks.OrderBy(e => e.Name).AsNoTracking().ToListAsync();
            }
        }

        public async Task<Task> GetTaskAsync(int id)
        {
            return await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Project.Task CreateAsync(Task task, int employeeId)
        {
            int et = 0;
            et = await _db.EmployeeTasks.Where(e => e.EmployeeId == employeeId 
                                                && e.Task.Name == task.Name)
                                        .CountAsync();
            if (et == 0)
            {
                Task taskId = new Task();
                int taskCount = 0;
                taskCount = await _db.Tasks.Where(t => t.Name == task.Name).CountAsync();
                if (taskCount == 0)
                {
                    await _db.Tasks.AddAsync(task);
                    await _db.SaveChangesAsync();

                    //taskId = await _db.Tasks.OrderBy(t => t.Id).LastOrDefaultAsync();
                    taskId.Id = task.Id;
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
                await _db.EmployeeTasks.AddAsync(employeeTask);
                await _db.SaveChangesAsync();
            }
        }

        public async Project.Task CreateEmployeeTaskAsync(int taskId, int employeeId)
        {
            int countEmployeeTask = 0;
            countEmployeeTask = await _db.EmployeeTasks.Where(e => e.EmployeeId == employeeId && e.TaskId == taskId).CountAsync();
            if (countEmployeeTask == 0)
            {
                EmployeeTask employeeTask = new EmployeeTask
                {
                    EmployeeId = employeeId,
                    TaskId = taskId,
                    AppointmentDate = DateTime.UtcNow,
                    Estemate = 4
                };
                await _db.EmployeeTasks.AddAsync(employeeTask);
                await _db.SaveChangesAsync();
            }
        }

        public async Project.Task UpdateTaskAsync(Task newTask)
        {
            var oldTask = await _db.Tasks.Where(t => t.Id == newTask.Id).FirstOrDefaultAsync();
            oldTask.Name = newTask.Name;
            oldTask.Description = newTask.Description;
            await _db.SaveChangesAsync();
        }

        public async Project.Task DeleteAsync(int id)
        {
            _db.Tasks.Remove(await GetTaskAsync(id));
            await _db.SaveChangesAsync();
        }
    }
}
