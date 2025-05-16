using BookService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class DeleteEventCommand : IDeleteEventCommand
{
    private readonly IEventRepository _repository;
    private readonly IAccessValidator _accessValidator;

    public DeleteEventCommand(IEventRepository repository, IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid id)
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<bool>(false, new List<string> { "Only admins can delete events." });

        var @event = await _repository.GetByIdAsync(id);
        if (@event == null)
            return new OperationResultResponse<bool>(false, new List<string> { "Event not found." });

        await _repository.DeleteAsync(id);
        return new OperationResultResponse<bool>(true);
    }
}