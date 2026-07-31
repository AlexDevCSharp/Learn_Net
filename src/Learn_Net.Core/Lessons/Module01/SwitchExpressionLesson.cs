using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module01;

/// <summary>Тема 17: switch statement vs switch expression.</summary>
public sealed class SwitchExpressionLesson : LessonBase
{
    public override string Id => "1.17-switch-expression";
    public override int Module => 1;
    public override string Title => "switch: statement vs expression";
    public override Level Level => Level.Beginner;
    public override string Category => "Управление потоком";

    public override string Summary =>
        "Компактный switch-expression, который возвращает значение, на примере OrderStatus.";

    public override string Explanation =>
        """
        Что это. Классический switch-statement — управляющая конструкция: набор case с
        break, которые что-то ДЕЛАЮТ. switch-expression (C# 8+) — это выражение, которое
        ВОЗВРАЩАЕТ значение: короче, без break, с лямбда-стрелками => и ветками через запятую.

        Синтаксис. status switch { OrderStatus.Paid => "Оплачен", _ => "?" }. Слева от switch
        стоит проверяемое значение, _ — обязательная ветка «по умолчанию» (discard).

        Зачем. Когда по входу нужно вернуть одно из значений (метка, цена, коэффициент),
        switch-expression читается как таблица соответствий и не даёт забыть про случаи.
        Компилятор ещё и предупредит, если не покрыты все варианты enum.

        Плюс. Отлично сочетается с pattern matching: в ветках можно проверять типы, свойства
        и диапазоны (relational patterns), не только константы. Это фундамент для темы 74.
        """;

    public override string Code =>
        """
        foreach (OrderStatus status in Enum.GetValues<OrderStatus>())
            output.Line(status.ToString(), Describe(status));

        // switch-expression: возвращает человекочитаемую метку статуса
        static string Describe(OrderStatus status) => status switch
        {
            OrderStatus.Pending   => "⏳ Ожидает оплаты",
            OrderStatus.Paid      => "💳 Оплачен",
            OrderStatus.Shipped   => "📦 Отправлен",
            OrderStatus.Delivered => "✅ Доставлен",
            OrderStatus.Cancelled => "❌ Отменён",
            _                     => "Неизвестный статус"
        };
        """;

    protected override void Demo(DemoResult output)
    {
        foreach (OrderStatus status in Enum.GetValues<OrderStatus>())
            output.Line(status.ToString(), Describe(status));
    }

    // switch-expression: возвращает человекочитаемую метку статуса.
    private static string Describe(OrderStatus status) => status switch
    {
        OrderStatus.Pending   => "⏳ Ожидает оплаты",
        OrderStatus.Paid      => "💳 Оплачен",
        OrderStatus.Shipped   => "📦 Отправлен",
        OrderStatus.Delivered => "✅ Доставлен",
        OrderStatus.Cancelled => "❌ Отменён",
        _                     => "Неизвестный статус"
    };

    public override Quiz Quiz => new(
        "Чем switch-expression отличается от классического switch-statement?",
        new[]
        {
            "Ничем, это синонимы",
            "Он возвращает значение и пишется компактно, без break",
            "Работает только с int",
            "Требует обязательный break в каждой ветке"
        },
        1,
        "switch-expression — это выражение, возвращающее значение; ветки через =>, без break.");
}
