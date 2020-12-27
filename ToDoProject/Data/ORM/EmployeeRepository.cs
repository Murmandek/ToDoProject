using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoProject.Data.ORM;
using System.IO;

namespace ToDoProject.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _db = context;
        }
        
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _db.Employees.OrderBy(e => e.Name).ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync(string searchString)
        {
            if (searchString != null)
            { 
                return await _db.Employees.Where(e => e.Name.Contains(searchString)
                                        || e.Position.Contains(searchString))
                                        .Include(e => e.Image)
                                        .AsNoTracking()
                                        .ToListAsync(); 
            }
            else
            {
                return await _db.Employees.Include(e => e.Image)
                                        .AsNoTracking()
                                        .ToListAsync();
            }
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> GetEmployeeWithImage(int id)
        {
            return await _db.Employees.Include(e => e.Image)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async System.Threading.Tasks.Task CreateAsync(Employee employee, ImageViewModel imageVM)
        {
            Image image = new Image
            {
                Name = imageVM.Name,
                EmployeeId = employee.Id
            };
             
            if (imageVM.Avatar != null)
            {
                byte[] imageData = null;

                using (BinaryReader binaryReader = new BinaryReader(imageVM.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)imageVM.Avatar.Length);
                }

                image.Avatar = imageData;
            }

            employee.Image = image;

            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Employee newEmployee, ImageViewModel newImage)
        {
            Employee oldEmployee = await _db.Employees.Where(e => e.Id == newEmployee.Id)
                                                    .Include(i => i.Image)
                                                    .FirstOrDefaultAsync();
            oldEmployee.Name = newEmployee.Name;
            oldEmployee.Age = newEmployee.Age;
            oldEmployee.Address = newEmployee.Address;
            oldEmployee.Position = newEmployee.Position;

            oldEmployee.Image.Name = newImage.Name;

            if (newImage.Avatar != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(newImage.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)newImage.Avatar.Length);
                }

                oldEmployee.Image.Avatar = imageData;
            }
            else
            {
                oldEmployee.Image.Avatar = oldEmployee.Image.Avatar;
            }
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            _db.Employees.Remove(await GetAsync(id));
            await _db.SaveChangesAsync();
        }
    }
}
