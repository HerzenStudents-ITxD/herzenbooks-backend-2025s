using BookService.Models.Dto.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;
[AutoInject]
public interface IGetAdminOrdersCommand
{
    Task<OperationResultResponse<List<OrderResponse>>> ExecuteAsync();
}