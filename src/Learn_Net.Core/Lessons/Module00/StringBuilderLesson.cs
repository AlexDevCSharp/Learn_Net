using System.Text;
using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 11: StringBuilder и производительность конкатенации.</summary>
public sealed class StringBuilderLesson : LessonBase
{
    public override string Id => "0.11-stringbuilder";
    public override int Module => 0;
    public override string Title => "StringBuilder и конкатенация";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Почему склейка строк в цикле дорогая и как её ускоряет StringBuilder.";

    public override string Explanation =>
        """
        Проблема. Строки неизменяемы (см. урок про строки), поэтому s += x в цикле создаёт
        КАЖДЫЙ раз новую строку и копирует туда всё накопленное. N склеек = N промежуточных
        строк и квадратичный объём копирования — лишняя нагрузка на память и GC.

        Решение. StringBuilder — изменяемый буфер символов. Метод Append/AppendLine дописывает
        в тот же буфер (при необходимости увеличивая его ёмкость), а строку мы получаем один
        раз в конце через ToString().

        Когда использовать. Разовая склейка 2–3 строк — обычный + или интерполяция $"...",
        читается лучше и разница незаметна. Склейка в цикле, сборка большого текста/отчёта/чека
        — StringBuilder.

        Нюанс. Если знаете примерный итоговый размер — задайте начальную ёмкость
        (new StringBuilder(capacity)), чтобы избежать лишних перевыделений буфера.
        """;

    public override string Code =>
        """
        // Собираем чек магазина: много строк — работа для StringBuilder.
        var sb = new StringBuilder();
        foreach (var p in ShopData.Catalog.Take(3))
            sb.AppendLine($"{p.Name,-20}{p.Price,8:C}");

        output.Line("=== Чек (через StringBuilder) ===");
        foreach (var line in sb.ToString().TrimEnd().Split(Environment.NewLine))
            output.Line(line);
        """;

    protected override void Demo(DemoResult output)
    {
        var sb = new StringBuilder();
        foreach (var p in ShopData.Catalog.Take(3))
            sb.AppendLine($"{p.Name,-20}{p.Price,8:C}");

        output.Line("=== Чек (через StringBuilder) ===");
        foreach (var line in sb.ToString().TrimEnd().Split(Environment.NewLine))
            output.Line(line);
    }

    public override Quiz Quiz => new(
        "Когда StringBuilder предпочтительнее обычной конкатенации +?",
        new[]
        {
            "Всегда, + использовать нельзя",
            "При многократной склейке в цикле или сборке большого текста",
            "Только для чисел",
            "Когда строк ровно две"
        },
        1,
        "Для склейки в цикле: + плодит промежуточные строки, а StringBuilder пишет в один буфер.");
}
