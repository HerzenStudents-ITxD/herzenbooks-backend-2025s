using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookService.Validation;

public class FindBooksRequestValidator : AbstractValidator<FindBooksRequest>, IFindBooksRequestValidator
{
    public FindBooksRequestValidator()
    {
        RuleFor(x => x.Limit)
            .GreaterThan(0).WithMessage("Limit must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Limit must not exceed 100.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1800, DateTime.UtcNow.Year).When(x => x.Year.HasValue)
            .WithMessage($"Year must be between 1800 and {DateTime.UtcNow.Year}.");
    }

    public async Task<ValidationResult> ValidateAsync(FindBooksRequest request)
    {
        return await base.ValidateAsync(request);
    }
}