using BookService.Models.Db;
using BookService.Models.Dto.Responses;
using System.Linq;

namespace BookService.Mappers.Responses;

public static class BookResponseMapper
{
    public static BookResponse ToBookResponse(this DbBook book)
    {
        return new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ShortDescription = book.ShortDescription,
            DepartmentId = book.DepartmentId,
            DepartmentName = book.Department?.Name,
            Photo = book.Photo,
            Price = book.Price,
            IsActive = book.IsActive,
            PublicationDate = book.PublicationDate,
            Quantity = book.Quantity,
            Authors = book.BookAuthors.Select(ba => ba.Author.ToAuthorResponse()).ToList()
        };
    }
}