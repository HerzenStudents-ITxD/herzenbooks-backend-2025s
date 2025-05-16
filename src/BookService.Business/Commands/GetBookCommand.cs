using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetBookCommand : IGetBookCommand
{
    private readonly IBookRepository _repository;

    public GetBookCommand(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<BookResponse?>> ExecuteAsync(Guid id)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book == null)
            return new OperationResultResponse<BookResponse?>(null, new List<string> { "Book not found." });

        return new OperationResultResponse<BookResponse?>(book.ToBookResponse());
    }
}