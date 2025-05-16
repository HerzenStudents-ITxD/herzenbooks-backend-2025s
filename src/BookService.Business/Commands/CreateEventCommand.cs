using BookService.Data.Interfaces;
using BookService.Mappers.Db;
using BookService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class CreateEventCommand : ICreateEventCommand
{
    private readonly IEventRepository _repository;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateEventCommand(IEventRepository repository, IHttpContextAccessor httpContextAccessor, IAccessValidator accessValidator)
    {
        _repository = repository;
        _accessValidator = accessValidator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateEventRequest request)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<Guid?>(null, new List<string> { "Only admins can create events." });

        var @event = request.ToDbEvent(userId);
        var eventId = await _repository.CreateAsync(@event);
        return new OperationResultResponse<Guid?>(eventId);
    }
}