using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetBookAuthorsCommand : IGetBookAuthorsCommand
{
    private readonly IAuthorRepository _repository;

    public GetBookAuthorsCommand(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<List<AuthorResponse>>> ExecuteAsync(Guid bookId)
    {
        var authors = await _repository.GetByBookIdAsync(bookId);
        var response = authors.Select(a => a.ToAuthorResponse()).ToList();
        return new OperationResultResponse<List<AuthorResponse>>(response);
    }
}