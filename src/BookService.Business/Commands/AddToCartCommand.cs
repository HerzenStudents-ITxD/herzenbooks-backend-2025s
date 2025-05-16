using BookService.Data.Interfaces;
using BookService.Mappers.Db;
using BookService.Models.Db;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class AddToCartCommand : IAddToCartCommand
{
    private readonly IBookCartRepository _cartRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddToCartCommand(
        IBookCartRepository cartRepository,
        IBookRepository bookRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _cartRepository = cartRepository;
        _bookRepository = bookRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(AddToCartRequest request)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var book = await _bookRepository.GetByIdAsync(request.BookId);
        if (book == null || !book.IsActive)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Book not found or inactive." });

        if (book.Quantity < request.Quantity)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Insufficient stock." });

        var existingCart = await _cartRepository.GetCartItemAsync(userId, request.BookId);
        if (existingCart != null)
        {
            existingCart.Quantity += request.Quantity;
            if (existingCart.Quantity > book.Quantity)
                return new OperationResultResponse<Guid?>(null, new List<string> { "Insufficient stock." });

            await _cartRepository.UpdateQuantityAsync(existingCart);
            return new OperationResultResponse<Guid?>(existingCart.Id);
        }

        var cart = request.ToDbBookCart(userId);
        var cartId = await _cartRepository.AddToCartAsync(cart);
        return new OperationResultResponse<Guid?>(cartId);
    }
}