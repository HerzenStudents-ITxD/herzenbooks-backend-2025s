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
public class AuthorsController : ControllerBase
{
    private readonly IGetAuthorsCommand _getAuthorsCommand;
    private readonly IGetAuthorCommand _getAuthorCommand;
    private readonly IGetBookAuthorsCommand _getBookAuthorsCommand;

    public AuthorsController(
        IGetAuthorsCommand getAuthorsCommand,
        IGetAuthorCommand getAuthorCommand,
        IGetBookAuthorsCommand getBookAuthorsCommand)
    {
        _getAuthorsCommand = getAuthorsCommand;
        _getAuthorCommand = getAuthorCommand;
        _getBookAuthorsCommand = getBookAuthorsCommand;
    }

    [HttpGet]
    [ProducesResponseType(typeof(FindResultResponse<List<AuthorResponse>>), 200)]
    public async Task<IActionResult> GetAuthors()
    {
        var result = await _getAuthorsCommand.ExecuteAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OperationResultResponse<AuthorResponse>), 200)]
    public async Task<IActionResult> GetAuthor(Guid id)
    {
        var result = await _getAuthorCommand.ExecuteAsync(id);
        return Ok(result);
    }

    [HttpGet("/api/books/{bookId}/authors")]
    [ProducesResponseType(typeof(FindResultResponse<List<AuthorResponse>>), 200)]
    public async Task<IActionResult> GetBookAuthors(Guid bookId)
    {
        var result = await _getBookAuthorsCommand.ExecuteAsync(bookId);
        return Ok(result);
    }
}