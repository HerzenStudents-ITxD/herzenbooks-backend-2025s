using System;
using System.Collections.Generic;

namespace BookService.Models.Dto.Responses;

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public List<OrderBookResponse> Books { get; set; } = new();
}

public class OrderBookResponse
{
    public Guid BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public decimal BookPrice { get; set; }
    public int Quantity { get; set; }
}