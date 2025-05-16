using BookService.Models.Db;
using BookService.Models.Dto.Requests;
using System;
using System.Linq;

namespace BookService.Mappers.Patch;

public static class PatchDbBookMapper
{
    public static void ApplyPatch(this DbBook book, UpdateBookRequest request, Guid userId)
    {
        if (request.Title != null)
            book.Title = request.Title;
        if (request.Description != null)
            book.Description = request.Description;
        if (request.ShortDescription != null)
            book.ShortDescription = request.ShortDescription;
        if (request.DepartmentId.HasValue)
            book.DepartmentId = request.DepartmentId;
        if (request.Photo != null)
            book.Photo = request.Photo;
        if (request.Price.HasValue)
            book.Price = request.Price.Value;
        if (request.IsActive.HasValue)
            book.IsActive = request.IsActive.Value;
        if (request.PublicationDate.HasValue)
            book.PublicationDate = request.PublicationDate.Value;
        if (request.Quantity.HasValue)
            book.Quantity = request.Quantity;
        //if (request.AuthorIds != null)
        //{
        //    book.BookAuthors.Clear();
        //    book.BookAuthors.AddRange(request.AuthorIds.Select(authorId => new DbBookAuthor
        //    {
        //        Id = Guid.NewGuid(),
        //        AuthorId = authorId,
        //        BookId = book.Id
        //    }));
        //}
        book.ModifiedBy = userId;
        book.ModifiedAt = DateTime.UtcNow;
    }
}