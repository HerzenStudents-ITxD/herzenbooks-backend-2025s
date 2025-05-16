using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookService.Validation;

public class FilterOrdersRequestValidator : AbstractValidator<FilterOrdersRequest>, IFilterOrdersRequestValidator
{
    public FilterOrdersRequestValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().When(x => x.Status.HasValue)
            .WithMessage("Status must be a valid OrderStatusType value.");

        RuleFor(x => x.DateFrom)
            .LessThanOrEqualTo(x => x.DateTo).When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
            .WithMessage("DateFrom must be less than or equal to DateTo.");
    }

    public async Task<ValidationResult> ValidateAsync(FilterOrdersRequest request)
    {
        return await base.ValidateAsync(request);
    }
}