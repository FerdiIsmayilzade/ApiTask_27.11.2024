using FluentValidation;
using Service.Helpers.DTOs.Colors;
using Service.Helpers.DTOs.Discounts;

namespace Onion_Api.Helpers.Validators
{
    public class DiscountCreateDtoValidator : AbstractValidator<DiscountCreateDto>
    {
        public DiscountCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must'n be empty");
            RuleFor(x => x.Name).NotNull().WithMessage("Name must'n be null");
            RuleFor(x => x.Percent).NotEmpty().WithMessage("Code must'n be empty");
            RuleFor(x => x.Percent).NotNull().WithMessage("Code must'n be null");
        }
    }
}
