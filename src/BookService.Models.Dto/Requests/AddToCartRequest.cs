using System;

namespace BookService.Models.Dto.Requests;

public class AddToCartRequest
{
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
}