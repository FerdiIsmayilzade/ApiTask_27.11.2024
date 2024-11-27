using AutoMapper;
using Domain.Entities;
using Repository.Exceptions;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Colors;
using Service.Helpers.DTOs.Discounts;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task CreateAsync(DiscountCreateDto discount)
        {
            await _repository.CreateAsync(_mapper.Map<Discount>(discount));

        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task EditAsync(int id, DiscountEditDto discount)
        {
            var existDiscount = await _repository.GetByIdAsync(id);

            if (existDiscount == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            _mapper.Map(discount, existDiscount);

            await _repository.EditAsync(existDiscount);
        }

        public async Task<IEnumerable<DiscountDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<DiscountDto>>(await _repository.GetAllAsync());
        }

        public async Task<DiscountDto> GetByIdAsync(int id)
        {
            return _mapper.Map<DiscountDto>(await _repository.GetByIdAsync(id, x => x.ProductDiscounts).ConfigureAwait(false));

        }

        public async Task<IEnumerable<DiscountDto>> SearchByNameAsync(string name)
        {
            return _mapper.Map<IEnumerable<DiscountDto>>(await _repository.GetAllWithExpression(x =>
                x.Name.Trim().ToLower().Contains(name.Trim().ToLower())));
        }
    }
}
