using BookService.Models.Dto.Responses;
using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;
[AutoInject]
public interface IGetEventCommand
{
    Task<OperationResultResponse<EventResponse?>> ExecuteAsync(Guid id);
}