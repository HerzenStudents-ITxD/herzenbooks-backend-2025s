using BookService.Models.Db;
using BookService.Models.Dto.Requests;

namespace BookService.Mappers.Db;

public static class DbEventMapper
{
    public static DbEvent ToDbEvent(this CreateEventRequest request, Guid createdBy)
    {
        return new DbEvent
        {
            Title = request.Title,
            Text = request.Description,
            Photo = request.Photo,
            Link = request.Link,
            Date = request.StartDate,
            PublicationDate = DateTime.UtcNow,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy,
            ModifiedAt = DateTime.UtcNow,
            ModifiedBy = createdBy
        };
    }
}