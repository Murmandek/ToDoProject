using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoProject.Models;

namespace ToDoProject.Data.ORM
{
    public interface IImageRepository
    {
        System.Threading.Tasks.Task CreateAsync(ImageViewModel imageVM);
        Task<List<Image>> GetImagesAsync();
        Task<Image> GetAsync(int id);
        System.Threading.Tasks.Task UpdateAsync(ImageViewModel newImage);
    }
}
