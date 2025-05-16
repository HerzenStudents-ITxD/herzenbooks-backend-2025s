using System;
using System.Collections.Generic;

namespace BookService.Models.Dto.Requests;

public class UpdateBookRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? Photo { get; set; }
    public decimal? Price { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? PublicationDate { get; set; }
    public int? Quantity { get; set; }
    public List<Guid>? AuthorIds { get; set; }
}