using BookService.Business.Commands;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IGetDepartmentsCommand _getDepartmentsCommand;

    public DepartmentsController(IGetDepartmentsCommand getDepartmentsCommand)
    {
        _getDepartmentsCommand = getDepartmentsCommand;
    }

    [HttpGet]
    [ProducesResponseType(typeof(FindResultResponse<List<DepartmentResponse>>), 200)]
    public async Task<IActionResult> GetDepartments()
    {
        var result = await _getDepartmentsCommand.ExecuteAsync();
        return Ok(result);
    }
}