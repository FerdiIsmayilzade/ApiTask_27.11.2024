using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Exceptions;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Categories;
using Service.Helpers.DTOs.Products;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        
        public ProductService(IProductRepository productRepository,
                              IMapper mapper,
                              AppDbContext context,
                              IFileService fileService)
        {
            _repository = productRepository;
            _mapper = mapper;
            _context = context;
            _fileService = fileService;
        }
        public async Task CreateAsync(ProductCreateDto product)
        {
            List<ProductImage> newImages = new();

            foreach (var item in product.Files)
            {
                var fileUrl = await _fileService.UploadAsync(item);

                newImages.Add(new ProductImage
                {
                    Image = fileUrl.Response
                });
            }

            newImages.FirstOrDefault().IsMain = true;

            await _context.ProductImages.AddRangeAsync(newImages);
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                Images = newImages
            };
            await _repository.CreateAsync(newProduct);
            await _context.SaveChangesAsync();

            foreach (var item in product.ColorIds)
            {
                var color = await _context.Colors.FindAsync(item) ?? throw new NotFoundException("Color not found");

                await _context.ProductColors.AddAsync(new ProductColor { ColorId = color.Id, ProductId = newProduct.Id });

            }

            foreach (var item in product.DiscountIds)
            {
                var discount = await _context.Discounts.FindAsync(item) ?? throw new NotFoundException("BookCenter not found");

                await _context.ProductDiscounts.AddAsync(new ProductDiscount { ProductId = newProduct.Id, DiscountId = discount.Id });
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existProduct = _mapper.Map<ProductDto>(await _repository.GetByIdAsync(id, x => x.Category, x => x.Images));

            foreach (var image in existProduct.Images)
            {
               _fileService.DeleteFile(image.ToString());

            }

            await _repository.DeleteAsync(id);

        }

        public async Task EditAsync(int id, ProductEditDto product)
        {
            var existProduct = _mapper.Map<ProductDto>(await _repository.GetByIdAsync(id, x => x.Category, x => x.Images).ConfigureAwait(false));

            foreach (var image in existProduct.Images)
            {
                _fileService.DeleteFile(image.ToString());
                

            }

            List<ProductImage> newImages = new();


            foreach (var item in product.Files)
            {
                var fileUrl = await _fileService.UploadAsync(item);

                newImages.Add(new ProductImage
                {
                    Image = fileUrl.Response
                });
            }

            newImages.FirstOrDefault().IsMain = true;

            _mapper.Map<Product>(existProduct).Images = newImages.ToList();
           

            _mapper.Map(product, existProduct);

            await _repository.EditAsync(_mapper.Map<Product>(existProduct));
        }

        //public async Task<IEnumerable<ProductDto>> GetAllAsync()
        //{
        //    return _mapper.Map<IEnumerable<ProductDto>>(await _repository.GetAllAsync());

        //}

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products =
         await this._repository
                   .GetAllWithCategoryAsync(x => x.Category,x=>x.Images)
                            
         .ConfigureAwait(false);
            
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }


        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return _mapper.Map<ProductDto>(await _repository.GetByIdAsync(id,x=>x.Category,x=>x.Images).ConfigureAwait(false));

        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(string str)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _repository.GetAllWithExpression(m => m.Name.Trim().ToLower()
                                                                                                    .Contains(str.Trim().ToLower())));
        }
    }
}
