using BookService.Data.Interfaces;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class UpdateCartQuantityCommand : IUpdateCartQuantityCommand
{
    private readonly IBookCartRepository _repository;
    private readonly IBookRepository _bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateCartQuantityCommand(
        IBookCartRepository repository,
        IBookRepository bookRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _bookRepository = bookRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid bookId, UpdateCartQuantityRequest request)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var cartItem = await _repository.GetCartItemAsync(userId, bookId);
        if (cartItem == null)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Cart item not found." });

        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null || !book.IsActive)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Book not found or inactive." });

        if (book.Quantity < request.Quantity)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Insufficient stock." });

        cartItem.Quantity = request.Quantity;
        await _repository.UpdateQuantityAsync(cartItem);
        return new OperationResultResponse<Guid?>(cartItem.Id);
    }
}