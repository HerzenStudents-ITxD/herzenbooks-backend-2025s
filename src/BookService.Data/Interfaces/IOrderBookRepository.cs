using BookService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Data.Interfaces;
[AutoInject]
public interface IOrderBookRepository
{
    Task<Guid> CreateAsync(DbOrderBook orderBook);
    Task<List<DbOrderBook>> GetByOrderIdAsync(Guid orderId);
    Task UpdateAsync(DbOrderBook orderBook);
    Task DeleteAsync(Guid id);
}