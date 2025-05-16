using BookService.Models.Dto.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;
[AutoInject]
public interface IGetFeaturedBooksCommand
{
    Task<OperationResultResponse<List<BookResponse>>> ExecuteAsync(int limit);
}