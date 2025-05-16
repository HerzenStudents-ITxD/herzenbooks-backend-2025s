using BookService.Models.Db;
using BookService.Models.Dto.Requests;

namespace BookService.Mappers.Patch;

public static class PatchDbEventMapper
{
    public static void ApplyPatch(this DbEvent @event, UpdateEventRequest request, Guid modifiedBy)
    {
        if (request.Title != null)
            @event.Title = request.Title;
        if (request.Description != null)
            @event.Text = request.Description;
        if (request.Photo != null)
            @event.Photo = request.Photo;
        if (request.Link != null)
            @event.Link = request.Link;
        if (request.StartDate.HasValue)
            @event.Date = request.StartDate.Value;
        if (request.IsActive.HasValue)
            @event.IsActive = request.IsActive.Value;
        @event.ModifiedAt = DateTime.UtcNow;
        @event.ModifiedBy = modifiedBy;
    }
}