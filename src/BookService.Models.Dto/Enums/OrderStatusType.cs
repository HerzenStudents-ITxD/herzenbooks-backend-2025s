using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UniversityHelper.BookService.Models.Dto.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum OrderStatusType
{
    /// <summary> Заказ создан, но не обработан </summary>
    Pending = 0,

    /// <summary> Заказ в процессе сборки </summary>
    Processing = 1,

    /// <summary> Заказ собран и готов к выдаче </summary>
    ReadyForPickup = 2,

    /// <summary> Заказ выдан клиенту </summary>
    Completed = 3,

    /// <summary> Заказ отменен </summary>
    Cancelled = 4
}