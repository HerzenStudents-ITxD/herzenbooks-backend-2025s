using System;
using UniversityHelper.BookService.Models.Dto.Enums;

namespace BookService.Models.Dto.Requests;

public class FilterOrdersRequest
{
    public OrderStatusType? Status { get; set; }
    public Guid? UserId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}