using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetAuthorCommand : IGetAuthorCommand
{
    private readonly IAuthorRepository _repository;

    public GetAuthorCommand(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<AuthorResponse?>> ExecuteAsync(Guid id)
    {
        var author = await _repository.GetByIdAsync(id);
        if (author == null)
            return new OperationResultResponse<AuthorResponse?>(null, new List<string> { "Author not found." });

        return new OperationResultResponse<AuthorResponse?>(author.ToAuthorResponse());
    }
}