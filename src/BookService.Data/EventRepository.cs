using BookService.Data.Interfaces;
using BookService.Data.Provider;
using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Data;

public class EventRepository : IEventRepository
{
    private readonly IDataProvider _provider;

    public EventRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<Guid> CreateAsync(DbEvent evt)
    {
        evt.Id = Guid.NewGuid();
        evt.CreatedAt = DateTime.UtcNow;
        evt.ModifiedAt = DateTime.UtcNow;
        await _provider.Events.AddAsync(evt);
        await _provider.SaveAsync();
        return evt.Id;
    }

    public async Task<DbEvent?> GetByIdAsync(Guid id)
    {
        return await _provider.Events.FindAsync(id);
    }

    public async Task<List<DbEvent>> GetAllAsync()
    {
        return await _provider.Events.ToListAsync();
    }

    public async Task UpdateAsync(DbEvent evt)
    {
        evt.ModifiedAt = DateTime.UtcNow;
        _provider.Events.Update(evt);
        await _provider.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var evt = await _provider.Events.FindAsync(id);
        if (evt != null)
        {
            _provider.Events.Remove(evt);
            await _provider.SaveAsync();
        }
    }
    public async Task<List<DbEvent>> GetActiveEventsAsync()
    {
        return await _provider.Events
            .Where(e => e.IsActive && e.Date >= DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<List<DbEvent>> GetUpcomingEventsAsync(int limit)
    {
        return await _provider.Events
            .Where(e => e.IsActive && e.Date >= DateTime.UtcNow)
            .OrderBy(e => e.Date)
            .Take(limit)
            .ToListAsync();
    }

}