using BookService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class RemoveFromCartCommand : IRemoveFromCartCommand
{
    private readonly IBookCartRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveFromCartCommand(
        IBookCartRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid bookId)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var cartItem = await _repository.GetCartItemAsync(userId, bookId);
        if (cartItem == null)
            return new OperationResultResponse<bool>(false, new List<string> { "Cart item not found." });

        await _repository.RemoveFromCartAsync(userId, bookId);
        return new OperationResultResponse<bool>(true);
    }
}