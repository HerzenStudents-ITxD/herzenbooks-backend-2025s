using System;

namespace BookService.Models.Dto.Requests;

public class CreateEventRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Photo { get; set; }
    public string? Link { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; } = true;
}