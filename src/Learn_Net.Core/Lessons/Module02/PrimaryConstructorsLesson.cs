using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 28: primary constructors.</summary>
public sealed class PrimaryConstructorsLesson : LessonBase
{
    public override string Id => "2.28-primary-constructors";
    public override int Module => 2;
    public override string Title => "Primary constructors";
    public override Level Level => Level.Beginner;
    public override string Category => "ООП";

    public override string Summary =>
        "Параметры конструктора прямо в объявлении класса — меньше шаблонного кода.";

    public override string Explanation =>
        """
        Что это. Primary constructor (C# 12) позволяет объявить параметры конструктора прямо в
        заголовке класса: class PricedItem(string name, decimal price). Эти параметры доступны
        во всём теле класса — в свойствах, методах, инициализаторах полей.

        Зачем. Убирает шаблон: не нужно вручную объявлять поля, писать конструктор и присваивать
        this.x = x. Особенно удобно для небольших классов и для внедрения зависимостей (сервис
        принимает зависимости через primary constructor).

        Отличие от record. У обычного класса primary-параметры — это НЕ автоматические публичные
        свойства (в отличие от record). Они просто доступны внутри; чтобы выставить наружу,
        объявляют свойство: public string Name => name.

        Наследование. Класс с primary constructor передаёт аргументы базовому так:
        class Sub(int x) : Base(x). Читается компактно.
        """;

    public override string Code =>
        """
        // class PricedItem(string name, decimal price)
        // {
        //     public string Name => name;              // параметр виден в теле класса
        //     public decimal PriceWithTax => price * 1.2m;
        // }

        var item = new PricedItem("Keyboard", 80m);
        output.Line("Name", item.Name);
        output.Line("Цена с налогом (×1.2)", item.PriceWithTax);
        """;

    protected override void Demo(DemoResult output)
    {
        var item = new PricedItem("Keyboard", 80m);
        output.Line("Name", item.Name);
        output.Line("Цена с налогом (×1.2)", item.PriceWithTax);
    }

    public override Quiz Quiz => new(
        "Что даёт primary constructor у обычного класса (не record)?",
        new[]
        {
            "Автоматически создаёт публичные свойства для каждого параметра",
            "Делает параметры доступными во всём теле класса без ручного объявления полей",
            "Запрещает добавлять другие конструкторы",
            "Превращает класс в структуру"
        },
        1,
        "Primary-параметры видны во всём теле класса; публичными свойствами их надо выставить явно (в отличие от record).");
}

file sealed class PricedItem(string name, decimal price)
{
    public string Name => name;
    public decimal PriceWithTax => price * 1.2m;
}
