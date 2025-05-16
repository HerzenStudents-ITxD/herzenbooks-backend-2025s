using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetBookRecommendationsCommand : IGetBookRecommendationsCommand
{
    private readonly IBookRepository _repository;

    public GetBookRecommendationsCommand(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<List<BookResponse>>> ExecuteAsync(Guid bookId, int limit)
    {
        var books = await _repository.GetRecommendationsAsync(bookId, limit);
        var response = books.Select(b => b.ToBookResponse()).ToList();
        return new OperationResultResponse<List<BookResponse>>(response);
    }
}