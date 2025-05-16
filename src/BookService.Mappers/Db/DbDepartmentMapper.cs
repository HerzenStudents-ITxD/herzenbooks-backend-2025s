using BookService.Models.Db;
using BookService.Models.Dto.Requests;

namespace BookService.Mappers.Db;

public static class DbDepartmentMapper
{
    public static DbDepartment ToDbDepartment(this CreateDepartmentRequest request)
    {
        return new DbDepartment
        {
            Name = request.Name
        };
    }
}