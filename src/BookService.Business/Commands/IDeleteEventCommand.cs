using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;

public interface IDeleteEventCommand
{
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid id);
}