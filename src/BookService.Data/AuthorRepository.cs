using BookService.Data.Interfaces;
using BookService.Data.Provider;
using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Data;

public class AuthorRepository : IAuthorRepository
{
    private readonly IDataProvider _provider;

    public AuthorRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<Guid> CreateAsync(DbAuthor author)
    {
        author.Id = Guid.NewGuid();
        await _provider.Authors.AddAsync(author);
        await _provider.SaveAsync();
        return author.Id;
    }

    public async Task<DbAuthor?> GetByIdAsync(Guid id)
    {
        return await _provider.Authors
            .Include(a => a.BookAuthors).ThenInclude(ba => ba.Book)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<DbAuthor>> GetAllAsync()
    {
        return await _provider.Authors
            .Include(a => a.BookAuthors).ThenInclude(ba => ba.Book)
            .ToListAsync();
    }

    public async Task<List<DbAuthor>> GetByBookIdAsync(Guid bookId)
    {
        return await _provider.BookAuthors
            .Include(ba => ba.Author)
            .Where(ba => ba.BookId == bookId)
            .Select(ba => ba.Author)
            .ToListAsync();
    }

    public async Task UpdateAsync(DbAuthor author)
    {
        _provider.Authors.Update(author);
        await _provider.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var author = await _provider.Authors.FindAsync(id);
        if (author != null)
        {
            _provider.Authors.Remove(author);
            await _provider.SaveAsync();
        }
    }
}