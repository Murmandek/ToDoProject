using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class User
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "LoginEmailAddress")]
        [Required(ErrorMessage = "LoginRequired")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "PasswordRequired")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
