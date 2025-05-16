using System;

namespace BookService.Models.Dto.Requests;

public class UpdateEventRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Photo { get; set; }
    public string? Link { get; set; }
    public DateTime? StartDate { get; set; }
    public bool? IsActive { get; set; }
}