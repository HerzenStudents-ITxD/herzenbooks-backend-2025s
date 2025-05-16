using BookService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Data.Interfaces;
[AutoInject]
public interface IDepartmentRepository
{
    Task<Guid> CreateAsync(DbDepartment department);
    Task<DbDepartment?> GetByIdAsync(Guid id);
    Task<List<DbDepartment>> GetAllAsync();
    Task UpdateAsync(DbDepartment department);
    Task DeleteAsync(Guid id);
}