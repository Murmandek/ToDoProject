using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ToDoProject.Models;
using System.Threading.Tasks;
using ToDoProject.Data.ORM;

namespace ToDoProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _repo;
        
        /// <summary>
        /// Some comment 
        /// </summary>
        /// <param name="context"></param>
        public AccountController(IUserRepository context)
        {
            _repo = context;
        }

        public IActionResult Index()
        {
            return View("Authentication"); 
        }

        [Route("registration")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.CreateAsync(user);
                if (result == true)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("Login", $"Sorry, user name {user.Login} is already taken"); 
                    return View(user);
                }  
            }
            else
                return View(user);
        }

        [HttpPost("/token")]
        public IActionResult Token(string username, string password)
        {
            var identity = _repo.GetIdentity(username, password);
            if (ModelState.IsValid)
            {
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username or password." });
                }

                var now = DateTime.UtcNow;
                // create JWT-token
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };

                return Json(response);
            }
            else
            {
                var now = DateTime.UtcNow;
                // create JWT-token
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };

                ModelState.AddModelError("Login", $"Incorrect user name");
                return Json(response);
            }
        }
    }
}
