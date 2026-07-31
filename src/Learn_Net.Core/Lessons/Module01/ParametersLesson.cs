using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module01;

/// <summary>Тема 22: параметры — по значению vs ref/out/in.</summary>
public sealed class ParametersLesson : LessonBase
{
    public override string Id => "1.22-parameters";
    public override int Module => 1;
    public override string Title => "Параметры: value, ref, out, in";
    public override Level Level => Level.Beginner;
    public override string Category => "Управление потоком";

    public override string Summary =>
        "Как передаются аргументы: копия значения, ссылка (ref), результат (out), read-only (in).";

    public override string Explanation =>
        """
        По значению (по умолчанию). В метод передаётся КОПИЯ аргумента. Изменение параметра
        внутри метода не влияет на переменную снаружи. (Для reference-типов копируется ссылка —
        сам объект общий, но переприсвоить внешнюю переменную нельзя.)

        ref. Передаёт переменную по ссылке: метод работает с тем же местом в памяти и может
        изменить внешнюю переменную. Аргумент должен быть инициализирован до вызова и помечен
        ref и при вызове.

        out. Как ref, но для ВОЗВРАТА значений: переменную можно не инициализировать заранее,
        зато метод ОБЯЗАН присвоить ей значение. Классика — Type.TryParse(str, out var result).
        Позволяет вернуть из метода несколько результатов.

        in. Передаёт по ссылке, но только для ЧТЕНИЯ (менять параметр нельзя). Полезно для
        больших struct — избегаем копирования, но гарантируем неизменность.
        """;

    public override string Code =>
        """
        int x = 5;

        Increment(x);        // по значению — копия, снаружи не изменится
        output.Line("после Increment(x) [value]", x);   // 5

        IncrementRef(ref x); // по ссылке — изменит внешнюю переменную
        output.Line("после IncrementRef(ref x)", x);    // 6

        // out: метод возвращает результат через параметр
        if (int.TryParse("42", out int parsed))
            output.Line("out: TryParse дал", parsed);    // 42

        static void Increment(int n) => n++;             // меняет только копию
        static void IncrementRef(ref int n) => n++;      // меняет оригинал
        """;

    protected override void Demo(DemoResult output)
    {
        int x = 5;

        Increment(x);
        output.Line("после Increment(x) [value]", x);

        IncrementRef(ref x);
        output.Line("после IncrementRef(ref x)", x);

        if (int.TryParse("42", out int parsed))
            output.Line("out: TryParse дал", parsed);
    }

    private static void Increment(int n) => n++;
    private static void IncrementRef(ref int n) => n++;

    public override Quiz Quiz => new(
        "Для чего нужен модификатор out?",
        new[]
        {
            "Чтобы передать копию значения",
            "Чтобы вернуть значение через параметр — метод обязан его присвоить",
            "Чтобы запретить изменение параметра",
            "Чтобы сделать параметр необязательным"
        },
        1,
        "out передаёт по ссылке для возврата: переменную можно не инициализировать, но метод обязан её задать.");
}
