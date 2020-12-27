using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Data.ORM
{
    public interface IEmployeeRepository
    {
        System.Threading.Tasks.Task CreateAsync(Employee employee, ImageViewModel imageVM);
        System.Threading.Tasks.Task DeleteAsync(int id);
        Task<Employee> GetAsync(int id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> GetEmployeesAsync(string searchString);
        System.Threading.Tasks.Task UpdateAsync(Employee employee, ImageViewModel image);
        Task<Employee> GetEmployeeWithImage(int id);
    }
}
