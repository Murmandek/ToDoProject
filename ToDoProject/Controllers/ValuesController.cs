using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ToDoProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            ViewData["Message"] = $"{User.Identity.Name}";
            return Ok($"Your login: {User.Identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Your role: administrator");
        }
    }
}
