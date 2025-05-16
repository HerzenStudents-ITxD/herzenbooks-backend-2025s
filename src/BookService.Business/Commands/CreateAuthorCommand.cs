using BookService.Data.Interfaces;
using BookService.Mappers.Db;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class CreateAuthorCommand : ICreateAuthorCommand
{
    private readonly IAuthorRepository _repository;
    private readonly IAccessValidator _accessValidator;

    public CreateAuthorCommand(
        IAuthorRepository repository,
        IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateAuthorRequest request)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can create authors." });

        var author = request.ToDbAuthor();
        var authorId = await _repository.CreateAsync(author);
        return new OperationResultResponse<Guid?>(authorId);
    }
}