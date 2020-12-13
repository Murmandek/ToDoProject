using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoProject.Models
{
    public interface IImageRepository
    {
        Task CreateAsync(ImageViewModel imageVM);
        Task<List<Image>> GetImagesAsync();
        Task<Image> GetAsync(int id);
        Task UpdateAsync(ImageViewModel newImage);
    }
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _db;
        public ImageRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<List<Image>> GetImagesAsync()
        {
            return await _db.Image.ToListAsync();
        }

        public async Task<Image> GetAsync(int id)
        {
            return await _db.Image.FirstOrDefaultAsync(t => t.EmployeeId == id);
        }

        public async Task CreateAsync(ImageViewModel imageVM)
        {
            Image image = new Image { Name = imageVM.Name, EmployeeId = imageVM.EmployeeId };
            if (imageVM.Avatar != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(imageVM.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)imageVM.Avatar.Length);
                }

                image.Avatar = imageData;
            }
            await _db.Image.AddAsync(image);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(ImageViewModel newImage)
        {
            var oldImage = await _db.Image.Where(t => t.EmployeeId == newImage.EmployeeId).FirstOrDefaultAsync();
            oldImage.Name = newImage.Name;
            
            if (newImage.Avatar != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(newImage.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)newImage.Avatar.Length);
                }
         
                oldImage.Avatar = imageData;
            }
            else
            {
                oldImage.Avatar = oldImage.Avatar;
            }
            await _db.SaveChangesAsync();
        }
    }
}
