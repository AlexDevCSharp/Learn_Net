using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 10: строки — immutability и интернирование.</summary>
public sealed class StringsLesson : LessonBase
{
    public override string Id => "0.10-strings";
    public override int Module => 0;
    public override string Title => "Строки: immutability и интернирование";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Почему строку нельзя изменить и что такое интернирование литералов.";

    public override string Explanation =>
        """
        Что это. Строка (string) — ссылочный тип, но неизменяемый (immutable): любой
        «изменяющий» метод — ToUpper, Replace, Trim, конкатенация + — не трогает исходную
        строку, а возвращает НОВУЮ.

        Зачем так. Неизменяемость даёт безопасность: строку можно свободно передавать в
        методы и шарить между потоками, не боясь, что её испортят. Это же позволяет среде
        кэшировать строки.

        Интернирование. Одинаковые строковые литералы компилятор складывает в общий пул —
        и обе переменные ссылаются на ОДИН объект в памяти (ReferenceEquals вернёт true).
        А строка, собранная в рантайме, — уже отдельный объект, пока её явно не «интернируют»
        через string.Intern.

        Подвох. Сравнивать строки нужно по значению (== у string сравнивает содержимое, и это
        правильно), а не полагаться на ссылочное равенство — оно зависит от интернирования.
        """;

    public override string Code =>
        """
        var name = "keyboard";
        var upper = name.ToUpperInvariant();

        output.Line("Исходная строка не изменилась", name);   // keyboard
        output.Line("ToUpper вернул новую строку", upper);     // KEYBOARD

        // интернирование: одинаковые литералы — один объект в пуле
        string a = "shop";
        string b = "shop";
        output.Line("Литералы — тот же объект?", ReferenceEquals(a, b)); // True

        // строка, собранная в рантайме, — отдельный объект
        string runtime = new string("shop".ToCharArray());
        output.Line("Рантайм-строка — тот же объект?", ReferenceEquals(a, runtime));         // False
        output.Line("После string.Intern — тот же?", ReferenceEquals(a, string.Intern(runtime))); // True
        """;

    protected override void Demo(DemoResult output)
    {
        var name = "keyboard";
        var upper = name.ToUpperInvariant();

        output.Line("Исходная строка не изменилась", name);
        output.Line("ToUpper вернул новую строку", upper);

        string a = "shop";
        string b = "shop";
        output.Line("Литералы — тот же объект?", ReferenceEquals(a, b));

        string runtime = new string("shop".ToCharArray());
        output.Line("Рантайм-строка — тот же объект?", ReferenceEquals(a, runtime));
        output.Line("После string.Intern — тот же?", ReferenceEquals(a, string.Intern(runtime)));
    }

    public override Quiz Quiz => new(
        "Что делает string.ToUpper() с исходной строкой?",
        new[]
        {
            "Меняет её на месте",
            "Возвращает новую строку, исходная не меняется",
            "Бросает исключение для immutable-строк",
            "Меняет только первую букву"
        },
        1,
        "Строки неизменяемы — строковые методы возвращают новый экземпляр.");
}
