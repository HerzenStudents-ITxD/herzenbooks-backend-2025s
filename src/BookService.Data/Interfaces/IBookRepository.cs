using BookService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Data.Interfaces;
[AutoInject]
public interface IBookRepository
{
    Task<Guid> CreateAsync(DbBook book);
    Task<DbBook?> GetByIdAsync(Guid id);
    Task<List<DbBook>> GetAllAsync();
    Task<List<DbBook>> FindBooksAsync(string? search, Guid? departmentId, int? year, int limit);
    Task<List<DbBook>> GetRecommendationsAsync(Guid bookId, int limit);
    Task<List<DbBook>> GetFeaturedBooksAsync(int limit);
    Task UpdateAsync(DbBook book);
    Task UpdateStockAsync(Guid bookId, int Quantity);
    Task DeleteAsync(Guid id);
}