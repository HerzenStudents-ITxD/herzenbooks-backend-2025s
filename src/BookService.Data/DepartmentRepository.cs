using BookService.Data.Interfaces;
using BookService.Data.Provider;
using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Data;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IDataProvider _provider;

    public DepartmentRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<Guid> CreateAsync(DbDepartment department)
    {
        department.Id = Guid.NewGuid();
        await _provider.Departments.AddAsync(department);
        await _provider.SaveAsync();
        return department.Id;
    }

    public async Task<DbDepartment?> GetByIdAsync(Guid id)
    {
        return await _provider.Departments
            .Include(d => d.Books)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<DbDepartment>> GetAllAsync()
    {
        return await _provider.Departments
            .Include(d => d.Books)
            .ToListAsync();
    }

    public async Task UpdateAsync(DbDepartment department)
    {
        _provider.Departments.Update(department);
        await _provider.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var department = await _provider.Departments.FindAsync(id);
        if (department != null)
        {
            _provider.Departments.Remove(department);
            await _provider.SaveAsync();
        }
    }
}