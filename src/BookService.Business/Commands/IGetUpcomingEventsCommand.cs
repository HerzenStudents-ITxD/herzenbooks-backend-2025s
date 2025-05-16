using BookService.Models.Dto.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;
[AutoInject]
public interface IGetUpcomingEventsCommand
{
    Task<OperationResultResponse<List<EventResponse>>> ExecuteAsync(int limit);
}