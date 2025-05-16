using BookService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetCartQuantityCommand : IGetCartQuantityCommand
{
    private readonly IBookCartRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetCartQuantityCommand(
        IBookCartRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<int>> ExecuteAsync()
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var Quantity = await _repository.GetCartQuantityAsync(userId);
        return new OperationResultResponse<int>(Quantity);
    }
}