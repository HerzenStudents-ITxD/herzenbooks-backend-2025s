using BookService.Data.Interfaces;
using BookService.Data.Provider;
using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Data;

public class BookRepository : IBookRepository
{
    private readonly IDataProvider _provider;

    public BookRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<Guid> CreateAsync(DbBook book)
    {
        book.Id = Guid.NewGuid();
        book.CreatedAt = DateTime.UtcNow;
        book.ModifiedAt = DateTime.UtcNow;
        await _provider.Books.AddAsync(book);
        await _provider.SaveAsync();
        return book.Id;
    }

    public async Task<DbBook?> GetByIdAsync(Guid id)
    {
        return await _provider.Books
            .Include(b => b.Department)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookCarts)
            .Include(b => b.OrderBooks)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<DbBook>> GetAllAsync()
    {
        return await _provider.Books
            .Include(b => b.Department)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Where(b => b.IsActive)
            .ToListAsync();
    }

    public async Task<List<DbBook>> FindBooksAsync(string? search, Guid? departmentId, int? year, int limit)
    {
        var query = _provider.Books
            .Include(b => b.Department)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Where(b => b.IsActive);

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                     b.BookAuthors.Any(ba => ba.Author.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                                             ba.Author.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)));
        }

        if (departmentId.HasValue)
        {
            query = query.Where(b => b.DepartmentId == departmentId);
        }

        if (year.HasValue)
        {
            query = query.Where(b => b.PublicationDate.Year == year);
        }

        return await query
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<DbBook>> GetRecommendationsAsync(Guid bookId, int limit)
    {
        var book = await _provider.Books
            .Include(b => b.Department)
            .Include(b => b.BookAuthors)
            .FirstOrDefaultAsync(b => b.Id == bookId);

        if (book == null)
            return new List<DbBook>();

        var authorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList();

        var query = _provider.Books
            .Include(b => b.Department)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Where(b => b.Id != bookId && b.IsActive);

        var recommendations = await query
            .Where(b => b.DepartmentId == book.DepartmentId || b.BookAuthors.Any(ba => authorIds.Contains(ba.AuthorId)))
            .OrderBy(b => b.DepartmentId == book.DepartmentId ? 0 : 1)
            .Take(limit)
            .ToListAsync();

        return recommendations;
    }

    public async Task<List<DbBook>> GetFeaturedBooksAsync(int limit)
    {
        return await _provider.Books
            .Include(b => b.Department)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Where(b => b.IsActive)
            .OrderByDescending(b => b.PublicationDate)
            .Take(limit)
            .ToListAsync();
    }

    public async Task UpdateAsync(DbBook book)
    {
        book.ModifiedAt = DateTime.UtcNow;
        _provider.Books.Update(book);
        await _provider.SaveAsync();
    }

    public async Task UpdateStockAsync(Guid bookId, int Quantity)
    {
        var book = await _provider.Books.FindAsync(bookId);
        if (book != null)
        {
            book.Quantity = Quantity;
            book.ModifiedAt = DateTime.UtcNow;
            _provider.Books.Update(book);
            await _provider.SaveAsync();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await _provider.Books.FindAsync(id);
        if (book != null)
        {
            _provider.Books.Remove(book);
            await _provider.SaveAsync();
        }
    }
}