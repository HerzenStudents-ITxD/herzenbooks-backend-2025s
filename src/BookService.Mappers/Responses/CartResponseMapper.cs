using BookService.Models.Db;
using BookService.Models.Dto.Responses;
using System.Collections.Generic;
using System.Linq;

namespace BookService.Mappers.Response;

public static class CartResponseMapper
{
    public static List<CartResponse> ToCartResponses(this IEnumerable<DbBookCart> bookCarts)
    {
        return bookCarts.Select(bc => new CartResponse
        {
            Id = bc.Id,
            BookId = bc.BookId,
            BookTitle = bc.Book.Title,
            BookPrice = bc.Book.Price,
            Quantity = bc.Quantity
        }).ToList();
    }
}