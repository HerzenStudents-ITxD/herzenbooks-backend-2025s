using BookService.Business.Commands;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IGetEventsCommand _getEventsCommand;
    private readonly IGetEventCommand _getEventCommand;
    private readonly IGetUpcomingEventsCommand _getUpcomingEventsCommand;

    public EventsController(
        IGetEventsCommand getEventsCommand,
        IGetEventCommand getEventCommand,
        IGetUpcomingEventsCommand getUpcomingEventsCommand)
    {
        _getEventsCommand = getEventsCommand;
        _getEventCommand = getEventCommand;
        _getUpcomingEventsCommand = getUpcomingEventsCommand;
    }

    [HttpGet]
    [ProducesResponseType(typeof(FindResultResponse<List<EventResponse>>), 200)]
    public async Task<IActionResult> GetEvents()
    {
        var result = await _getEventsCommand.ExecuteAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OperationResultResponse<EventResponse>), 200)]
    public async Task<IActionResult> GetEvent(Guid id)
    {
        var result = await _getEventCommand.ExecuteAsync(id);
        return Ok(result);
    }

    [HttpGet("upcoming")]
    [ProducesResponseType(typeof(FindResultResponse<List<EventResponse>>), 200)]
    public async Task<IActionResult> GetUpcomingEvents([FromQuery] int limit = 3)
    {
        var result = await _getUpcomingEventsCommand.ExecuteAsync(limit);
        return Ok(result);
    }
}