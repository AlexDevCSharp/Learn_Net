namespace LearnNet.Core.Domain;

/// <summary>Товар каталога. Типичный доменный класс, который мы переиспользуем во всех уроках.</summary>
public class Product
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; }
    public Category Category { get; }

    public Product(int id, string name, decimal price, int stock, Category category)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
        Category = category;
    }

    /// <summary>Вычисляемое свойство — есть ли товар в наличии.</summary>
    public bool InStock => Stock > 0;

    public override string ToString() => $"#{Id} {Name} ({Category}) — {Price:C} x{Stock}";
}
