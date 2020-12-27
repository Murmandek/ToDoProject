using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class User
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "Incorrect email address")]
        [Required(ErrorMessage = "Please enter employee name")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}
