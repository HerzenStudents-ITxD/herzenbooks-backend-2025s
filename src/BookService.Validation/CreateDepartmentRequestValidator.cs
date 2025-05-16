using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookService.Validation;

public class CreateDepartmentRequestValidator : AbstractValidator<CreateDepartmentRequest>, ICreateDepartmentRequestValidator
{
    public CreateDepartmentRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }

    public async Task<ValidationResult> ValidateAsync(CreateDepartmentRequest request)
    {
        return await base.ValidateAsync(request);
    }
}