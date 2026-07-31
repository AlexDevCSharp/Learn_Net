using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module01;

/// <summary>Тема 21: локальные функции.</summary>
public sealed class LocalFunctionsLesson : LessonBase
{
    public override string Id => "1.21-local-functions";
    public override int Module => 1;
    public override string Title => "Локальные функции";
    public override Level Level => Level.Beginner;
    public override string Category => "Управление потоком";

    public override string Summary =>
        "Функция внутри метода: локальный помощник, видит переменные, может быть рекурсивной.";

    public override string Explanation =>
        """
        Что это. Локальная функция объявляется ВНУТРИ другого метода и видна только там. Это
        именованный помощник для логики, которая нужна лишь в этом методе и не заслуживает
        отдельного приватного метода класса.

        Возможности. Локальная функция может захватывать переменные окружающего метода
        (замыкание), вызываться до своего объявления, быть рекурсивной и иметь несколько
        перегрузок нельзя, но обычные параметры/возврат — пожалуйста.

        static-версия. Если пометить локальную функцию как static, она НЕ сможет захватывать
        переменные внешнего метода — только свои параметры. Это защищает от случайных захватов
        и иногда эффективнее (нет выделения объекта-замыкания).

        Зачем. Читаемость и локальность: помощник живёт рядом с местом использования, не
        засоряет класс, а имя делает код понятнее анонимной лямбды.
        """;

    public override string Code =>
        """
        var product = ShopData.Catalog[2]; // C# in Depth, 39.50

        // локальная функция-помощник (захватывает ничего, могла бы — окружающие переменные)
        decimal WithVat(decimal net) => net * 1.2m;

        output.Line("Цена без НДС", product.Price);
        output.Line("Цена с НДС (локальная функция)", WithVat(product.Price));

        // локальная функция может быть рекурсивной
        int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);
        output.Line("5! (рекурсивная локальная функция)", Factorial(5));
        """;

    protected override void Demo(DemoResult output)
    {
        var product = ShopData.Catalog[2];

        decimal WithVat(decimal net) => net * 1.2m;

        output.Line("Цена без НДС", product.Price);
        output.Line("Цена с НДС (локальная функция)", WithVat(product.Price));

        int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);
        output.Line("5! (рекурсивная локальная функция)", Factorial(5));
    }

    public override Quiz Quiz => new(
        "Что умеет локальная функция в отличие от обычной лямбды?",
        new[]
        {
            "Ничего особенного",
            "Иметь имя, быть рекурсивной и захватывать переменные метода (а static — не захватывать)",
            "Работать только с числами",
            "Объявляться вне метода"
        },
        1,
        "Локальная функция именованная, может рекурсивно вызывать себя и захватывать локальные переменные.");
}
