using FluentValidation;
using Service.Helpers.DTOs.Categories;

namespace Onion_Api.Helpers.Validators
{
    public class CategoryEditValidator : AbstractValidator<CategoryEditDto>
    {
        public CategoryEditValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Name is max length 20!");

        }
    }
}
