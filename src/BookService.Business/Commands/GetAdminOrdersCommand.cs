using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetAdminOrdersCommand : IGetAdminOrdersCommand
{
    private readonly IOrderRepository _repository;
    private readonly IAccessValidator _accessValidator;

    public GetAdminOrdersCommand(IOrderRepository repository, IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<List<OrderResponse>>> ExecuteAsync()
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<List<OrderResponse>>(null, new List<string> { "Only admins can access this resource." });

        var orders = await _repository.GetAllAsync();
        var response = orders.Select(o => o.ToOrderResponse()).ToList();
        return new OperationResultResponse<List<OrderResponse>>(response);
    }
}