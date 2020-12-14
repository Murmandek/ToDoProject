using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoProject.Models;
using System.Threading.Tasks;

namespace ToDoProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPersonRepository _repo;
        public AccountController(IPersonRepository r)
        {
            _repo = r;
        }

        public IActionResult Index()
        {
            return View("~/Views/Account/Authentication.cshtml");
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<ActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.CreateAsync(person);
                if (result == true)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("Login", $"Sorry, user name {person.Login} is already taken"); 
                    return View(person);
                }  
            }
            else
                return View(person);
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
