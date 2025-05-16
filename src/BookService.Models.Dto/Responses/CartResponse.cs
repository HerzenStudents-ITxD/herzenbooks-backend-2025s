using System;
using System.Collections.Generic;

namespace BookService.Models.Dto.Responses;

public class CartResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string BookTitle { get; set; }
    public decimal BookPrice { get; set; }
    public int Quantity { get; set; }
}