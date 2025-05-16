using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookService.Validation;

public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>, ICreateEventRequestValidator
{
    public CreateEventRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.Photo)
            .Must(BeValidBase64).When(x => x.Photo != null)
            .WithMessage("Photo must be a valid Base64 string.");

        RuleFor(x => x.Link)
            .MaximumLength(500).When(x => x.Link != null)
            .WithMessage("Link must not exceed 500 characters.");

        RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("StartDate must be in the future.");
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

    public async Task<ValidationResult> ValidateAsync(CreateEventRequest request)
    {
        return await base.ValidateAsync(request);
    }
}