using System;
using System.Collections.Generic;

namespace BookService.Models.Dto.Responses;

public class BookResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ShortDescription { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public string? Photo { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public DateTime PublicationDate { get; set; }
    public int? Quantity { get; set; }
    public List<AuthorResponse> Authors { get; set; } = new();
}