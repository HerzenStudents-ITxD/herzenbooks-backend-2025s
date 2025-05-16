using System;

namespace BookService.Models.Dto.Responses;

public class EventResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? Photo { get; set; }
    public DateTime Date { get; set; }
    public DateTime PublicationDate { get; set; }
    public bool IsActive { get; set; }
}