using BookService.Data.Interfaces;
using BookService.Mappers.Patch;
using BookService.Models.Dto.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class AdminUpdateOrderStatusCommand : IAdminUpdateOrderStatusCommand
{
    private readonly IOrderRepository _repository;
    private readonly IAccessValidator _accessValidator;

    public AdminUpdateOrderStatusCommand(IOrderRepository repository, IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id, UpdateOrderStatusRequest request)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can update order status." });

        var order = await _repository.GetByIdAsync(id);
        if (order == null)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Order not found." });

        order.ApplyPatch(request);
        await _repository.UpdateStatusAsync(id, request.Status.ToString());
        return new OperationResultResponse<Guid?>(id);
    }
}