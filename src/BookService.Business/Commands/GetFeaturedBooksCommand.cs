using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetFeaturedBooksCommand : IGetFeaturedBooksCommand
{
    private readonly IBookRepository _repository;

    public GetFeaturedBooksCommand(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<List<BookResponse>>> ExecuteAsync(int limit)
    {
        var books = await _repository.GetFeaturedBooksAsync(limit);
        var response = books.Select(b => b.ToBookResponse()).ToList();
        return new OperationResultResponse<List<BookResponse>>(response);
    }
}