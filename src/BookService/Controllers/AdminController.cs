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
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IGetAdminBooksCommand _getAdminBooksCommand;
    private readonly ICreateBookCommand _createBookCommand;
    private readonly IUpdateBookCommand _updateBookCommand;
    private readonly IUpdateStockCommand _updateStockCommand;
    private readonly ICreateAuthorCommand _createAuthorCommand;
    private readonly IDeleteAuthorCommand _deleteAuthorCommand;
    private readonly ICreateDepartmentCommand _createDepartmentCommand;
    private readonly IUpdateDepartmentCommand _updateDepartmentCommand;
    private readonly IGetAdminOrdersCommand _getAdminOrdersCommand;
    private readonly IAdminUpdateOrderStatusCommand _adminUpdateOrderStatusCommand;
    private readonly ICreateEventCommand _createEventCommand;
    private readonly IUpdateEventCommand _updateEventCommand;
    private readonly IDeleteEventCommand _deleteEventCommand;
    private readonly IFilterAdminOrdersCommand _filterAdminOrdersCommand;
    private readonly IFulfillOrderCommand _fulfillOrderCommand;

    public AdminController(
        IGetAdminBooksCommand getAdminBooksCommand,
        ICreateBookCommand createBookCommand,
        IUpdateBookCommand updateBookCommand,
        IUpdateStockCommand updateStockCommand,
        ICreateAuthorCommand createAuthorCommand,
        IDeleteAuthorCommand deleteAuthorCommand,
        ICreateDepartmentCommand createDepartmentCommand,
        IUpdateDepartmentCommand updateDepartmentCommand,
        IGetAdminOrdersCommand getAdminOrdersCommand,
        IAdminUpdateOrderStatusCommand adminUpdateOrderStatusCommand,
        ICreateEventCommand createEventCommand,
        IUpdateEventCommand updateEventCommand,
        IDeleteEventCommand deleteEventCommand,
        IFilterAdminOrdersCommand filterAdminOrdersCommand,
        IFulfillOrderCommand fulfillOrderCommand)
    {
        _getAdminBooksCommand = getAdminBooksCommand;
        _createBookCommand = createBookCommand;
        _updateBookCommand = updateBookCommand;
        _updateStockCommand = updateStockCommand;
        _createAuthorCommand = createAuthorCommand;
        _deleteAuthorCommand = deleteAuthorCommand;
        _createDepartmentCommand = createDepartmentCommand;
        _updateDepartmentCommand = updateDepartmentCommand;
        _getAdminOrdersCommand = getAdminOrdersCommand;
        _adminUpdateOrderStatusCommand = adminUpdateOrderStatusCommand;
        _createEventCommand = createEventCommand;
        _updateEventCommand = updateEventCommand;
        _deleteEventCommand = deleteEventCommand;
        _filterAdminOrdersCommand = filterAdminOrdersCommand;
        _fulfillOrderCommand = fulfillOrderCommand;
    }

    [HttpGet("books")]
    [ProducesResponseType(typeof(FindResultResponse<List<BookResponse>>), 200)]
    public async Task<IActionResult> GetBooks()
    {
        var result = await _getAdminBooksCommand.ExecuteAsync();
        return Ok(result);
    }

    [HttpPost("books")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
        var result = await _createBookCommand.ExecuteAsync(request);
        return Ok(result);
    }

    [HttpPatch("books/{id}")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookRequest request)
    {
        var result = await _updateBookCommand.ExecuteAsync(id, request);
        return Ok(result);
    }

    [HttpPost("books/{id}/stock")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> UpdateStock(Guid id, [FromBody] UpdateStockRequest request)
    {
        var result = await _updateStockCommand.ExecuteAsync(id, request);
        return Ok(result);
    }

    [HttpPost("authors")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest request)
    {
        var result = await _createAuthorCommand.ExecuteAsync(request);
        return Ok(result);
    }

    [HttpDelete("authors/{id}")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), 200)]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var result = await _deleteAuthorCommand.ExecuteAsync(id);
        return Ok(result);
    }

    [HttpPost("departments")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request)
    {
        var result = await _createDepartmentCommand.ExecuteAsync(request);
        return Ok(result);
    }

    [HttpPatch("departments/{id}")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] UpdateDepartmentRequest request)
    {
        var result = await _updateDepartmentCommand.ExecuteAsync(id, request);
        return Ok(result);
    }

    [HttpGet("orders")]
    [ProducesResponseType(typeof(FindResultResponse<List<OrderResponse>>), 200)]
    public async Task<IActionResult> GetOrders()
    {
        var result = await _getAdminOrdersCommand.ExecuteAsync();
        return Ok(result);
    }

    [HttpPatch("orders/{id}/status")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusRequest request)
    {
        var result = await _adminUpdateOrderStatusCommand.ExecuteAsync(id, request);
        return Ok(result);
    }

    [HttpPost("events")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request)
    {
        var result = await _createEventCommand.ExecuteAsync(request);
        return Ok(result);
    }

    [HttpPatch("events/{id}")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventRequest request)
    {
        var result = await _updateEventCommand.ExecuteAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("events/{id}")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), 200)]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var result = await _deleteEventCommand.ExecuteAsync(id);
        return Ok(result);
    }

    [HttpGet("orders/filter")]
    [ProducesResponseType(typeof(FindResultResponse<List<OrderResponse>>), 200)]
    public async Task<IActionResult> FilterOrders([FromQuery] FilterOrdersRequest request)
    {
        var result = await _filterAdminOrdersCommand.ExecuteAsync(request);
        return Ok(result);
    }

    [HttpPost("orders/{id}/fulfill")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> FulfillOrder(Guid id)
    {
        var result = await _fulfillOrderCommand.ExecuteAsync(id);
        return Ok(result);
    }
}