using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookService.Validation;

public class CreateAuthorRequestValidator : AbstractValidator<CreateAuthorRequest>, ICreateAuthorRequestValidator
{
    public CreateAuthorRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");

        RuleFor(x => x.Patronim)
            .MaximumLength(50).When(x => x.Patronim != null)
            .WithMessage("Patronim must not exceed 50 characters.");
    }

    public async Task<ValidationResult> ValidateAsync(CreateAuthorRequest request)
    {
        return await base.ValidateAsync(request);
    }
}