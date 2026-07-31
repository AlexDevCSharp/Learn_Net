namespace LearnNet.Core.Domain;

/// <summary>Статус заказа в магазине — удобный enum для уроков про switch и pattern matching.</summary>
public enum OrderStatus
{
    Pending,
    Paid,
    Shipped,
    Delivered,
    Cancelled
}
