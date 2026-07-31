using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Module02;

namespace LearnNet.Tests;

/// <summary>Тесты доменной логики — заодно живой пример Arrange-Act-Assert и [Theory].</summary>
public class DomainTests
{
    [Fact]
    public void Catalog_IsNotEmpty()
    {
        Assert.NotEmpty(ShopData.Catalog);
    }

    [Fact]
    public void Product_InStock_TrueWhenStockPositive()
    {
        // Arrange
        var product = new Product(1, "Test", 10m, stock: 3, Category.Electronics);

        // Act
        var inStock = product.InStock;

        // Assert
        Assert.True(inStock);
    }

    [Fact]
    public void Product_InStock_FalseWhenStockZero()
    {
        var product = new Product(1, "Test", 10m, stock: 0, Category.Electronics);

        Assert.False(product.InStock);
    }

    [Theory]
    [InlineData(0, 100)]
    [InlineData(10, 90)]
    [InlineData(25, 75)]
    [InlineData(50, 50)]
    public void PercentageDiscount_AppliesExpectedPrice(double percent, double expected)
    {
        // Arrange
        var discount = new PercentageDiscount((decimal)percent);

        // Act
        var result = discount.Apply(100m);

        // Assert
        Assert.Equal((decimal)expected, result);
    }

    [Fact]
    public void NoDiscount_LeavesPriceUnchanged()
    {
        var discount = new NoDiscount();

        Assert.Equal(79.90m, discount.Apply(79.90m));
    }
}
