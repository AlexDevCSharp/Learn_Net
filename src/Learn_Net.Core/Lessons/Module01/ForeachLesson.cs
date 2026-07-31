using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module01;

/// <summary>Тема 19: foreach и как он работает под капотом.</summary>
public sealed class ForeachLesson : LessonBase
{
    public override string Id => "1.19-foreach";
    public override int Module => 1;
    public override string Title => "foreach: как он работает под капотом";
    public override Level Level => Level.Beginner;
    public override string Category => "Управление потоком";

    public override string Summary =>
        "foreach — это сахар над GetEnumerator / MoveNext / Current.";

    public override string Explanation =>
        """
        Что это. foreach перебирает элементы коллекции без индекса: foreach (var x in items).
        Работает с любым типом, у которого есть перечислитель — то есть реализующим IEnumerable
        (массивы, List, Dictionary, строки, результаты LINQ и т.д.).

        Под капотом. Компилятор разворачивает foreach в примерно такой код: берётся
        enumerator = items.GetEnumerator(), затем в цикле while (enumerator.MoveNext()) читается
        enumerator.Current. Если перечислитель реализует IDisposable, он корректно освобождается
        в finally.

        Зачем знать. Это объясняет, почему foreach работает с чем угодно перечислимым и почему
        внутри него нельзя обращаться к индексу (его просто нет) — если нужен индекс, берите for
        или Select с индексом.

        Подвох. Нельзя менять саму коллекцию (Add/Remove) во время foreach — это бросит
        InvalidOperationException, потому что перечислитель обнаружит, что коллекция изменилась.
        """;

    public override string Code =>
        """
        // обычный foreach
        foreach (var p in ShopData.Catalog.Take(3))
            output.Line("товар", p.Name);

        // то же самое, но вручную — так foreach выглядит под капотом
        using var e = ShopData.Catalog.GetEnumerator();
        int count = 0;
        while (e.MoveNext())
        {
            var current = e.Current;
            count++;
        }
        output.Line("Прошли вручную через enumerator", count);
        """;

    protected override void Demo(DemoResult output)
    {
        foreach (var p in ShopData.Catalog.Take(3))
            output.Line("товар", p.Name);

        using var e = ShopData.Catalog.GetEnumerator();
        int count = 0;
        while (e.MoveNext())
        {
            _ = e.Current;
            count++;
        }
        output.Line("Прошли вручную через enumerator", count);
    }

    public override Quiz Quiz => new(
        "Во что компилятор разворачивает foreach?",
        new[]
        {
            "В обычный for со счётчиком по индексу",
            "В вызовы GetEnumerator(), затем цикл MoveNext() и чтение Current",
            "В рекурсию",
            "В LINQ-запрос"
        },
        1,
        "foreach — сахар: GetEnumerator() → while(MoveNext()) → Current, с Dispose в конце.");
}
