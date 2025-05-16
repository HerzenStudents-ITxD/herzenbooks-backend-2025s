using BookService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class FulfillOrderCommand : IFulfillOrderCommand
{
    private readonly IOrderRepository _repository;
    private readonly IAccessValidator _accessValidator;

    public FulfillOrderCommand(IOrderRepository repository, IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can fulfill orders." });

        var order = await _repository.GetByIdAsync(id);
        if (order == null)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Order not found." });

        if (order.Status != "Ready")
            return new OperationResultResponse<Guid?>(null, new List<string> { "Order must be in 'Ready' status to fulfill." });

        await _repository.FulfillOrderAsync(id);
        return new OperationResultResponse<Guid?>(id);
    }
}