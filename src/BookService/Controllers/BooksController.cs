using BookService.Business.Commands;
using BookService.Models.Dto.Requests;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IFindBooksCommand _findBooksCommand;
    private readonly IGetBookCommand _getBookCommand;
    private readonly IGetBookRecommendationsCommand _getBookRecommendationsCommand;
    private readonly IGetFeaturedBooksCommand _getFeaturedBooksCommand;

    public BooksController(
        IFindBooksCommand findBooksCommand,
        IGetBookCommand getBookCommand,
        IGetBookRecommendationsCommand getBookRecommendationsCommand,
        IGetFeaturedBooksCommand getFeaturedBooksCommand)
    {
        _findBooksCommand = findBooksCommand;
        _getBookCommand = getBookCommand;
        _getBookRecommendationsCommand = getBookRecommendationsCommand;
        _getFeaturedBooksCommand = getFeaturedBooksCommand;
    }

    [HttpGet]
    [ProducesResponseType(typeof(FindResultResponse<List<BookResponse>>), 200)]
    public async Task<IActionResult> GetBooks([FromQuery] FindBooksRequest request)
    {
        var result = await _findBooksCommand.ExecuteAsync(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OperationResultResponse<BookResponse>), 200)]
    public async Task<IActionResult> GetBook(Guid id)
    {
        var result = await _getBookCommand.ExecuteAsync(id);
        return Ok(result);
    }

    [HttpGet("{id}/recommendation")]
    [ProducesResponseType(typeof(FindResultResponse<List<BookResponse>>), 200)]
    public async Task<IActionResult> GetRecommendations(Guid id, [FromQuery] int limit = 5)
    {
        var result = await _getBookRecommendationsCommand.ExecuteAsync(id, limit);
        return Ok(result);
    }

    [HttpGet("featured")]
    [ProducesResponseType(typeof(FindResultResponse<List<BookResponse>>), 200)]
    public async Task<IActionResult> GetFeatured([FromQuery] int limit = 5)
    {
        var result = await _getFeaturedBooksCommand.ExecuteAsync(limit);
        return Ok(result);
    }
}