using BookService.Business.Commands;
using BookService.Models.Dto.Requests;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IGetOrdersCommand _getOrdersCommand;
    private readonly ICreateOrderCommand _createOrderCommand;
    private readonly IGetOrderCommand _getOrderCommand;
    private readonly IUpdateOrderStatusCommand _updateOrderStatusCommand;

    public OrdersController(
        IGetOrdersCommand getOrdersCommand,
        ICreateOrderCommand createOrderCommand,
        IGetOrderCommand getOrderCommand,
        IUpdateOrderStatusCommand updateOrderStatusCommand)
    {
        _getOrdersCommand = getOrdersCommand;
        _createOrderCommand = createOrderCommand;
        _getOrderCommand = getOrderCommand;
        _updateOrderStatusCommand = updateOrderStatusCommand;
    }

    [HttpGet]
    [ProducesResponseType(typeof(FindResultResponse<List<OrderResponse>>), 200)]
    public async Task<IActionResult> GetOrders()
    {
        var result = await _getOrdersCommand.ExecuteAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var result = await _createOrderCommand.ExecuteAsync(request);
        return Ok(result);
    }

    [HttpGet("{orderId}")]
    [ProducesResponseType(typeof(OperationResultResponse<OrderResponse>), 200)]
    public async Task<IActionResult> GetOrder(Guid orderId)
    {
        var result = await _getOrderCommand.ExecuteAsync(orderId);
        return Ok(result);
    }

    [HttpPatch("{orderId}/status")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusRequest request)
    {
        var result = await _updateOrderStatusCommand.ExecuteAsync(orderId, request);
        return Ok(result);
    }
}