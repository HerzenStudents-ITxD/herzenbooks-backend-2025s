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
public class CartController : ControllerBase
{
    private readonly IGetCartCommand _getCartCommand;
    private readonly IAddToCartCommand _addToCartCommand;
    private readonly IRemoveFromCartCommand _removeFromCartCommand;
    private readonly IClearCartCommand _clearCartCommand;
    private readonly IGetCartQuantityCommand _getCartQuantityCommand;
    private readonly IUpdateCartQuantityCommand _updateCartQuantityCommand;

    public CartController(
        IGetCartCommand getCartCommand,
        IAddToCartCommand addToCartCommand,
        IRemoveFromCartCommand removeFromCartCommand,
        IClearCartCommand clearCartCommand,
        IGetCartQuantityCommand getCartQuantityCommand,
        IUpdateCartQuantityCommand updateCartQuantityCommand)
    {
        _getCartCommand = getCartCommand;
        _addToCartCommand = addToCartCommand;
        _removeFromCartCommand = removeFromCartCommand;
        _clearCartCommand = clearCartCommand;
        _getCartQuantityCommand = getCartQuantityCommand;
        _updateCartQuantityCommand = updateCartQuantityCommand;
    }

    [HttpGet]
    [ProducesResponseType(typeof(FindResultResponse<List<CartResponse>>), 200)]
    public async Task<IActionResult> GetCart()
    {
        var result = await _getCartCommand.ExecuteAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
    {
        var result = await _addToCartCommand.ExecuteAsync(request);
        return Ok(result);
    }

    [HttpDelete("{bookId}")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), 200)]
    public async Task<IActionResult> RemoveFromCart(Guid bookId)
    {
        var result = await _removeFromCartCommand.ExecuteAsync(bookId);
        return Ok(result);
    }

    [HttpPost("clear")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), 200)]
    public async Task<IActionResult> ClearCart()
    {
        var result = await _clearCartCommand.ExecuteAsync();
        return Ok(result);
    }

    [HttpGet("Quantity")]
    [ProducesResponseType(typeof(OperationResultResponse<int>), 200)]
    public async Task<IActionResult> GetCartQuantity()
    {
        var result = await _getCartQuantityCommand.ExecuteAsync();
        return Ok(result);
    }

    [HttpPatch("{bookId}/quantity")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), 200)]
    public async Task<IActionResult> UpdateCartQuantity(Guid bookId, [FromBody] UpdateCartQuantityRequest request)
    {
        var result = await _updateCartQuantityCommand.ExecuteAsync(bookId, request);
        return Ok(result);
    }
}