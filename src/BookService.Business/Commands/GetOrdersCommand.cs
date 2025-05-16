using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetOrdersCommand : IGetOrdersCommand
{
    private readonly IOrderRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrdersCommand(IOrderRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<List<OrderResponse>>> ExecuteAsync()
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var orders = await _repository.GetByUserIdAsync(userId);
        var response = orders.Select(o => o.ToOrderResponse()).ToList();
        return new OperationResultResponse<List<OrderResponse>>(response);
    }
}