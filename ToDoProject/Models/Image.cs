using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual Employee Employee { get; set; }
    }
}
