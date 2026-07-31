using System.Reflection;
using System.Runtime.CompilerServices;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 3: компиляция — IL, JIT vs AOT.</summary>
public sealed class CompilationLesson : LessonBase
{
    public override string Id => "0.03-compilation";
    public override int Module => 0;
    public override string Title => "Компиляция: IL, JIT vs AOT";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Путь кода: C# → IL → машинный код (JIT при запуске или AOT заранее).";

    public override string Explanation =>
        """
        Два этапа. C#-компилятор (Roslyn) переводит исходник не в машинный код, а в
        промежуточный язык IL (Intermediate Language) — платформонезависимые байт-инструкции.
        IL вместе с метаданными упаковывается в сборку (.dll/.exe).

        JIT. При запуске CLR применяет JIT-компиляцию (Just-In-Time): переводит IL в машинный
        код того процессора, на котором выполняется, — по мере вызова методов. Плюс: код
        оптимизируется под конкретную машину. Минус: небольшая задержка на старте («прогрев»).

        AOT. Ahead-Of-Time компилирует всё в нативный код заранее, при сборке. Плюсы: мгновенный
        старт, меньше памяти, не нужен JIT. Минусы: платформенно-специфичный бинарник и ограничения
        на динамику (рефлексию, генерацию кода на лету).

        Зачем знать. Это объясняет, почему .NET кроссплатформенный (IL один, JIT разный) и
        почему рефлексия/динамика «дружат» с JIT, но требуют осторожности в AOT.
        """;

    public override string Code =>
        """
        // Под JIT динамический код компилируется в рантайме; под AOT — нет.
        output.Line("Динамический код (JIT) компилируется?", RuntimeFeature.IsDynamicCodeCompiled);
        output.Line("Динамический код поддерживается?", RuntimeFeature.IsDynamicCodeSupported);

        // Метод реально хранится как IL — достанем его байты через рефлексию.
        MethodInfo add = typeof(CompilationLesson).GetMethod(nameof(Add),
            BindingFlags.NonPublic | BindingFlags.Static)!;
        byte[] il = add.GetMethodBody()!.GetILAsByteArray()!;
        output.Line("Размер IL метода Add (байт)", il.Length);
        output.Line("Add(2, 3) после JIT", Add(2, 3));
        """;

    protected override void Demo(DemoResult output)
    {
        output.Line("Динамический код (JIT) компилируется?", RuntimeFeature.IsDynamicCodeCompiled);
        output.Line("Динамический код поддерживается?", RuntimeFeature.IsDynamicCodeSupported);

        MethodInfo add = typeof(CompilationLesson).GetMethod(nameof(Add),
            BindingFlags.NonPublic | BindingFlags.Static)!;
        byte[] il = add.GetMethodBody()!.GetILAsByteArray()!;
        output.Line("Размер IL метода Add (байт)", il.Length);
        output.Line("Add(2, 3) после JIT", Add(2, 3));
    }

    private static int Add(int a, int b) => a + b;

    public override Quiz Quiz => new(
        "В какой промежуточный формат C#-компилятор переводит исходный код?",
        new[]
        {
            "Сразу в машинный код процессора",
            "В IL (Intermediate Language), который затем JIT'ится в машинный код",
            "В байт-код JVM",
            "В ассемблер x86"
        },
        1,
        "C# → IL (в сборке) → машинный код через JIT (или заранее через AOT).");
}
