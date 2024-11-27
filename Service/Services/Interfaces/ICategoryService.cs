using Domain.Entities;
using Service.Helpers.DTOs.Categories;

namespace Service.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateDto category);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id,CategoryEditDto category);
        Task<IEnumerable<CategoryDto>> SearchAsync(string str);

    }
}
