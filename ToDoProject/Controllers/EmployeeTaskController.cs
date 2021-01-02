using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ToDoProject.Data.ORM;
using ToDoProject.Models;
using ToDoProject.Models.ViewModels;

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

        [Route("employees-tasks")]
     //   [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            int pageSize = 3;
            int pageNumder = page ?? 1;
            var employeeTasks = await _repo.GetEmployeeTasksAsync(searchString);

            int count = employeeTasks.Count();
            var items = employeeTasks.Skip((pageNumder - 1) * pageSize).Take(pageSize).ToList();

            TasksViewModel tasksViewModel = new TasksViewModel
            {
                PaginationViewModel = new PaginationViewModel(count, pageNumder, pageSize),
                Tasks = items,
                SearchString = searchString
            };

            return View(tasksViewModel);
        }

        public IActionResult ClearIndex()
        {
            string searchString = "";
            return RedirectToAction("Index", "EmployeeTask", searchString);
        }


        [Route("employees-tasks/add-employee-task")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            EmployeeTaskViewModel employeeTaskViewModel = new EmployeeTaskViewModel
            {
                Employees = await _repoEmp.GetAllEmployeesAsync(),
                Tasks = await _repoTask.GetAllTasksAsync()
            };

            return View(employeeTaskViewModel);
        }

        /// <summary>
        /// to save entered data
        /// </summary>
        [Route("employees-tasks/add-employee-task")]
        [HttpPost]
        public async Task<ActionResult> Create(EmployeeTaskViewModel employeeTaskViewModel) 
        {
            if (ModelState.IsValid) 
            {
                await _repoTask.CreateEmployeeTaskAsync(employeeTaskViewModel.TaskSelectedValue, employeeTaskViewModel.EmployeeSelectedValue);
                return RedirectToAction("Index");
            }
            else
            {
                employeeTaskViewModel.Employees = await _repoEmp.GetAllEmployeesAsync();
                return View(employeeTaskViewModel);
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
