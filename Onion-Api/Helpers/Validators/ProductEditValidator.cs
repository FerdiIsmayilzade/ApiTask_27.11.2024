using FluentValidation;
using Service.Helpers.DTOs.Products;

namespace Onion_Api.Helpers.Validators
{
    public class ProductEditValidator : AbstractValidator<ProductEditDto>
    {
        public ProductEditValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Name is max length 20!");
            RuleFor(x => x.Description).MaximumLength(20).WithMessage("Description is max length 20!");
            RuleFor(x => x.Description).NotNull().WithMessage("Description is required");
            RuleFor(x => x.Price).NotNull().WithMessage("Price is required");
            RuleFor(x => x.Stock).NotNull().WithMessage("Stock is required");
            RuleFor(x => x.Price).GreaterThan(100).WithMessage("Price is greater than 100$");



        }
    }
}
