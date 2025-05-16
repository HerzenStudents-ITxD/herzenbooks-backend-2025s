using BookService.Data.Interfaces;
using BookService.Data.Provider;
using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Data;

public class BookCartRepository : IBookCartRepository
{
    private readonly IDataProvider _provider;

    public BookCartRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<Guid> AddToCartAsync(DbBookCart bookCart)
    {
        bookCart.Id = Guid.NewGuid();
        await _provider.BookCarts.AddAsync(bookCart);
        await _provider.SaveAsync();
        return bookCart.Id;
    }

    public async Task<List<DbBookCart>> GetCartByUserIdAsync(Guid userId)
    {
        return await _provider.BookCarts
            .Include(bc => bc.Book)
            .Where(bc => bc.UserId == userId)
            .ToListAsync();
    }

    public async Task<DbBookCart?> GetCartItemAsync(Guid userId, Guid bookId)
    {
        return await _provider.BookCarts
            .FirstOrDefaultAsync(bc => bc.UserId == userId && bc.BookId == bookId);
    }

    public async Task UpdateQuantityAsync(DbBookCart bookCart)
    {
        _provider.BookCarts.Update(bookCart);
        await _provider.SaveAsync();
    }

    public async Task RemoveFromCartAsync(Guid userId, Guid bookId)
    {
        var bookCart = await GetCartItemAsync(userId, bookId);
        if (bookCart != null)
        {
            _provider.BookCarts.Remove(bookCart);
            await _provider.SaveAsync();
        }
    }

    public async Task ClearCartAsync(Guid userId)
    {
        var bookCarts = await _provider.BookCarts
            .Where(bc => bc.UserId == userId)
            .ToListAsync();
        _provider.BookCarts.RemoveRange(bookCarts);
        await _provider.SaveAsync();
    }

    public async Task<int> GetCartQuantityAsync(Guid userId)
    {
        return await _provider.BookCarts
            .Where(bc => bc.UserId == userId)
            .SumAsync(bc => bc.Quantity);
    }
}

