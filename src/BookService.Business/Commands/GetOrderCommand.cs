using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetOrderCommand : IGetOrderCommand
{
    private readonly IOrderRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrderCommand(IOrderRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<OrderResponse?>> ExecuteAsync(Guid id)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var order = await _repository.GetByIdAsync(id);
        if (order == null || order.UserId != userId)
            return new OperationResultResponse<OrderResponse?>(null, new List<string> { "Order not found or access denied." });

        return new OperationResultResponse<OrderResponse?>(order.ToOrderResponse());
    }
}