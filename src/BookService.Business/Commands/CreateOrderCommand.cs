using BookService.Data.Interfaces;
using BookService.Mappers.Db;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class CreateOrderCommand : ICreateOrderCommand
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookCartRepository _cartRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateOrderCommand(
        IOrderRepository orderRepository,
        IBookCartRepository cartRepository,
        IBookRepository bookRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _bookRepository = bookRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateOrderRequest request)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var carts = await _cartRepository.GetCartByUserIdAsync(userId);
        if (!carts.Any())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Cart is empty." });

        foreach (var cart in carts)
        {
            var book = await _bookRepository.GetByIdAsync(cart.BookId);
            if (book == null || !book.IsActive)
                return new OperationResultResponse<Guid?>(null, new List<string> { $"Book {cart.BookId} not found or inactive." });

            if (cart.Quantity > book.Quantity)
                return new OperationResultResponse<Guid?>(null, new List<string> { $"Insufficient stock for book {cart.BookId}." });
        }

        var order = request.ToDbOrder(userId, carts);
        var orderId = await _orderRepository.CreateAsync(order);
        await _cartRepository.ClearCartAsync(userId);
        return new OperationResultResponse<Guid?>(orderId);
    }
}