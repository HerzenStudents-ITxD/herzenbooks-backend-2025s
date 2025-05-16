using BookService.Data.Interfaces;
using BookService.Mappers.Patch;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class UpdateDepartmentCommand : IUpdateDepartmentCommand
{
    private readonly IDepartmentRepository _repository;
    private readonly IAccessValidator _accessValidator;

    public UpdateDepartmentCommand(
        IDepartmentRepository repository,
        IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id, UpdateDepartmentRequest request)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can update departments." });

        var department = await _repository.GetByIdAsync(id);
        if (department == null)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Department not found." });

        department.ApplyPatch(request);
        await _repository.UpdateAsync(department);
        return new OperationResultResponse<Guid?>(id);
    }
}