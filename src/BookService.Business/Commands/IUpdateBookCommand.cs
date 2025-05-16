using BookService.Models.Dto.Requests;
using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace BookService.Business.Commands;
[AutoInject]
public interface IUpdateBookCommand
{
    Task<OperationResultResponse<Guid?>> ExecuteAsync(Guid id, UpdateBookRequest request);
}