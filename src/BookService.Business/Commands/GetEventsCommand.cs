using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetEventsCommand : IGetEventsCommand
{
    private readonly IEventRepository _repository;

    public GetEventsCommand(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResultResponse<List<EventResponse>>> ExecuteAsync()
    {
        var events = await _repository.GetActiveEventsAsync();
        var response = events.Select(e => e.ToEventResponse()).ToList();
        return new OperationResultResponse<List<EventResponse>>(response);
    }
}