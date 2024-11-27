using Service.Helpers.DTOs.Categories;
using Service.Helpers.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateDto product);
       
        Task<ProductDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, ProductEditDto product);
        Task<IEnumerable<ProductDto>> SearchAsync(string str);
    
        Task<IEnumerable<ProductDto>> GetAllAsync();

    }
}
