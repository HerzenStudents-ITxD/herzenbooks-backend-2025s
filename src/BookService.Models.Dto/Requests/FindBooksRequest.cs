namespace BookService.Models.Dto.Requests;

public class FindBooksRequest
{
    public string? Search { get; set; }
    public Guid? DepartmentId { get; set; }
    public int? Year { get; set; }
    public int Limit { get; set; } = 5;
}