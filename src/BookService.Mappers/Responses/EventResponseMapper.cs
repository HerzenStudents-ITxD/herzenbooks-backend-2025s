using BookService.Models.Db;
using BookService.Models.Dto.Responses;

namespace BookService.Mappers.Responses;

public static class EventResponseMapper
{
    public static EventResponse ToEventResponse(this DbEvent @event)
    {
        return new EventResponse
        {
            Id = @event.Id,
            Title = @event.Title,
            Description = @event.Description,
            Text = @event.Text,
            Photo = @event.Photo,
            Date = @event.Date,
            PublicationDate = @event.PublicationDate,
            IsActive = @event.IsActive
        };
    }
}