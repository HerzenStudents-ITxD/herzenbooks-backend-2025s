using BookService.Models.Dto.Requests;
using FluentValidation.Results;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Validators.Interfaces;

[AutoInject]
public interface IUpdateStockRequestValidator
{
    Task<ValidationResult> ValidateAsync(UpdateStockRequest request);
}