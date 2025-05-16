using BookService.Data.Interfaces;
using BookService.Mappers.Patch;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class UpdateBookCommand : IUpdateBookCommand
{
    private readonly IBookRepository _repository;
    private readonly IBookAuthorRepository _bookAuthorRepository;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateBookCommand(
        IBookRepository repository,
        IBookAuthorRepository bookAuthorRepository,
        IAccessValidator accessValidator,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _bookAuthorRepository = bookAuthorRepository;
        _accessValidator = accessValidator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id, UpdateBookRequest request)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can update books." });

        var book = await _repository.GetByIdAsync(id);
        if (book == null)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Book not found." });

        var userId = _httpContextAccessor.HttpContext.GetUserId();
        book.ApplyPatch(request, userId);
        await _repository.UpdateAsync(book);

        if (request.AuthorIds != null)
        {
            var existingAuthors = await _bookAuthorRepository.GetByBookIdAsync(id);
            foreach (var author in existingAuthors)
                await _bookAuthorRepository.DeleteAsync(author.Id);

            foreach (var authorId in request.AuthorIds)
                await _bookAuthorRepository.CreateAsync(new BookService.Models.Db.DbBookAuthor
                {
                    BookId = id,
                    AuthorId = authorId
                });
        }

        return new OperationResultResponse<Guid?>(id);
    }
}