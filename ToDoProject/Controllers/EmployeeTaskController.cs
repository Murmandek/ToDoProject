using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Controllers
{
    public class EmployeeTaskController : Controller
    {
        private readonly IEmployeeTaskRepository _repo;
        private readonly IEmployeeRepository _repoEmp;
        private readonly ITaskRepository _repoTask;

        public EmployeeTaskController(IEmployeeTaskRepository repo, IEmployeeRepository repoEmp, ITaskRepository repoTask)
        {
            _repo = repo;
            _repoEmp = repoEmp;
            _repoTask = repoTask;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetEmployeeTasksAsync()); 
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var employees = await _repoEmp.GetAllEmployeesAsync();
            var tasks = await _repoTask.GetAllTasksAsync();

            EmployeeTaskViewModel etViewModel = new EmployeeTaskViewModel
            {
                Employees = employees,
                Taskss = tasks
            };

            return View(etViewModel); //"Create"
        }
        
        /// <summary>
        /// to save entered data
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(EmployeeTaskViewModel etViewModel) 
        {
            var employees = await _repoEmp.GetAllEmployeesAsync();
            etViewModel.Employees = employees;

            if (ModelState.IsValid) 
            {
                await _repoTask.CreateEmployeeTaskAsync(etViewModel.TaskSelectedValue, etViewModel.EmployeeSelectedValue);
                return RedirectToAction("Index");
            }
            else
            {
                return View(etViewModel);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int employeeId, int taskId)
        {
            await _repo.DeleteAsync(employeeId, taskId);
            return RedirectToAction("Index");
        }
    }
}
