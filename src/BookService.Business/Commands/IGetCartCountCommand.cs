using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;
[AutoInject]
public interface IGetCartQuantityCommand
{
    Task<OperationResultResponse<int>> ExecuteAsync();
}