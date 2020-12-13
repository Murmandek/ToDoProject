using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using ToDoProject.Models;
using ToDoProject.Models.ViewModels;

namespace ToDoProject.Controllers
{
    public class TasksController : Controller
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITaskRepository _repo;
        private readonly IEmployeeRepository _repoEmp;
      
        public TasksController(ILogger<TasksController> logger, ITaskRepository context, IEmployeeRepository contextEmp)
        {
            _logger = logger;
            _repo = context;
            _repoEmp = contextEmp;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 3;

            var tasks = await _repo.GetTasksAsync(searchString);

            var count = tasks.Count();
            var items = tasks.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            TasksViewModel tasksViewModel = new TasksViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Tasks = items,
                SearchString = searchString
            };

            return View(tasksViewModel); 
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> ClearIndex(string searchString = "", int page = 1)
        {
            int pageSize = 3;
            var tasks = await _repo.GetTasksAsync(searchString);

            var count = tasks.Count();
            var items = tasks.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            TasksViewModel tasksViewModel = new TasksViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Tasks = items,
                SearchString = ""
            };

            return View("~/Views/Tasks/Index.cshtml", tasksViewModel);   
        }

        public async Task<ActionResult> Details(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            { 
                Tasks tasks = await _repo.GetAsync(id);
                return View(tasks);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// to generate view with employees for entering task data
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var employees = await _repoEmp.GetAllEmployeesAsync();

            var etViewModel = new ETViewModel
            {
                Employees = employees
            };

            return View("~/Views/Tasks/Create.cshtml", etViewModel);
        }

        /// <summary>
        /// to save entered data
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(ETViewModel etViewModel)
        {
            var employees = await _repoEmp.GetAllEmployeesAsync();

            etViewModel.Employees = employees;

            Tasks taskObj = new Tasks
            {
                Name = etViewModel.TaskName,
                Description = etViewModel.TaskDescription
            };

            if (ModelState.IsValid)
            {
                await _repo.CreateAsync(taskObj, etViewModel.SelectedValue);
                return RedirectToAction("Create");
            }
            else
            {
                return View(etViewModel);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                Tasks tasks = await _repo.GetAsync(id);
                return View(tasks);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Tasks tasks)
        {
            await _repo.UpdateTaskAsync(tasks);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            Tasks tasks = await _repo.GetAsync(id);
            if (tasks != null)
                return View(tasks);
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
