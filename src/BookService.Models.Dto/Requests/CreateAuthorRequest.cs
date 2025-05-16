namespace BookService.Models.Dto.Requests;

public class CreateAuthorRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Patronim { get; set; }
}