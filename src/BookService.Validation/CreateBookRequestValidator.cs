using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookService.Validation;

public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>, ICreateBookRequestValidator
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.ShortDescription)
            .MaximumLength(500).When(x => x.ShortDescription != null)
            .WithMessage("ShortDescription must not exceed 500 characters.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");

        RuleFor(x => x.PublicationDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Publication date cannot be in the future.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).When(x => x.Quantity.HasValue)
            .WithMessage("Quantity must be non-negative.");

        RuleFor(x => x.Photo)
            .Must(BeValidBase64).When(x => x.Photo != null)
            .WithMessage("Photo must be a valid Base64 string.");
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

    public async Task<ValidationResult> ValidateAsync(CreateBookRequest request)
    {
        return await base.ValidateAsync(request);
    }
}