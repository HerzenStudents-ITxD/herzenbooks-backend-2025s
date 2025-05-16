using BookService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Data.Interfaces;
[AutoInject]
public interface IBookAuthorRepository
{
    Task<Guid> CreateAsync(DbBookAuthor bookAuthor);
    Task<List<DbBookAuthor>> GetByBookIdAsync(Guid bookId);
    Task DeleteAsync(Guid id);
}