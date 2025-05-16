using BookService.Models.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;
[AutoInject]
public interface IGetBookAuthorsCommand
{
    Task<OperationResultResponse<List<AuthorResponse>>> ExecuteAsync(Guid bookId);
}