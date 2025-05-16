using UniversityHelper.BookService.Models.Dto.Enums;

namespace BookService.Models.Dto.Requests;

public class UpdateOrderStatusRequest
{
    public string Status { get; set; }
}