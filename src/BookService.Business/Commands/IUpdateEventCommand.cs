using BookService.Models.Dto.Requests;
using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public interface IUpdateEventCommand
{
    Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id, UpdateEventRequest request);
}