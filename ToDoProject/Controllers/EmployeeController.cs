using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoProject.Models;
using ToDoProject.Models.ViewModels;

namespace ToDoProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _repo;
        private readonly IImageRepository _repoImage;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository context, IImageRepository contextImage)
        {
            _logger = logger;
            _repo = context;
            _repoImage = contextImage;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 3;
            var employees = await _repo.GetEmployeesAsync(searchString);

            var count = employees.Count();
            var items = employees.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                PaginationViewModel = new PaginationViewModel(count, page, pageSize),
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

        public async Task<ActionResult> Details(int id)
        {
            Employee employee = await _repo.GetAsync(id);
            if (employee != null)
                return View(employee);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Employee employee, ImageViewModel imageVM)
        {
            if (ModelState.IsValid)
            {
                await _repo.CreateAsync(employee, imageVM);
                return RedirectToAction("Index");
            }
            else
                return View(employee);
        }

        public async Task<ActionResult> Edit(int id)
        {
            Employee employee = await _repo.GetAsync(id);
            Image image = await _repoImage.GetAsync(employee.Id);
            var res = new EmployeeImageViewModel 
            { 
                Employee = employee, 
                Image = image 
            };
            if (employee != null)
                return View(res);
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Employee employee, ImageViewModel image)
        {
            await _repo.UpdateAsync(employee);
            await _repoImage.UpdateAsync(image);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            Employee employee = await _repo.GetAsync(id);
            if (employee != null)
                return View(employee);
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
