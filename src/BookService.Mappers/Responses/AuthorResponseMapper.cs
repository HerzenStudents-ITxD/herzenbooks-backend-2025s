using BookService.Models.Db;
using BookService.Models.Dto.Responses;

namespace BookService.Mappers.Responses;

public static class AuthorResponseMapper
{
    public static AuthorResponse ToAuthorResponse(this DbAuthor author)
    {
        return new AuthorResponse
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Patronim = author.Patronim,
            Books = author.BookAuthors.Select(ba => ba.Book.ToBookResponse()).ToList()
        };
    }
}