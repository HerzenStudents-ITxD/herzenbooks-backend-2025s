using BookService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Data.Interfaces;
[AutoInject]
public interface IAuthorRepository
{
    Task<Guid> CreateAsync(DbAuthor author);
    Task<DbAuthor?> GetByIdAsync(Guid id);
    Task<List<DbAuthor>> GetAllAsync();
    Task<List<DbAuthor>> GetByBookIdAsync(Guid bookId);
    Task UpdateAsync(DbAuthor author);
    Task DeleteAsync(Guid id);
}