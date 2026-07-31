using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module01;

/// <summary>Тема 18: циклы for / while / do-while.</summary>
public sealed class LoopsLesson : LessonBase
{
    public override string Id => "1.18-loops";
    public override int Module => 1;
    public override string Title => "Циклы: for, while, do-while";
    public override Level Level => Level.Beginner;
    public override string Category => "Управление потоком";

    public override string Summary =>
        "Три вида циклов и когда какой уместен; do-while выполняется хотя бы раз.";

    public override string Explanation =>
        """
        for. Классический цикл со счётчиком: for (инициализация; условие; шаг). Удобен, когда
        известно число итераций или нужен индекс. Все три части опциональны.

        while. Проверяет условие ПЕРЕД телом: если оно ложно сразу, тело не выполнится ни разу.
        Подходит, когда число повторений заранее неизвестно и зависит от условия.

        do-while. Проверяет условие ПОСЛЕ тела, поэтому тело выполняется минимум один раз.
        Нужен, когда действие надо сделать хотя бы раз, а потом решать, повторять ли (например,
        прочитать ввод, затем проверить).

        Управление. break досрочно выходит из цикла, continue пропускает остаток текущей
        итерации. Для перебора коллекций обычно берут foreach (следующая тема) — он безопаснее
        и читается лучше, чем for по индексу.
        """;

    public override string Code =>
        """
        // for: сумма цен первых трёх товаров
        decimal sum = 0;
        for (int i = 0; i < 3; i++)
            sum += ShopData.Catalog[i].Price;
        output.Line("Сумма первых 3 (for)", sum);

        // while: сколько товаров подряд с начала есть в наличии
        int j = 0;
        while (j < ShopData.Catalog.Count && ShopData.Catalog[j].InStock)
            j++;
        output.Line("Подряд в наличии (while)", j);

        // do-while: тело выполнится минимум один раз
        int n = 0;
        do { n++; } while (n < 1);
        output.Line("do-while выполнился раз", n);
        """;

    protected override void Demo(DemoResult output)
    {
        decimal sum = 0;
        for (int i = 0; i < 3; i++)
            sum += ShopData.Catalog[i].Price;
        output.Line("Сумма первых 3 (for)", sum);

        int j = 0;
        while (j < ShopData.Catalog.Count && ShopData.Catalog[j].InStock)
            j++;
        output.Line("Подряд в наличии (while)", j);

        int n = 0;
        do { n++; } while (n < 1);
        output.Line("do-while выполнился раз", n);
    }

    public override Quiz Quiz => new(
        "Чем do-while отличается от while?",
        new[]
        {
            "Ничем",
            "do-while проверяет условие после тела, поэтому тело выполняется минимум один раз",
            "do-while работает только с числами",
            "while всегда выполняется хотя бы раз"
        },
        1,
        "do-while проверяет условие в конце → тело гарантированно выполнится хотя бы раз.");
}
