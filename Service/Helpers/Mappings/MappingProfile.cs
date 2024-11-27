using AutoMapper;
using Domain.Entities;
using Service.Helpers.DTOs.Categories;
using Service.Helpers.DTOs.Colors;
using Service.Helpers.DTOs.Discounts;
using Service.Helpers.DTOs.Products;

namespace Service.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Categories
            CreateMap<Category, CategoryDto>()
                           .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("MM/dd/yyyy HH:mm")));

            CreateMap<CategoryCreateDto, Category>();

            CreateMap<CategoryEditDto, Category>()
                           .ForAllMembers(opts =>
                           {
                               opts.AllowNull();
                               opts.Condition((src, dest, srcMember) => srcMember != null);
                           });


            #endregion

            #region Products
            CreateMap<Product, ProductDto>()
                           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                           .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
            CreateMap<ProductEditDto, Product>()
              .ForMember(dest => dest.Images, opt => opt.Ignore())
              .ForAllMembers(opts =>
              {
                  opts.AllowNull();
                  opts.Condition((src, dest, srcMember) => srcMember != null);
              });
            #endregion



            #region Colors
            CreateMap<Color, ColorDto>();
            CreateMap<ColorCreateDto, Color>();
            CreateMap<ColorEditDto, Color>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            #endregion

            #region Discount
            CreateMap<Discount, DiscountDto>()
                .ForMember(dest => dest.ProductNames, opt => opt.MapFrom(src => src.ProductDiscounts.Select(x => x.Product.Name)));
            CreateMap<DiscountCreateDto, Discount>();
            CreateMap<DiscountEditDto, Discount>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            #endregion
        }
    }
}
    