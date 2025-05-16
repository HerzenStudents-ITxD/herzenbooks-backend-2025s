using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookService.Validation;

public class AddToCartRequestValidator : AbstractValidator<AddToCartRequest>, IAddToCartRequestValidator
{
    public AddToCartRequestValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("BookId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }

    public async Task<ValidationResult> ValidateAsync(AddToCartRequest request)
    {
        return await base.ValidateAsync(request);
    }
}