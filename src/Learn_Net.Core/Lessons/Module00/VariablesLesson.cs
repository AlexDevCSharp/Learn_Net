using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 13: переменные — var, const, readonly, default.</summary>
public sealed class VariablesLesson : LessonBase
{
    public override string Id => "0.13-variables";
    public override int Module => 0;
    public override string Title => "Переменные: var, const, readonly, default";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Вывод типа var, константы времени компиляции и значения по умолчанию.";

    public override string Explanation =>
        """
        var. Это НЕ «динамический тип», а вывод типа компилятором: var count = 5 — count всё
        равно строго int, просто тип выведен из выражения. Типизация остаётся статической.
        var удобен, когда тип очевиден из правой части.

        const. Константа времени компиляции: значение подставляется прямо в код при сборке.
        Поэтому const обязана инициализироваться литералом и допускает только простые типы
        (числа, bool, string). const double Vat = 0.2.

        readonly. Поле, которое можно присвоить только в объявлении или в конструкторе, дальше
        — только читать. В отличие от const, значение может вычисляться в рантайме (например,
        зависеть от параметров конструктора).

        default. Ключевое слово даёт значение по умолчанию для типа: 0 для чисел, false для
        bool, null для ссылочных типов. default(int) == 0, default(string) == null.
        """;

    public override string Code =>
        """
        var count = 5;            // тип выведен как int (статически)
        const double Vat = 0.2;   // константа времени компиляции

        output.Line("var count тип", count.GetType().Name);   // Int32
        output.Line("const Vat", Vat);
        output.Line("default(int)", default(int));            // 0

        string? maybe = default;                              // default(string) == null
        output.Line("default(string) is null", maybe is null); // True
        """;

    protected override void Demo(DemoResult output)
    {
        var count = 5;
        const double Vat = 0.2;

        output.Line("var count тип", count.GetType().Name);
        output.Line("const Vat", Vat);
        output.Line("default(int)", default(int));

        string? maybe = default;
        output.Line("default(string) is null", maybe is null);
    }

    public override Quiz Quiz => new(
        "Чем const отличается от readonly?",
        new[]
        {
            "Ничем",
            "const — значение времени компиляции (литерал); readonly можно вычислить в рантайме (в конструкторе)",
            "readonly работает только со строками",
            "const можно менять в конструкторе"
        },
        1,
        "const вшивается при компиляции и требует литерал; readonly присваивается один раз, в т.ч. в рантайме.");
}
