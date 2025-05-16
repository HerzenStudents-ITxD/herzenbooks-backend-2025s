using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetEventCommand : IGetEventCommand
{
    private readonly IEventRepository _repository;

    public GetEventCommand(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<EventResponse?>> ExecuteAsync(Guid id)
    {
        var @event = await _repository.GetByIdAsync(id);
        if (@event == null)
            return new OperationResultResponse<EventResponse?>(null, new List<string> { "Event not found." });

        return new OperationResultResponse<EventResponse?>(@event.ToEventResponse());
    }
}