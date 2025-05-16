using BookService.Models.Db;
using BookService.Models.Dto.Requests;

namespace BookService.Mappers.Db;

public static class DbAuthorMapper
{
    public static DbAuthor ToDbAuthor(this CreateAuthorRequest request)
    {
        return new DbAuthor
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronim = request.Patronim
        };
    }
}