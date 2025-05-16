using BookService.Models.Db;
using BookService.Models.Dto.Requests;

namespace BookService.Mappers.Patch;

public static class PatchDbOrderMapper
{
    public static void ApplyPatch(this DbOrder order, UpdateOrderStatusRequest request)
    {
        order.Status = request.Status.ToString(); // Преобразуем enum в строку для хранения в базе
        order.ModifiedAt = DateTime.UtcNow;
    }
}