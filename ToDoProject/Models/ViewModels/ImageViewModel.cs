using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ToDoProject.Models
{
    public class ImageViewModel
    {
        public string Name { get; set; }

        public IFormFile Avatar { get; set; }
        public int EmployeeId { get; set; }
    }
}
