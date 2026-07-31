using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module01;

/// <summary>Тема 24: рекурсия и стек вызовов.</summary>
public sealed class RecursionLesson : LessonBase
{
    public override string Id => "1.24-recursion";
    public override int Module => 1;
    public override string Title => "Рекурсия и стек вызовов";
    public override Level Level => Level.Beginner;
    public override string Category => "Управление потоком";

    public override string Summary =>
        "Метод, вызывающий сам себя; зачем нужен базовый случай и чем грозит стек.";

    public override string Explanation =>
        """
        Что это. Рекурсия — когда метод вызывает сам себя, сводя задачу к более простому случаю.
        Обязательны две части: базовый случай (условие остановки) и рекурсивный шаг (вызов себя
        с «меньшим» аргументом, приближающим к базовому случаю).

        Стек вызовов. Каждый вызов метода кладёт на стек кадр (frame) с его локальными данными.
        Рекурсия наращивает стек на каждый вложенный вызов и сворачивает его, когда вызовы
        возвращаются. Возврат идёт в обратном порядке — от базового случая назад.

        Опасность. Без базового случая (или если он недостижим) рекурсия бесконечна и упрётся в
        предел стека — StackOverflowException, которую нельзя перехватить. Слишком глубокая
        рекурсия тоже переполнит стек.

        Когда использовать. Рекурсия элегантна для древовидных/иерархических задач (обход
        дерева, факториал, разбор структуры). Для простых линейных повторений цикл часто
        эффективнее и безопаснее по стеку.
        """;

    public override string Code =>
        """
        // базовый случай: n <= 1; шаг: n * Factorial(n - 1)
        int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);

        // сумма 1..n через рекурсию
        int SumTo(int n) => n == 0 ? 0 : n + SumTo(n - 1);

        output.Line("5! (рекурсия)", Factorial(5));      // 120
        output.Line("Сумма 1..100 (рекурсия)", SumTo(100)); // 5050
        """;

    protected override void Demo(DemoResult output)
    {
        int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);
        int SumTo(int n) => n == 0 ? 0 : n + SumTo(n - 1);

        output.Line("5! (рекурсия)", Factorial(5));
        output.Line("Сумма 1..100 (рекурсия)", SumTo(100));
    }

    public override Quiz Quiz => new(
        "Что произойдёт с рекурсией без достижимого базового случая?",
        new[]
        {
            "Она вернёт 0",
            "Бесконечные вызовы переполнят стек — StackOverflowException",
            "Компилятор не даст собрать",
            "Ничего, рекурсия остановится сама"
        },
        1,
        "Без базового случая вызовы не прекращаются и переполняют стек вызовов (StackOverflowException).");
}
