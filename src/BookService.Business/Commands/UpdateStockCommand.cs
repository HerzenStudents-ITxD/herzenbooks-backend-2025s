using BookService.Data.Interfaces;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class UpdateStockCommand : IUpdateStockCommand
{
    private readonly IBookRepository _repository;
    private readonly IAccessValidator _accessValidator;

    public UpdateStockCommand(
        IBookRepository repository,
        IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id, UpdateStockRequest request)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can update stock." });

        var book = await _repository.GetByIdAsync(id);
        if (book == null)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Book not found." });

        await _repository.UpdateStockAsync(id, request.Quantity);
        return new OperationResultResponse<Guid?>(id);
    }
}