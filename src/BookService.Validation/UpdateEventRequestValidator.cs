using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookService.Validation;

public class UpdateEventRequestValidator : AbstractValidator<UpdateEventRequest>, IUpdateEventRequestValidator
{
    public UpdateEventRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().When(x => x.Title != null)
            .MaximumLength(200).When(x => x.Title != null)
            .WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().When(x => x.Description != null)
            .MaximumLength(1000).When(x => x.Description != null)
            .WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.Photo)
            .Must(BeValidBase64).When(x => x.Photo != null)
            .WithMessage("Photo must be a valid Base64 string.");

        RuleFor(x => x.Link)
            .MaximumLength(500).When(x => x.Link != null)
            .WithMessage("Link must not exceed 500 characters.");

        RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow).When(x => x.StartDate.HasValue)
            .WithMessage("StartDate must be in the future.");
    }

    private bool BeValidBase64(string? base64)
    {
        if (string.IsNullOrEmpty(base64))
            return true;
        try
        {
            Convert.FromBase64String(base64);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ValidationResult> ValidateAsync(UpdateEventRequest request)
    {
        return await base.ValidateAsync(request);
    }
}