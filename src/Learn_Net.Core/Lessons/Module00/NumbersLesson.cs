using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 8: числовые типы, decimal vs double, overflow и checked.</summary>
public sealed class NumbersLesson : LessonBase
{
    public override string Id => "0.08-numbers";
    public override int Module => 0;
    public override string Title => "Числа: decimal vs double и переполнение";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Почему деньги — это decimal, а не double, и что такое overflow/checked.";

    public override string Explanation =>
        """
        Что это. double/float — числа с плавающей точкой в двоичной системе: быстрые, но
        хранят многие десятичные дроби приближённо. decimal — десятичный тип с большой
        точностью, специально для денег и финансов.

        Классический пример. 0.1 + 0.2 в double даёт 0.30000000000000004, потому что 0.1
        не представима точно в двоичной дроби. В decimal — ровно 0.3. Поэтому цены, суммы
        заказов, налоги считаем в decimal (литерал с суффиксом m: 9.99m).

        Переполнение (overflow). Целые типы имеют границы (int: примерно ±2.1 млрд). По
        умолчанию арифметика unchecked: при выходе за границу значение «заворачивается»
        (int.MaxValue + 1 → int.MinValue) молча, без ошибки. Блок checked заставляет среду
        бросить OverflowException — полезно, когда молчаливый переворот недопустим.

        Правило. Деньги и точные десятичные — decimal. Наука/графика/производительность —
        double. Там, где переполнение опасно — оборачивай в checked.
        """;

    protected override void Demo(DemoResult output)
    {
        output.Line("double: 0.1 + 0.2", 0.1 + 0.2);
        output.Line("decimal: 0.1m + 0.2m", 0.1m + 0.2m);

        output.Line();
        int max = int.MaxValue;
        output.Line("int.MaxValue", max);
        output.Line("unchecked: MaxValue + 1", unchecked(max + 1));

        try
        {
            _ = checked(max + 1);
        }
        catch (OverflowException)
        {
            output.Line("checked: MaxValue + 1", "бросил OverflowException");
        }
    }

    public override Quiz Quiz => new(
        "Какой тип выбрать для хранения цены товара и суммы заказа?",
        new[]
        {
            "double — он быстрее",
            "decimal — точные десятичные вычисления без ошибок округления",
            "float — экономит память",
            "int — цены всегда целые"
        },
        1,
        "Деньги считают в decimal: double хранит десятичные дроби приближённо.");
}
