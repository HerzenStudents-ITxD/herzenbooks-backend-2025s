using System;

namespace BookService.Models.Dto.Responses;

public class DepartmentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}