using BookService.Data.Interfaces;
using BookService.Data.Provider;
using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Data;

public class BookAuthorRepository : IBookAuthorRepository
{
    private readonly IDataProvider _provider;

    public BookAuthorRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<Guid> CreateAsync(DbBookAuthor bookAuthor)
    {
        bookAuthor.Id = Guid.NewGuid();
        await _provider.BookAuthors.AddAsync(bookAuthor);
        await _provider.SaveAsync();
        return bookAuthor.Id;
    }

    public async Task<List<DbBookAuthor>> GetByBookIdAsync(Guid bookId)
    {
        return await _provider.BookAuthors
            .Include(ba => ba.Author)
            .Where(ba => ba.BookId == bookId)
            .ToListAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var bookAuthor = await _provider.BookAuthors.FindAsync(id);
        if (bookAuthor != null)
        {
            _provider.BookAuthors.Remove(bookAuthor);
            await _provider.SaveAsync();
        }
    }
}