using BookService.Data.Interfaces;
using BookService.Mappers.Patch;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class UpdateEventCommand : IUpdateEventCommand
{
    private readonly IEventRepository _repository;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateEventCommand(IEventRepository repository, IHttpContextAccessor httpContextAccessor, IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id, UpdateEventRequest request)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can update events." });

        var @event = await _repository.GetByIdAsync(id);
        if (@event == null)
            return new OperationResultResponse<Guid?>(null, new List<string> { "Event not found." });

        @event.ApplyPatch(request,userId);
        await _repository.UpdateAsync(@event);
        return new OperationResultResponse<Guid?>(id);
    }
}