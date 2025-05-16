using BookService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Data.Interfaces;
[AutoInject]
public interface IOrderRepository
{
    Task<Guid> CreateAsync(DbOrder order);
    Task<DbOrder?> GetByIdAsync(Guid id);
    Task<List<DbOrder>> GetByUserIdAsync(Guid userId);
    Task UpdateAsync(DbOrder order);
    Task DeleteAsync(Guid id);
    Task<List<DbOrder>> GetAllAsync();
    Task<List<DbOrder>> FilterOrdersAsync(string? status, Guid? userId, DateTime? dateFrom, DateTime? dateTo);
    Task UpdateStatusAsync(Guid id, string status);
    Task FulfillOrderAsync(Guid id);
}