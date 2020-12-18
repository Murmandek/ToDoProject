using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Data.ORM
{
    public interface IPersonRepository
    {
        Task<bool> CreateAsync(Person people);
        List<Person> GetPersons();

        ClaimsIdentity GetIdentity(string username, string password);
    }
}
