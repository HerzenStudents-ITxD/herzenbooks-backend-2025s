using BookService.Models.Db;
using BookService.Models.Dto.Requests;

namespace BookService.Mappers.Db;

public static class DbBookCartMapper
{
    public static DbBookCart ToDbBookCart(this AddToCartRequest request, Guid userId)
    {
        return new DbBookCart
        {
            UserId = userId,
            BookId = request.BookId,
            Quantity = request.Quantity
        };
    }
}