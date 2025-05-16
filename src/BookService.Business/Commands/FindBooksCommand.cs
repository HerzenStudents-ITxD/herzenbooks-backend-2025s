using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Requests;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class FindBooksCommand : IFindBooksCommand
{
    private readonly IBookRepository _repository;

    public FindBooksCommand(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<List<BookResponse>>> ExecuteAsync(FindBooksRequest request)
    {
        var books = await _repository.FindBooksAsync(request.Search, request.DepartmentId, request.Year, request.Limit);
        var response = books.Select(b => b.ToBookResponse()).ToList();
        return new OperationResultResponse<List<BookResponse>>(response);
    }
}