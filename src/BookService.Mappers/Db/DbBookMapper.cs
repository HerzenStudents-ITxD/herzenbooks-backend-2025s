using BookService.Models.Db;
using BookService.Models.Dto.Requests;
using System;
using System.Collections.Generic;

namespace BookService.Mappers.Db;

public static class DbBookMapper
{
    public static DbBook ToDbBook(this CreateBookRequest request, Guid userId)
    {
        return new DbBook
        {
            Title = request.Title,
            Description = request.Description,
            ShortDescription = request.ShortDescription,
            DepartmentId = request.DepartmentId,
            Photo = request.Photo,
            Price = request.Price,
            IsActive = request.IsActive,
            PublicationDate = request.PublicationDate,
            Quantity = request.Quantity,
            CreatedBy = userId,
            CreatedAt = DateTime.UtcNow,
            ModifiedBy = userId,
            ModifiedAt = DateTime.UtcNow,
            BookAuthors = request.AuthorIds?.Select(authorId => new DbBookAuthor
            {
                Id = Guid.NewGuid(),
                AuthorId = authorId,
                BookId = Guid.Empty // Will be set after book creation
            }).ToList() ?? new List<DbBookAuthor>()
        };
    }
}