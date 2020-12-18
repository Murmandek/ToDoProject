using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoProject.Data.ORM;

namespace ToDoProject.Models
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<bool> CreateAsync(Person person)
        {
            try
            {
                await _db.Person.AddAsync(person);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Person> GetPersons()
        {
            return _db.Person.ToList();
        }

        public ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = GetPersons().FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // if user not found
            return null;
        }
    }
}
