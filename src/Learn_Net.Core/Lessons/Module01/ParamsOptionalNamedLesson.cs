using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module01;

/// <summary>Тема 23: params, опциональные и именованные параметры.</summary>
public sealed class ParamsOptionalNamedLesson : LessonBase
{
    public override string Id => "1.23-params-optional-named";
    public override int Module => 1;
    public override string Title => "params, опциональные и именованные аргументы";
    public override Level Level => Level.Beginner;
    public override string Category => "Управление потоком";

    public override string Summary =>
        "Переменное число аргументов (params), значения по умолчанию и вызов по имени.";

    public override string Explanation =>
        """
        params. Позволяет передать переменное число аргументов, которые метод получит как
        массив: Sum(params int[] nums). Вызвать можно и без аргументов, и с любым их числом:
        Sum(), Sum(1, 2, 3). params-параметр всегда последний в списке.

        Опциональные параметры. Параметр со значением по умолчанию можно не указывать при
        вызове: Greet(string name, string greeting = "Hello"). Значение по умолчанию должно быть
        константой и идти после обязательных параметров.

        Именованные аргументы. При вызове можно указать имя параметра: Greet("Alex",
        greeting: "Привет"). Это повышает читаемость и позволяет пропускать опциональные
        параметры в середине, задавая только нужные.

        Зачем. Вместе это делает API гибким: один метод покрывает разные сценарии вызова без
        десятка перегрузок.
        """;

    public override string Code =>
        """
        // params: любое число аргументов
        int Sum(params int[] nums)
        {
            int total = 0;
            foreach (var n in nums) total += n;
            return total;
        }

        // опциональный параметр greeting со значением по умолчанию
        string Greet(string name, string greeting = "Hello") => $"{greeting}, {name}!";

        output.Line("Sum()", Sum());
        output.Line("Sum(1, 2, 3)", Sum(1, 2, 3));
        output.Line("Greet (по умолчанию)", Greet("Alex"));
        output.Line("Greet (именованный)", Greet("Alex", greeting: "Привет"));
        """;

    protected override void Demo(DemoResult output)
    {
        int Sum(params int[] nums)
        {
            int total = 0;
            foreach (var n in nums) total += n;
            return total;
        }

        string Greet(string name, string greeting = "Hello") => $"{greeting}, {name}!";

        output.Line("Sum()", Sum());
        output.Line("Sum(1, 2, 3)", Sum(1, 2, 3));
        output.Line("Greet (по умолчанию)", Greet("Alex"));
        output.Line("Greet (именованный)", Greet("Alex", greeting: "Привет"));
    }

    public override Quiz Quiz => new(
        "Что позволяет ключевое слово params в объявлении метода?",
        new[]
        {
            "Сделать параметр обязательным",
            "Передавать переменное число аргументов, которые метод получит как массив",
            "Передать параметр по ссылке",
            "Задать значение по умолчанию"
        },
        1,
        "params принимает любое число аргументов (в т.ч. ноль) и упаковывает их в массив.");
}
