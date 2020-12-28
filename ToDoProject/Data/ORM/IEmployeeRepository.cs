using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoProject.Models;
using Project = System.Threading.Tasks;

namespace ToDoProject.Data.ORM
{
    public interface IEmployeeRepository
    {
        Project.Task CreateAsync(Employee employee, ImageViewModel imageVM);
        Project.Task DeleteAsync(int id);
        Task<Employee> GetAsync(int id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> GetEmployeesAsync(string searchString);
        Project.Task UpdateAsync(Employee employee, ImageViewModel image);
        Task<Employee> GetEmployeeWithImage(int id);
    }
}
