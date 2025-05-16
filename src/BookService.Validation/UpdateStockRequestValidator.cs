using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookService.Validation;

public class UpdateStockRequestValidator : AbstractValidator<UpdateStockRequest>, IUpdateStockRequestValidator
{
    public UpdateStockRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be non-negative.");
    }

    public async Task<ValidationResult> ValidateAsync(UpdateStockRequest request)
    {
        return await base.ValidateAsync(request);
    }
}