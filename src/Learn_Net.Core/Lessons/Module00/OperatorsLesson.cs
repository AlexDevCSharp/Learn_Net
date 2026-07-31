using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 14: операторы — арифметика, сравнение, логика.</summary>
public sealed class OperatorsLesson : LessonBase
{
    public override string Id => "0.14-operators";
    public override int Module => 0;
    public override string Title => "Операторы: арифметика, сравнение, логика";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Целочисленное деление, остаток и короткое замыкание логических операторов.";

    public override string Explanation =>
        """
        Арифметика. + - * / % работают по типам операндов. Важный подвох: если оба операнда
        целые, деление / тоже целочисленное (7 / 2 == 3, дробная часть отбрасывается). Чтобы
        получить дробь, хотя бы один операнд должен быть double/decimal (7 / 2.0 == 3.5).
        Оператор % даёт остаток от деления.

        Сравнение. == != < > <= >= возвращают bool. Для value-типов == сравнивает значения,
        для ссылочных по умолчанию — ссылки (кроме string, у которого == сравнивает содержимое).

        Логика и короткое замыкание. && и || «ленивы»: если результат уже ясен по левому
        операнду, правый НЕ вычисляется. false && f() не вызовет f(); true || f() тоже. Это
        используют для защиты: obj != null && obj.IsValid.

        Приоритет. У операторов есть приоритет (* / % выше + -; сравнение выше && выше ||).
        Когда сомневаетесь — ставьте скобки, так читается однозначно.
        """;

    public override string Code =>
        """
        output.Line("7 / 2 (целочисленное)", 7 / 2);   // 3
        output.Line("7 % 2 (остаток)", 7 % 2);          // 1
        output.Line("7 / 2.0 (double)", 7 / 2.0);       // 3.5
        output.Line("3 > 2", 3 > 2);                    // True
        output.Line("true && false", true && false);    // False

        // короткое замыкание: правый операнд не вычисляется
        bool called = false;
        bool Check() { called = true; return true; }
        _ = false && Check();
        output.Line("после (false && Check()) — Check вызывался?", called); // False
        """;

    protected override void Demo(DemoResult output)
    {
        output.Line("7 / 2 (целочисленное)", 7 / 2);
        output.Line("7 % 2 (остаток)", 7 % 2);
        output.Line("7 / 2.0 (double)", 7 / 2.0);
        output.Line("3 > 2", 3 > 2);
        output.Line("true && false", true && false);

        bool called = false;
        bool Check() { called = true; return true; }
        _ = false && Check();
        output.Line("после (false && Check()) — Check вызывался?", called);
    }

    public override Quiz Quiz => new(
        "Чему равно 7 / 2 в C#, если оба операнда типа int?",
        new[]
        {
            "3.5",
            "3 — целочисленное деление отбрасывает дробную часть",
            "4 — округление вверх",
            "Ошибка компиляции"
        },
        1,
        "Если оба операнда целые, / — целочисленное деление: 7 / 2 == 3. Для 3.5 нужен double.");
}
