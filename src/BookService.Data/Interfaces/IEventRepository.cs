using BookService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Data.Interfaces;
[AutoInject]
public interface IEventRepository
{
    Task<Guid> CreateAsync(DbEvent evt);
    Task<DbEvent?> GetByIdAsync(Guid id);
    Task<List<DbEvent>> GetAllAsync();
    Task UpdateAsync(DbEvent evt);
    Task DeleteAsync(Guid id);
    Task<List<DbEvent>> GetActiveEventsAsync();
    Task<List<DbEvent>> GetUpcomingEventsAsync(int limit);
}