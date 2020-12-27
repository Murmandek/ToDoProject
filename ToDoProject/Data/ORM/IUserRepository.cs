using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Data.ORM
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(User user);
        List<User> GetUsers();

        ClaimsIdentity GetIdentity(string username, string password);
    }
}
