using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookService.Validation;

public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequest>, IUpdateOrderStatusRequestValidator
{
    public UpdateOrderStatusRequestValidator()
    {
        //RuleFor(x => x.Status)
        //    .IsInEnum().WithMessage("Status must be a valid OrderStatusType value.");
    }

    public async Task<ValidationResult> ValidateAsync(UpdateOrderStatusRequest request)
    {
        return await base.ValidateAsync(request);
    }
}