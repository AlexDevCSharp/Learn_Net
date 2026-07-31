using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module01;

/// <summary>Тема 20: методы — сигнатура, возврат, перегрузка.</summary>
public sealed class MethodsLesson : LessonBase
{
    public override string Id => "1.20-methods";
    public override int Module => 1;
    public override string Title => "Методы: сигнатура, возврат, перегрузка";
    public override Level Level => Level.Beginner;
    public override string Category => "Управление потоком";

    public override string Summary =>
        "Что входит в сигнатуру и как работает перегрузка по параметрам.";

    public override string Explanation =>
        """
        Что это. Метод — именованный блок кода с параметрами и типом возврата. Сигнатура — это
        имя + список типов параметров (и их модификаторы). Тип возврата и имена параметров в
        сигнатуру НЕ входят.

        Возврат. return завершает метод и отдаёт значение указанного типа; void означает «ничего
        не возвращает». Метод может иметь несколько return в разных ветках.

        Перегрузка (overloading). В одном типе можно объявить несколько методов с ОДНИМ именем,
        но разными списками параметров — компилятор выберет подходящий по аргументам вызова.
        Именно поэтому есть Console.WriteLine для int, string, double и т.д.

        Важный подвох. Перегрузки различаются ТОЛЬКО по параметрам. Нельзя сделать две
        перегрузки, отличающиеся лишь типом возврата — это ошибка компиляции.
        """;

    public override string Code =>
        """
        // перегрузка по количеству параметров
        int Total(int a, int b) => a + b;
        int Total(int a, int b, int c) => a + b + c;

        // перегрузка по типу параметра
        string Describe(int n) => $"число {n}";
        string Describe(string s) => $"строка \"{s}\"";

        output.Line("Total(2, 3)", Total(2, 3));
        output.Line("Total(2, 3, 5)", Total(2, 3, 5));
        output.Line("Describe(42)", Describe(42));
        output.Line("Describe(\"hi\")", Describe("hi"));
        """;

    protected override void Demo(DemoResult output)
    {
        output.Line("Total(2, 3)", Total(2, 3));
        output.Line("Total(2, 3, 5)", Total(2, 3, 5));
        output.Line("Describe(42)", Describe(42));
        output.Line("Describe(\"hi\")", Describe("hi"));
    }

    private static int Total(int a, int b) => a + b;
    private static int Total(int a, int b, int c) => a + b + c;
    private static string Describe(int n) => $"число {n}";
    private static string Describe(string s) => $"строка \"{s}\"";

    public override Quiz Quiz => new(
        "Чем должны различаться перегрузки метода?",
        new[]
        {
            "Только типом возвращаемого значения",
            "Списком параметров (количеством или типами)",
            "Именем метода",
            "Модификатором доступа"
        },
        1,
        "Перегрузки различаются списком параметров; отличие лишь по типу возврата — ошибка компиляции.");
}
