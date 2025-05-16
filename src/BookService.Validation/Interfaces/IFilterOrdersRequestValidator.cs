using BookService.Models.Dto.Requests;
using FluentValidation.Results;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Validators.Interfaces;

[AutoInject]
public interface IFilterOrdersRequestValidator
{
    Task<ValidationResult> ValidateAsync(FilterOrdersRequest request);
}