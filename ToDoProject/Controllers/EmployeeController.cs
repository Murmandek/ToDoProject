using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoProject.Data.ORM;
using ToDoProject.Models;
using ToDoProject.Models.ViewModels;

namespace ToDoProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _repo;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository context)
        {
            _logger = logger;
            _repo = context;
        }

        [Route("employees")]
     //   [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            int pageSize = 3;
            int pageNumder = page ?? 1;
            var employees = await _repo.GetEmployeesAsync(searchString);

            int count = employees.Count();
            var items = employees.Skip((pageNumder - 1) * pageSize).Take(pageSize).ToList();

            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                PaginationViewModel = new PaginationViewModel(count, pageNumder, pageSize),
                Employees = items,
                SearchString = searchString
            };
            return View(employeeViewModel);
        }

        public IActionResult ClearIndex()
        {
            string searchString = "";
            return RedirectToAction("Index", "Employee", searchString);
        }

        [Route("employees/info/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            Employee employee = await _repo.GetEmployeeWithImage(id);
            if (employee != null)
                return View(employee);
            return NotFound();
        }

        [Route("employees/add-employee")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("employees/add-employee")]
        public async Task<ActionResult> Create(Employee employee, ImageViewModel imageVM)
        {
            if (ModelState.IsValid)
            {
                await _repo.CreateAsync(employee, imageVM);
                return RedirectToAction("Details", new { id = employee.Id });
            }
            else
                return View(employee);
        }

        [Route("employees/edit-employee/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            Employee employeeWithImage = await _repo.GetEmployeeWithImage(id);
            if (employeeWithImage != null)
                return View(employeeWithImage);
            return NotFound();
        }

        [HttpPost]
        [Route("employees/edit-employee/{id}")]
        public async Task<ActionResult> Edit(Employee employee, ImageViewModel image)
        {
            await _repo.UpdateAsync(employee, image);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        [Route("employees/remove-employee/{id}")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            Employee employee = await _repo.GetEmployeeWithImage(id);
            if (employee != null)
                return View(employee);
            return NotFound();
        }

        [HttpPost]
        [Route("employees/remove-employee/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
