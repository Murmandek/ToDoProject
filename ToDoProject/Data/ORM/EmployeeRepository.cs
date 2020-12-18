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
                                        .AsNoTracking()
                                        .ToListAsync(); 
            }
            else
            {
                return await _db.Employee
                            .Include(e => e.Image)
                            .AsNoTracking()
                            .ToListAsync();
            }
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _db.Employee.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> GetEmployeeWithImage(int id)
        {
            return await _db.Employee.Include(e => e.Image).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(Employee employee, ImageViewModel imageVM)
        {
            await _db.Employee.AddAsync(employee);
            await _db.SaveChangesAsync();

            var image = new Image
            {
                Name = imageVM.Name,
                EmployeeId = employee.Id
            };
             
            if (imageVM.Avatar != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(imageVM.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)imageVM.Avatar.Length);
                }

                image.Avatar = imageData;
            }

            await _db.Image.AddAsync(image);
            await _db.SaveChangesAsync();




            
            /*IT'S WORKING*/
            //   var empId = await _db.Employee.OrderBy(e => e.Id).LastOrDefaultAsync();
          /*  imageVM.EmployeeId = employee.Id;                  //empId.Id;
            await _imageRepository.CreateAsync(imageVM);*/
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
