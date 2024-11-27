using AutoMapper;
using Domain.Entities;
using Repository.Exceptions;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Colors;
using Service.Helpers.DTOs.Products;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repo;
        private readonly IMapper _mapper;
        public ColorService(IColorRepository repo,
                            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task CreateAsync(ColorCreateDto color)
        {
            await _repo.CreateAsync(_mapper.Map<Color>(color));

        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);

        }

        public async Task EditAsync(int id, ColorEditDto color)
        {
            var existColor = await _repo.GetByIdAsync(id);

            if (existColor == null) throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            _mapper.Map(color, existColor);

            await _repo.EditAsync(existColor);
        }

        public async Task<IEnumerable<ColorDto>> GetAllAsync()
        {
            var products =
                await this._repo
                          .GetAllWithProductAsync(x => x.ProductColors)

                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<ColorDto>>(products);
        }

        public async Task<ColorDto> GetByIdAsync(int id)
        {
            return _mapper.Map<ColorDto>(await _repo.GetByIdAsync(id, x => x.ProductColors).ConfigureAwait(false));

        }

        public async Task<IEnumerable<ColorDto>> SearchByNameAsync(string name)
        {
            return _mapper.Map<IEnumerable<ColorDto>>(await _repo.GetAllWithExpression(x => x.Name.Trim()
                                                                                               .ToLower()
                                                                                               .Contains(name.ToLower()
                                                                                                             .Trim())));
        }
    }
}
