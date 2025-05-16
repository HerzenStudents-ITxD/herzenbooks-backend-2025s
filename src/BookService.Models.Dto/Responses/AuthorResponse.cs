using System;
using System.Collections.Generic;

namespace BookService.Models.Dto.Responses;

public class AuthorResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronim { get; set; }
    public List<BookResponse> Books { get; set; } = new();
}