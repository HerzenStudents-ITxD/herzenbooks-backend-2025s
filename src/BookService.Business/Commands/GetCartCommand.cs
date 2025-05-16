using BookService.Data.Interfaces;
using BookService.Mappers.Response;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetCartCommand : IGetCartCommand
{
    private readonly IBookCartRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetCartCommand(
        IBookCartRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<List<CartResponse>>> ExecuteAsync()
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var cartItems = await _repository.GetCartByUserIdAsync(userId);
        var cartResponses = cartItems.ToCartResponses();
        return new OperationResultResponse<List<CartResponse>>(cartResponses);
    }
}