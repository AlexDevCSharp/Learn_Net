using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 15: null-операторы — ?., ??, ??=.</summary>
public sealed class NullOperatorsLesson : LessonBase
{
    public override string Id => "0.15-null-operators";
    public override int Module => 0;
    public override string Title => "Null-операторы: ?. ?? ??=";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Безопасный доступ ?. и подстановка значений по умолчанию через ?? и ??=.";

    public override string Explanation =>
        """
        ?. — null-conditional. Обращается к члену только если объект не null, иначе всё
        выражение возвращает null (без NullReferenceException). name?.Length вернёт длину или
        null. Цепочки тоже безопасны: a?.b?.c.

        ?? — null-coalescing. Возвращает левый операнд, если он не null, иначе правый —
        значение по умолчанию. name ?? "аноним" даст "аноним", если name == null.

        ??= — null-coalescing assignment. Присваивает переменной значение, только если она
        сейчас null. name ??= "гость" — «если пусто, поставь гостя». Удобно для ленивой
        инициализации.

        Зачем. Эти операторы убирают горы ручных проверок if (x != null) и делают код короче и
        безопаснее. В связке с nullable reference types (?) компилятор ещё и подсказывает, где
        значение может быть null.
        """;

    public override string Code =>
        """
        string? name = null;

        output.Line("name?.Length (null-conditional)", name?.Length); // null
        output.Line("name ?? запасное", name ?? "аноним");            // аноним

        name ??= "гость";          // присвоить, только если было null
        output.Line("после ??=", name);                              // гость

        // ?. в цепочке с LINQ: товара нет — не падаем
        Product? missing = ShopData.Catalog.FirstOrDefault(p => p.Id == 999);
        output.Line("Нет товара, ?. + ??", missing?.Name ?? "не найдено");
        """;

    protected override void Demo(DemoResult output)
    {
        string? name = null;

        output.Line("name?.Length (null-conditional)", name?.Length);
        output.Line("name ?? запасное", name ?? "аноним");

        name ??= "гость";
        output.Line("после ??=", name);

        Product? missing = ShopData.Catalog.FirstOrDefault(p => p.Id == 999);
        output.Line("Нет товара, ?. + ??", missing?.Name ?? "не найдено");
    }

    public override Quiz Quiz => new(
        "Что делает оператор ??= в выражении name ??= \"гость\"?",
        new[]
        {
            "Всегда присваивает \"гость\"",
            "Присваивает \"гость\", только если name сейчас null",
            "Сравнивает name с \"гость\"",
            "Бросает исключение, если name null"
        },
        1,
        "??= присваивает правое значение, только когда левая переменная равна null.");
}
