using BookService.Models.Db;
using BookService.Models.Dto.Responses;

namespace BookService.Mappers.Responses;

public static class DepartmentResponseMapper
{
    public static DepartmentResponse ToDepartmentResponse(this DbDepartment department)
    {
        return new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name
        };
    }
}