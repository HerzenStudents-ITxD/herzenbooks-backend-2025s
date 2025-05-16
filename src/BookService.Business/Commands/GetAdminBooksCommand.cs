using BookService.Data.Interfaces;
using BookService.Mappers.Responses;
using BookService.Models.Dto.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public class GetAdminBooksCommand : IGetAdminBooksCommand
{
    private readonly IBookRepository _repository;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAdminBooksCommand(
        IBookRepository repository,
        IAccessValidator accessValidator,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _accessValidator = accessValidator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<List<BookResponse>>> ExecuteAsync()
    {
        if (!await _accessValidator.IsAdminAsync())
            return new OperationResultResponse<List<BookResponse>>(null, new List<string> { "Only admins can access this resource." });

        var books = await _repository.GetAllAsync();
        var response = books.Select(b => b.ToBookResponse()).ToList();
        return new OperationResultResponse<List<BookResponse>>(response);
    }
}