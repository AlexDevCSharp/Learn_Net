using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 27: конструкторы — цепочка this()/base(), статические.</summary>
public sealed class ConstructorsLesson : LessonBase
{
    public override string Id => "2.27-constructors";
    public override int Module => 2;
    public override string Title => "Конструкторы: цепочка и статический";
    public override Level Level => Level.Beginner;
    public override string Category => "ООП";

    public override string Summary =>
        "Инициализация объекта, вызов одного конструктора из другого (this) и статический ctor.";

    public override string Explanation =>
        """
        Что это. Конструктор — специальный метод без типа возврата, с именем класса, который
        вызывается при new и задаёт начальное состояние объекта. Если не написать ни одного,
        компилятор даёт неявный пустой конструктор.

        Цепочка this(). Конструктор может вызвать другой конструктор ЭТОГО же класса через
        : this(...). Так убирают дублирование: «короткий» конструктор задаёт значения по
        умолчанию и делегирует «полному».

        Цепочка base(). Конструктор наследника вызывает конструктор базового класса через
        : base(...), передавая ему нужные аргументы (подробнее — в теме наследования).

        Статический конструктор. static Ctor() без параметров и модификаторов доступа
        выполняется ОДИН раз, автоматически, перед первым использованием типа. Нужен для
        инициализации статических данных. Явно его не вызывают.
        """;

    public override string Code =>
        """
        // class Order {
        //     public int Id { get; }
        //     public decimal Total { get; }
        //     public string Currency { get; }
        //     public Order(int id, decimal total, string currency) { ... }
        //     public Order(int id, decimal total) : this(id, total, "USD") { }  // делегирует
        // }

        var full = new Order(1, 99.90m, "EUR");
        var shortCtor = new Order(2, 50m);   // валюта по умолчанию через this(...)

        output.Line("Полный конструктор", $"{full.Total} {full.Currency}");
        output.Line("Короткий конструктор (this→USD)", $"{shortCtor.Total} {shortCtor.Currency}");
        """;

    protected override void Demo(DemoResult output)
    {
        var full = new Order(1, 99.90m, "EUR");
        var shortCtor = new Order(2, 50m);

        output.Line("Полный конструктор", $"{full.Total} {full.Currency}");
        output.Line("Короткий конструктор (this→USD)", $"{shortCtor.Total} {shortCtor.Currency}");
    }

    public override Quiz Quiz => new(
        "Что делает : this(...) в объявлении конструктора?",
        new[]
        {
            "Вызывает конструктор базового класса",
            "Вызывает другой конструктор этого же класса, чтобы не дублировать код",
            "Создаёт статическое поле",
            "Возвращает значение из конструктора"
        },
        1,
        ": this(...) делегирует инициализацию другому конструктору того же класса.");
}

file sealed class Order
{
    public int Id { get; }
    public decimal Total { get; }
    public string Currency { get; }

    public Order(int id, decimal total, string currency)
    {
        Id = id;
        Total = total;
        Currency = currency;
    }

    // делегирует «полному» конструктору со значением валюты по умолчанию
    public Order(int id, decimal total) : this(id, total, "USD")
    {
    }
}
