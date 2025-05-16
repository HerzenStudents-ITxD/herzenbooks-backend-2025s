using BookService.Models.Dto.Requests;
using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public interface ICreateEventCommand
{
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateEventRequest request);
}