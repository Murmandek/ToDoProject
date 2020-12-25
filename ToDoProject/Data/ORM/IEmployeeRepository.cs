using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Data.ORM
{
    public interface IEmployeeRepository
    {
        Task CreateAsync(Employee employee, ImageViewModel imageVM);
        Task DeleteAsync(int id);
        Task<Employee> GetAsync(int id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> GetEmployeesAsync(string searchString);
        Task UpdateAsync(Employee employee, ImageViewModel image);
        Task<Employee> GetEmployeeWithImage(int id);
    }
}
