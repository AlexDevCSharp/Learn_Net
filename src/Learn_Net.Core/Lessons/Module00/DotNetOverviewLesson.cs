using System.Runtime.InteropServices;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 1: что такое .NET (CLR, BCL, runtime vs SDK).</summary>
public sealed class DotNetOverviewLesson : LessonBase
{
    public override string Id => "0.01-what-is-dotnet";
    public override int Module => 0;
    public override string Title => "Что такое .NET";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "CLR, BCL и разница между runtime и SDK — из чего состоит платформа.";

    public override string Explanation =>
        """
        Что это. .NET — платформа для запуска приложений на C# (а также F#, VB). Ключевые
        части: CLR (Common Language Runtime) — виртуальная машина, которая исполняет код,
        управляет памятью (GC) и типами; BCL (Base Class Library) — огромная стандартная
        библиотека (строки, коллекции, файлы, сеть и т.д.).

        Как это работает. Компилятор превращает C# не в машинный код напрямую, а в
        промежуточный язык IL. CLR при запуске переводит IL в машинный код (JIT) и исполняет.
        Благодаря этому один и тот же код работает на Windows, Linux и macOS.

        Runtime vs SDK. Runtime — то, что нужно, чтобы ЗАПУСКАТЬ готовые .NET-приложения.
        SDK — набор для РАЗРАБОТКИ (компилятор, CLI dotnet, шаблоны) плюс runtime внутри.
        На машине разработчика ставят SDK; на сервере для запуска достаточно runtime.

        Зачем знать. Это фундамент: понимание CLR/IL/GC объясняет, почему C# управляемый,
        кроссплатформенный и как он вообще исполняется.
        """;

    public override string Code =>
        """
        // Интроспекция рантайма, на котором мы сейчас выполняемся.
        output.Line(".NET версия", Environment.Version);
        output.Line("Описание фреймворка", RuntimeInformation.FrameworkDescription);
        output.Line("ОС", RuntimeInformation.OSDescription);
        output.Line("Архитектура процесса", RuntimeInformation.ProcessArchitecture);
        """;

    protected override void Demo(DemoResult output)
    {
        output.Line(".NET версия", Environment.Version);
        output.Line("Описание фреймворка", RuntimeInformation.FrameworkDescription);
        output.Line("ОС", RuntimeInformation.OSDescription);
        output.Line("Архитектура процесса", RuntimeInformation.ProcessArchitecture);
    }

    public override Quiz Quiz => new(
        "Чем runtime отличается от SDK?",
        new[]
        {
            "Ничем, это синонимы",
            "Runtime нужен чтобы запускать приложения, SDK — чтобы их разрабатывать",
            "SDK нужен только на сервере",
            "Runtime содержит компилятор, SDK — нет"
        },
        1,
        "Runtime исполняет готовые приложения; SDK — инструменты разработки (и включает runtime).");
}
