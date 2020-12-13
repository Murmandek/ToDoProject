using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Image name")]
        public string Name { get; set; }

        public byte[] Avatar { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
