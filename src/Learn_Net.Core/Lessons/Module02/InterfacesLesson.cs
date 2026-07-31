using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 33: интерфейсы как контракт (заодно превью паттерна Strategy).</summary>
public sealed class InterfacesLesson : LessonBase
{
    public override string Id => "2.33-interfaces";
    public override int Module => 2;
    public override string Title => "Интерфейсы как контракт";
    public override Level Level => Level.Beginner;
    public override string Category => "ООП";

    public override string Summary =>
        "Один контракт IDiscount — разные реализации, взаимозаменяемые за счёт полиморфизма.";

    public override string Explanation =>
        """
        Что это. Интерфейс — это контракт: список членов (методы, свойства) без реализации.
        Класс, реализующий интерфейс, обязуется их предоставить. Интерфейс отвечает на вопрос
        «что умеет объект», не диктуя «как именно».

        Зачем. Код зависит от интерфейса, а не от конкретного класса, — и реализацию можно
        подменить, не меняя вызывающий код. Это основа слабой связанности, тестируемости
        (подставляем моки) и Dependency Injection.

        Пример. Контракт IDiscount { decimal Apply(decimal price) } — «умею посчитать цену
        со скидкой». Реализации разные: без скидки, процент, сезонная акция. Метод считает
        итог через переменную типа IDiscount и не знает, какая именно скидка внутри —
        это полиморфизм. По сути это уже паттерн Strategy (тема 110).

        Отличие от класса. Класс можно наследовать только один, а интерфейсов реализовать
        сколько угодно. Интерфейс — про способности, класс — про сущность и общий код.
        """;

    protected override void Demo(DemoResult output)
    {
        var product = ShopData.Catalog[1]; // Mechanical Keyboard, 79.90
        output.Line("Товар", product.Name);
        output.Line("Базовая цена", product.Price);

        // Разные реализации одного контракта — взаимозаменяемы.
        IDiscount[] discounts =
        {
            new NoDiscount(),
            new PercentageDiscount(10),
            new PercentageDiscount(25),
        };

        output.Line();
        foreach (IDiscount discount in discounts)
            output.Line(discount.Name, discount.Apply(product.Price));
    }

    public override Quiz Quiz => new(
        "Зачем метод расчёта принимает параметр типа IDiscount, а не конкретный класс скидки?",
        new[]
        {
            "Так быстрее выполняется",
            "Чтобы подменять реализацию без изменения кода — слабая связанность и полиморфизм",
            "Интерфейсы обязательны в C#",
            "Чтобы скидка стала value-типом"
        },
        1,
        "Зависимость от контракта (интерфейса) позволяет подставлять любую реализацию — основа гибкости и тестируемости.");
}

/// <summary>Контракт стратегии скидки.</summary>
public interface IDiscount
{
    string Name { get; }
    decimal Apply(decimal price);
}

/// <summary>Скидки нет — цена без изменений.</summary>
public sealed class NoDiscount : IDiscount
{
    public string Name => "Без скидки";
    public decimal Apply(decimal price) => price;
}

/// <summary>Процентная скидка.</summary>
public sealed class PercentageDiscount : IDiscount
{
    private readonly decimal _percent;

    public PercentageDiscount(decimal percent) => _percent = percent;

    public string Name => $"Скидка {_percent}%";
    public decimal Apply(decimal price) => price * (1 - _percent / 100m);
}
