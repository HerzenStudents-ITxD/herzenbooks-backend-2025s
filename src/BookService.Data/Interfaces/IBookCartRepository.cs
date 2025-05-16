using BookService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace BookService.Data.Interfaces;
[AutoInject]
public interface IBookCartRepository
{
    Task<Guid> AddToCartAsync(DbBookCart bookCart);
    Task<List<DbBookCart>> GetCartByUserIdAsync(Guid userId);
    Task<DbBookCart?> GetCartItemAsync(Guid userId, Guid bookId);
    Task UpdateQuantityAsync(DbBookCart bookCart);
    Task RemoveFromCartAsync(Guid userId, Guid bookId);
    Task ClearCartAsync(Guid userId);
    Task<int> GetCartQuantityAsync(Guid userId);
}