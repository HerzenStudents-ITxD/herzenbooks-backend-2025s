using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetDepartmentsCommand : IGetDepartmentsCommand
{
    private readonly IDepartmentRepository _repository;

    public GetDepartmentsCommand(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<List<DepartmentResponse>>> ExecuteAsync()
    {
        var departments = await _repository.GetAllAsync();
        var response = departments.Select(d => d.ToDepartmentResponse()).ToList();
        return new OperationResultResponse<List<DepartmentResponse>>(response);
    }
}