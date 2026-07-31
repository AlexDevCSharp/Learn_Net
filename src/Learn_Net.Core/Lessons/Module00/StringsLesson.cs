using System.Text;
using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 10–11: строки, immutability и StringBuilder.</summary>
public sealed class StringsLesson : LessonBase
{
    public override string Id => "0.10-strings";
    public override int Module => 0;
    public override string Title => "Строки: immutability и StringBuilder";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Почему строку нельзя изменить и когда вместо + нужен StringBuilder.";

    public override string Explanation =>
        """
        Что это. Строка (string) — это ссылочный тип, но ведёт себя как значение:
        экземпляр неизменяем (immutable). Любой «изменяющий» метод — ToUpper, Replace,
        Trim, конкатенация + — не трогает исходную строку, а возвращает НОВУЮ.

        Зачем так. Неизменяемость даёт безопасность: одну строку можно свободно
        передавать в методы и шарить между потоками, не боясь, что её испортят. Плюс
        работает интернирование — одинаковые строковые литералы могут ссылаться на один
        объект в памяти.

        Обратная сторона. Склейка в цикле дорогая: каждое s += x создаёт новую строку и
        копирует всё содержимое. N склеек = N промежуточных строк и лишняя нагрузка на GC.
        Для этого есть StringBuilder — изменяемый буфер, который накапливает символы и один
        раз отдаёт результат через ToString().

        Правило. Разовая склейка 2–3 строк — обычный + или интерполяция $"...".
        Склейка в цикле или сборка большого текста — StringBuilder.
        """;

    protected override void Demo(DemoResult output)
    {
        var name = "keyboard";
        var upper = name.ToUpperInvariant();

        output.Line("Исходная строка не изменилась", name);
        output.Line("ToUpper вернул новую строку", upper);
        output.Line("Ссылка та же?", ReferenceEquals(name, name.ToUpperInvariant()));

        // Формируем чек магазина через StringBuilder.
        var sb = new StringBuilder();
        foreach (var p in ShopData.Catalog.Take(3))
            sb.AppendLine($"{p.Name,-20}{p.Price,8:C}");

        output.Line();
        output.Line("=== Чек (через StringBuilder) ===");
        foreach (var line in sb.ToString().TrimEnd().Split(Environment.NewLine))
            output.Line(line);
    }

    public override Quiz Quiz => new(
        "Что делает string.ToUpper() с исходной строкой?",
        new[]
        {
            "Меняет её на месте",
            "Возвращает новую строку, исходная не меняется",
            "Бросает исключение для immutable-строк",
            "Меняет только первую букву"
        },
        1,
        "Строки неизменяемы — строковые методы возвращают новый экземпляр.");
}
