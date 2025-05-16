using BookService.Data.Interfaces;
using BookService.Data.Provider;
using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Data;

public class OrderBookRepository : IOrderBookRepository
{
    private readonly IDataProvider _provider;

    public OrderBookRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<Guid> CreateAsync(DbOrderBook orderBook)
    {
        orderBook.Id = Guid.NewGuid();
        await _provider.OrderBooks.AddAsync(orderBook);
        await _provider.SaveAsync();
        return orderBook.Id;
    }

    public async Task<List<DbOrderBook>> GetByOrderIdAsync(Guid orderId)
    {
        return await _provider.OrderBooks
            .Include(ob => ob.Book)
            .Where(ob => ob.OrderId == orderId)
            .ToListAsync();
    }

    public async Task UpdateAsync(DbOrderBook orderBook)
    {
        _provider.OrderBooks.Update(orderBook);
        await _provider.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var orderBook = await _provider.OrderBooks.FindAsync(id);
        if (orderBook != null)
        {
            _provider.OrderBooks.Remove(orderBook);
            await _provider.SaveAsync();
        }
    }
}