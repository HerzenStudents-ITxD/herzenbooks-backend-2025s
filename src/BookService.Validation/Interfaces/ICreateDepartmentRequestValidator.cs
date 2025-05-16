using BookService.Models.Dto.Requests;
using FluentValidation.Results;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Validators.Interfaces;

[AutoInject]
public interface ICreateDepartmentRequestValidator
{
    Task<ValidationResult> ValidateAsync(CreateDepartmentRequest request);
}