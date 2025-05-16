using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;
[AutoInject]
public interface IFulfillOrderCommand
{
    Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id);
}