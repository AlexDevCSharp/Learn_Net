using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 34: default interface methods и explicit implementation.</summary>
public sealed class DefaultInterfaceMethodsLesson : LessonBase
{
    public override string Id => "2.34-default-interface-methods";
    public override int Module => 2;
    public override string Title => "Default-методы и явная реализация интерфейса";
    public override Level Level => Level.Intermediate;
    public override string Category => "ООП";

    public override string Summary =>
        "Метод с телом прямо в интерфейсе и разрешение конфликта имён через explicit implementation.";

    public override string Explanation =>
        """
        Default interface methods (C# 8). Интерфейс может дать методу РЕАЛИЗАЦИЮ по умолчанию:
        LogError(string m) => Log("ERROR: " + m). Классы, реализующие интерфейс, могут её не
        переопределять. Главная цель — добавлять новые методы в существующий интерфейс, не ломая
        уже написанные реализации.

        Нюанс вызова. Default-метод доступен только через ссылку ИНТЕРФЕЙСА, а не через переменную
        конкретного класса (если класс его не переобъявил). То есть ((ILogger)obj).LogError(...).

        Explicit implementation. Когда класс реализует два интерфейса с методом одинакового имени
        (или мы хотим «спрятать» член), реализацию пишут явно: string IEnglish.Greet() {...}.
        Такой член не виден через переменную класса — только после приведения к нужному интерфейсу.

        Зачем. Явная реализация разруливает конфликты имён и позволяет держать «служебные» члены
        интерфейса вне публичной поверхности класса.
        """;

    public override string Code =>
        """
        // interface ILogger {
        //     void Log(string m);
        //     void LogError(string m) => Log("ERROR: " + m);   // default-метод с телом
        // }
        // class ListLogger : ILogger { public List<string> Lines = new(); public void Log(string m) => Lines.Add(m); }

        var logger = new ListLogger();
        ((ILogger)logger).LogError("нет в наличии");   // используем default-метод через интерфейс
        output.Line("Записано в лог", logger.Lines[0]); // ERROR: нет в наличии

        // explicit implementation: два интерфейса с методом Greet()
        var b = new Bilingual();
        output.Line("как IEnglish", ((IEnglish)b).Greet()); // Hello
        output.Line("как IFrench", ((IFrench)b).Greet());   // Bonjour
        """;

    protected override void Demo(DemoResult output)
    {
        var logger = new ListLogger();
        ((ILogger)logger).LogError("нет в наличии");
        output.Line("Записано в лог", logger.Lines[0]);

        var b = new Bilingual();
        output.Line("как IEnglish", ((IEnglish)b).Greet());
        output.Line("как IFrench", ((IFrench)b).Greet());
    }

    public override Quiz Quiz => new(
        "Зачем нужны default-методы в интерфейсе?",
        new[]
        {
            "Чтобы запретить реализацию интерфейса",
            "Чтобы добавить метод с готовым телом, не ломая существующие реализации интерфейса",
            "Чтобы хранить состояние в интерфейсе",
            "Чтобы сделать интерфейс абстрактным классом"
        },
        1,
        "Default-метод даёт реализацию по умолчанию — можно расширять интерфейс, не трогая уже написанные классы.");
}

file interface ILogger
{
    void Log(string message);
    void LogError(string message) => Log("ERROR: " + message);
}

file sealed class ListLogger : ILogger
{
    public List<string> Lines { get; } = new();
    public void Log(string message) => Lines.Add(message);
}

file interface IEnglish
{
    string Greet();
}

file interface IFrench
{
    string Greet();
}

file sealed class Bilingual : IEnglish, IFrench
{
    string IEnglish.Greet() => "Hello";
    string IFrench.Greet() => "Bonjour";
}
