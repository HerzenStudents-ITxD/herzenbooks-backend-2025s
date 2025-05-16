using BookService.Models.Db;
using BookService.Models.Dto.Requests;
using System;
using System.Collections.Generic;

namespace BookService.Mappers.Db;

public static class DbOrderMapper
{
    public static DbOrder ToDbOrder(this CreateOrderRequest request, Guid userId, List<DbBookCart> carts)
    {
        return new DbOrder
        {
            UserId = userId,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            OrderBooks = carts.Select(c => new DbOrderBook
            {
                BookId = c.BookId
            }).ToList()
        };
    }
}