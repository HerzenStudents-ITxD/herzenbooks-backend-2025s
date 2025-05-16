using BookService.Data.Interfaces;
using BookService.Data.Provider;
using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Data;

public class OrderRepository : IOrderRepository
{
    private readonly IDataProvider _provider;

    public OrderRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<Guid> CreateAsync(DbOrder order)
    {
        order.Id = Guid.NewGuid();
        order.CreatedAt = DateTime.UtcNow;
        await _provider.Orders.AddAsync(order);
        await _provider.SaveAsync();
        return order.Id;
    }

    public async Task<DbOrder?> GetByIdAsync(Guid id)
    {
        return await _provider.Orders
            .Include(o => o.OrderBooks).ThenInclude(ob => ob.Book)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<DbOrder>> GetByUserIdAsync(Guid userId)
    {
        return await _provider.Orders
            .Include(o => o.OrderBooks).ThenInclude(ob => ob.Book)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task UpdateAsync(DbOrder order)
    {
        _provider.Orders.Update(order);
        await _provider.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await _provider.Orders.FindAsync(id);
        if (order != null)
        {
            _provider.Orders.Remove(order);
            await _provider.SaveAsync();
        }
    }

    public async Task<List<DbOrder>> GetAllAsync()
    {
        return await _provider.Orders
            .Include(o => o.OrderBooks).ThenInclude(ob => ob.Book)
            .ToListAsync();
    }

    public async Task<List<DbOrder>> FilterOrdersAsync(string? status, Guid? userId, DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _provider.Orders
            .Include(o => o.OrderBooks).ThenInclude(ob => ob.Book)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
            query = query.Where(o => o.Status == status);

        if (userId.HasValue)
            query = query.Where(o => o.UserId == userId.Value);

        if (dateFrom.HasValue)
            query = query.Where(o => o.CreatedAt >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(o => o.CreatedAt <= dateTo.Value);

        return await query.ToListAsync();
    }

    public async Task UpdateStatusAsync(Guid id, string status)
    {
        var order = await _provider.Orders.FindAsync(id);
        if (order != null)
        {
            order.Status = status;
            order.ModifiedAt = DateTime.UtcNow;
            _provider.Orders.Update(order);
            await _provider.SaveAsync();
        }
    }

    public async Task FulfillOrderAsync(Guid id)
    {
        var order = await _provider.Orders
            .Include(o => o.OrderBooks).ThenInclude(ob => ob.Book)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order != null)
        {
            order.Status = "Fulfilled";
            order.ModifiedAt = DateTime.UtcNow;

            foreach (var orderBook in order.OrderBooks)
            {
                var book = orderBook.Book;
                if (book != null)
                {
                    book.Quantity -= orderBook.Quantity;
                    _provider.Books.Update(book);
                }
            }

            _provider.Orders.Update(order);
            await _provider.SaveAsync();
        }
    }
}