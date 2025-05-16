using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookService.Validation;

public class UpdateCartQuantityRequestValidator : AbstractValidator<UpdateCartQuantityRequest>, IUpdateCartQuantityRequestValidator
{
    public UpdateCartQuantityRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }

    public async Task<ValidationResult> ValidateAsync(UpdateCartQuantityRequest request)
    {
        return await base.ValidateAsync(request);
    }
}