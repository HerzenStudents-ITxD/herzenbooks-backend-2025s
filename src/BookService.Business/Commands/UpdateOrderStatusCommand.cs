using BookService.Data.Interfaces;
using BookService.Mappers.Patch;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class UpdateOrderStatusCommand : IUpdateOrderStatusCommand
{
    private readonly IOrderRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateOrderStatusCommand(IOrderRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id, UpdateOrderStatusRequest request)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var order = await _repository.GetByIdAsync(id);
        if (order == null || order.UserId != userId)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Order not found or access denied." });

        if (!IsValidStatusTransition(order.Status, request.Status))
            return new OperationResultResponse<Guid?>(null, new List<string> { "Invalid status transition." });

        order.ApplyPatch(request);
        await _repository.UpdateStatusAsync(id, request.Status);
        return new OperationResultResponse<Guid?>(id);
    }

    private bool IsValidStatusTransition(string currentStatus, string newStatus)
    {
        return (currentStatus, newStatus) switch
        {
            ("Pending", "Confirmed") => true,
            ("Pending", "Cancelled") => true,
            ("Confirmed", "Ready") => true,
            ("Confirmed", "Cancelled") => true,
            ("Ready", "Fulfilled") => true,
            ("Ready", "Cancelled") => true,
            _ => false
        };
    }
}