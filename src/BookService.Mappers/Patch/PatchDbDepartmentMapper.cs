using BookService.Models.Db;
using BookService.Models.Dto.Requests;

namespace BookService.Mappers.Patch;

public static class PatchDbDepartmentMapper
{
    public static void ApplyPatch(this DbDepartment department, UpdateDepartmentRequest request)
    {
        if (request.Name != null)
            department.Name = request.Name;
    }
}