namespace LearnNet.Core.Domain;

/// <summary>Сид-данные каталога. Единый источник товаров для всех уроков.</summary>
public static class ShopData
{
    public static IReadOnlyList<Product> Catalog { get; } = new List<Product>
    {
        new(1, "USB-C Cable",        9.99m, 120, Category.Electronics),
        new(2, "Mechanical Keyboard", 79.90m, 15, Category.Electronics),
        new(3, "C# in Depth",        39.50m,   8, Category.Books),
        new(4, "Clean Code",         32.00m,   0, Category.Books),
        new(5, "T-Shirt",            14.99m,  60, Category.Clothing),
        new(6, "Coffee Beans 1kg",   21.75m,  40, Category.Groceries),
    };
}
