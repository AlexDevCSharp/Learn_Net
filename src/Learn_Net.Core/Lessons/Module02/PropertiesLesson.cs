using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 26: свойства — auto, init, expression-bodied, вычисляемые.</summary>
public sealed class PropertiesLesson : LessonBase
{
    public override string Id => "2.26-properties";
    public override int Module => 2;
    public override string Title => "Свойства: auto, init, вычисляемые";
    public override Level Level => Level.Beginner;
    public override string Category => "ООП";

    public override string Summary =>
        "Автосвойства, init-only, вычисляемые свойства и контроль доступа к данным.";

    public override string Explanation =>
        """
        Что это. Свойство — «умное поле»: снаружи выглядит как поле, но за get/set стоят методы.
        Это даёт инкапсуляцию — можно валидировать, вычислять и ограничивать доступ, не меняя
        синтаксис обращения.

        Авто-свойства. public string Name { get; set; } — компилятор сам создаёт скрытое поле.
        Доступ можно сузить: { get; private set; } — читать всем, писать только внутри класса.

        init-only. { get; init; } разрешает присвоение ТОЛЬКО при создании объекта (в
        конструкторе или через объектный инициализатор), а дальше свойство неизменяемо. Отличный
        способ сделать иммутабельные объекты.

        Вычисляемые (expression-bodied). Свойство без хранимого поля, которое считается на лету:
        public bool IsVip => OrderCount >= 10. Значение вычисляется при каждом обращении.
        """;

    public override string Code =>
        """
        // class Customer {
        //     public string Name { get; init; }       // задаётся только при создании
        //     public int OrderCount { get; set; }      // обычное авто-свойство
        //     public bool IsVip => OrderCount >= 10;    // вычисляемое
        // }

        var c = new Customer { Name = "Alex", OrderCount = 3 };
        output.Line("Name (init-only)", c.Name);
        output.Line("IsVip при 3 заказах", c.IsVip);

        c.OrderCount = 12;                       // set разрешён
        output.Line("IsVip при 12 заказах", c.IsVip);
        // c.Name = "Bob";  // ← ошибка компиляции: init-only после создания менять нельзя
        """;

    protected override void Demo(DemoResult output)
    {
        var c = new Customer { Name = "Alex", OrderCount = 3 };
        output.Line("Name (init-only)", c.Name);
        output.Line("IsVip при 3 заказах", c.IsVip);

        c.OrderCount = 12;
        output.Line("IsVip при 12 заказах", c.IsVip);
    }

    public override Quiz Quiz => new(
        "Что означает свойство с { get; init; }?",
        new[]
        {
            "Его нельзя читать",
            "Присвоить можно только при создании объекта, дальше — только чтение",
            "Оно всегда вычисляется на лету",
            "Оно доступно только внутри класса"
        },
        1,
        "init-only свойство задаётся при инициализации объекта и потом становится неизменяемым.");
}

file sealed class Customer
{
    public string Name { get; init; } = "";
    public int OrderCount { get; set; }
    public bool IsVip => OrderCount >= 10;
}
