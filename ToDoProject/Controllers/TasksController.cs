using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using ToDoProject.Data.ORM;
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

        [Route("tasks")]
     //   [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            int pageSize = 3;
            int pageNumder = page ?? 1;
            var tasks = await _repo.GetTasksAsync(searchString);

            int count = tasks.Count();
            var items = tasks.Skip((pageNumder - 1) * pageSize).Take(pageSize).ToList();

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
            return RedirectToAction("Index", "Tasks", searchString);
        }

        [Route("tasks/info/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                return View(await _repo.GetTaskAsync(id));
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// to generate view with employees for entering task data
        /// </summary>
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        [HttpGet]
        [Route("tasks/add-task")]
        public async Task<ActionResult> Create()
        {
            CreateTaskViewModel createTaskViewModel = new CreateTaskViewModel
            {
                Employees = await _repoEmp.GetAllEmployeesAsync()
            };

            return View(createTaskViewModel);
        }

        /// <summary>
        /// to save entered data
        /// </summary>
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        [HttpPost]
        [Route("tasks/add-task")]
        public async Task<ActionResult> Create(CreateTaskViewModel createTaskViewModel)
        {
            Models.Task task = new Models.Task
            {
                Name = createTaskViewModel.TaskName,
                Description = createTaskViewModel.TaskDescription
            };

            if (ModelState.IsValid)
            {
                await _repo.CreateAsync(task, createTaskViewModel.SelectedValue);
                return RedirectToAction("Index");
            }
            else
            {
                createTaskViewModel.Employees = await _repoEmp.GetAllEmployeesAsync();
                return View(createTaskViewModel);
            }
        }

        [Route("tasks/edit-task/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                return View(await _repo.GetTaskAsync(id));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("tasks/edit-task/{id}")]
        public async Task<ActionResult> Edit(Models.Task tasks)
        {
            await _repo.UpdateTaskAsync(tasks);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        [Route("tasks/remove-task/{id}")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            Models.Task tasks = await _repo.GetTaskAsync(id);
            if (tasks != null)
                return View(tasks);
            return NotFound();
        }

        [HttpPost]
        [Route("tasks/remove-task/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
