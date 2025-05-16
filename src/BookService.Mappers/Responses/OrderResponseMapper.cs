using BookService.Models.Db;
using BookService.Models.Dto.Responses;

namespace BookService.Mappers.Responses;

public static class OrderResponseMapper
{
    public static OrderResponse ToOrderResponse(this DbOrder order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            UserId = order.UserId,
            Status = order.Status, // Status уже строка в базе
            CreatedAt = order.CreatedAt,
            ModifiedAt = order.ModifiedAt,
            Books = order.OrderBooks.Select(ob => new OrderBookResponse
            {
                BookId = ob.BookId,
                BookTitle = ob.Book?.Title ?? string.Empty,
                BookPrice = ob.Book?.Price ?? 0,
                Quantity = ob.Quantity
            }).ToList()
        };
    }
}