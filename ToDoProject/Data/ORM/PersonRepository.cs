using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoProject.Models
{
    public interface IPersonRepository
    {
        Task<bool> CreateAsync(Person people);
        List<Person> GetPerson();
    }
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<bool> CreateAsync(Person people)
        {
            try
            {
                await _db.Person.AddAsync(people);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Person> GetPerson()
        {
            return _db.Person.ToList();
        }
    }
}
