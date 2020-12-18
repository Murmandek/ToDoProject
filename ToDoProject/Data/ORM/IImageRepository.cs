using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Data.ORM
{
    public interface IImageRepository
    {
        Task CreateAsync(ImageViewModel imageVM);
        Task<List<Image>> GetImagesAsync();
        Task<Image> GetAsync(int id);
        Task UpdateAsync(ImageViewModel newImage);
    }
}
