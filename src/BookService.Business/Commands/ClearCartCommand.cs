using BookService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class ClearCartCommand : IClearCartCommand
{
    private readonly IBookCartRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClearCartCommand(
        IBookCartRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync()
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        await _repository.ClearCartAsync(userId);
        return new OperationResultResponse<bool>(true);
    }
}