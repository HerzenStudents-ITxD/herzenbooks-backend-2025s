using BookService.Models.Dto.Requests;
using FluentValidation.Results;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Validators.Interfaces;

[AutoInject]
public interface IUpdateOrderStatusRequestValidator
{
    Task<ValidationResult> ValidateAsync(UpdateOrderStatusRequest request);
}