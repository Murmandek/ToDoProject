using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ToDoProject.Models
{
    public interface IEmployeeRepository
    {
        Task CreateAsync(Employee employee, ImageViewModel imageVM);
        Task DeleteAsync(int id);
        Task<Employee> GetAsync(int id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> GetEmployeesAsync(string searchString);
        Task UpdateAsync(Employee employee);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IImageRepository _imageRepository;
        public EmployeeRepository(ApplicationDbContext context, IImageRepository repository)
        {
            _db = context;
            _imageRepository = repository;
        }
        
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _db.Employee.OrderBy(e => e.Name).ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync(string searchString)
        {
            if (searchString != null)
            { 
                return await _db.Employee.Where(e => e.Name.Contains(searchString)
                                        || e.Position.Contains(searchString))
                                        .Include(e => e.Image)
                                        .ToListAsync(); 
            }
            else
            {
                return await _db.Employee
                            .Include(e => e.Image)
                            .ToListAsync();
            }
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _db.Employee.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateAsync(Employee employee, ImageViewModel imageVM)
        {
            await _db.Employee.AddAsync(employee);
            await _db.SaveChangesAsync();

            var empId = await _db.Employee.OrderBy(e => e.Id).LastOrDefaultAsync();
            imageVM.EmployeeId = empId.Id;
            await _imageRepository.CreateAsync(imageVM);
        }

        public async Task UpdateAsync(Employee newEmployee)
        {
            var oldEmployee = await _db.Employee.Where(t => t.Id == newEmployee.Id).FirstOrDefaultAsync();
            oldEmployee.Name = newEmployee.Name;
            oldEmployee.Age = newEmployee.Age;
            oldEmployee.Address = newEmployee.Address;
            oldEmployee.Position = newEmployee.Position;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _db.Employee.Remove(await GetAsync(id));
            await _db.SaveChangesAsync();
        }
    }
}
