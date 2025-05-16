using BookService.Models.Dto.Requests;
using BookService.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookService.Validation;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>, ICreateOrderRequestValidator
{
    public CreateOrderRequestValidator()
    {
    
    }

    public async Task<ValidationResult> ValidateAsync(CreateOrderRequest request)
    {
        return await base.ValidateAsync(request);
    }
}