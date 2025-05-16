using BookService.Data.Interfaces;
using BookService.Mappers.Db;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class CreateDepartmentCommand : ICreateDepartmentCommand
{
    private readonly IDepartmentRepository _repository;
    private readonly IAccessValidator _accessValidator;

    public CreateDepartmentCommand(
        IDepartmentRepository repository,
        IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateDepartmentRequest request)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can create departments." });

        var department = request.ToDbDepartment();
        var departmentId = await _repository.CreateAsync(department);
        return new OperationResultResponse<Guid?>(departmentId);
    }
}