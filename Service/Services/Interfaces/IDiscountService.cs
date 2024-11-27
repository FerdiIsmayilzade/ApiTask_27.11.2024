using Service.Helpers.DTOs.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountDto>> GetAllAsync();
        Task<DiscountDto> GetByIdAsync(int id);
        Task CreateAsync(DiscountCreateDto discount);
        Task EditAsync(int id, DiscountEditDto discount);
        Task DeleteAsync(int id);
        Task<IEnumerable<DiscountDto>> SearchByNameAsync(string name);
    }
}
