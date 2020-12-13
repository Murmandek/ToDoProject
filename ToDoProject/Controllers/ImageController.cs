using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoProject.Models;
using Microsoft.Extensions.Logging;

namespace ToDoProject.Controllers
{
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageRepository _repo;

        public ImageController(ILogger<ImageController> logger, IImageRepository context)
        {
            _logger = logger;
            _repo = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetImagesAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ImageViewModel imageVM)
        {
            await _repo.CreateAsync(imageVM);
            return RedirectToAction("Index");
        }
    }
}
