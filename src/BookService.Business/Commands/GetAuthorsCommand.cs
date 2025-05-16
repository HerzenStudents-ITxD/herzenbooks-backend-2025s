using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetAuthorsCommand : IGetAuthorsCommand
{
    private readonly IAuthorRepository _repository;

    public GetAuthorsCommand(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<List<AuthorResponse>>> ExecuteAsync()
    {
        var authors = await _repository.GetAllAsync();
        var response = authors.Select(a => a.ToAuthorResponse()).ToList();
        return new OperationResultResponse<List<AuthorResponse>>(response);
    }
}