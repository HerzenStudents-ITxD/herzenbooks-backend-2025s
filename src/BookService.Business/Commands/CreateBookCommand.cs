using BookService.Data.Interfaces;
using BookService.Mappers.Db;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class CreateBookCommand : ICreateBookCommand
{
    private readonly IBookRepository _repository;
    private readonly IBookAuthorRepository _bookAuthorRepository;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateBookCommand(
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

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateBookRequest request)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can create books." });

        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var book = request.ToDbBook(userId);
        var bookId = await _repository.CreateAsync(book);

        foreach (var bookAuthor in book.BookAuthors)
        {
            bookAuthor.BookId = bookId;
            await _bookAuthorRepository.CreateAsync(bookAuthor);
        }

        return new OperationResultResponse<Guid?>(bookId);
    }
}