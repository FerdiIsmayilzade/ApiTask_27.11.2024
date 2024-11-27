using FluentValidation;
using Service.Helpers.DTOs.Colors;

namespace Onion_Api.Helpers.Validators
{
    public class ColorCreateDtoValidator : AbstractValidator<ColorCreateDto>
    {
        public ColorCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must'n be empty");
            RuleFor(x => x.Name).NotNull().WithMessage("Name must'n be null");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code must'n be empty");
            RuleFor(x => x.Code).NotNull().WithMessage("Code must'n be null");
        }
    }
}
