using BookService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class DeleteAuthorCommand : IDeleteAuthorCommand
{
    private readonly IAuthorRepository _repository;
    private readonly IAccessValidator _accessValidator;

    public DeleteAuthorCommand(
        IAuthorRepository repository,
        IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid id)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<bool>(false, new List<string> { "Only admins can delete authors." });

        var author = await _repository.GetByIdAsync(id);
        if (author == null)
            return new OperationResultResponse<bool>(false, new List<string> { "Author not found." });

        await _repository.DeleteAsync(id);
        return new OperationResultResponse<bool>(true);
    }
}