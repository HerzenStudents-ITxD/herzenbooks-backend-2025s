using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookService.Validation;

public class UpdateDepartmentRequestValidator : AbstractValidator<UpdateDepartmentRequest>, IUpdateDepartmentRequestValidator
{
    public UpdateDepartmentRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().When(x => x.Name != null)
            .MaximumLength(100).When(x => x.Name != null)
            .WithMessage("Name must not exceed 100 characters.");
    }

    public async Task<ValidationResult> ValidateAsync(UpdateDepartmentRequest request)
    {
        return await base.ValidateAsync(request);
    }
}