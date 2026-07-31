using System.Runtime.InteropServices;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 2: .NET Framework vs Core vs .NET 8+.</summary>
public sealed class DotNetVersionsLesson : LessonBase
{
    public override string Id => "0.02-dotnet-versions";
    public override int Module => 0;
    public override string Title => ".NET Framework vs Core vs .NET 8+";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Старый Framework, кроссплатформенный Core и единый современный .NET.";

    public override string Explanation =>
        """
        .NET Framework (1.0–4.8). Первый .NET: только Windows, закрытый, ставится в систему.
        Живёт в легаси-проектах, новых версий не будет (4.8 — последняя).

        .NET Core (1.0–3.1). Переписанный с нуля: кроссплатформенный (Windows/Linux/macOS),
        open source, модульный, быстрый, ставится рядом с приложением. Именно он стал основой
        будущего.

        Единый .NET (5, 6, 7, 8, 9, 10…). После Core 3.1 «Core» убрали из названия и
        объединили линейки: теперь просто «.NET». Это прямое продолжение Core. Версии с чётным
        номером (6, 8, 10) — LTS (долгая поддержка). Сегодня новые проекты пишут на .NET 8+.

        Как выбирать. Новый проект — последний .NET (8/10). .NET Framework — только если
        поддерживаешь старую систему. .NET Standard — устаревшая абстракция для общих
        библиотек, сейчас чаще просто таргетят net8.0.
        """;

    public override string Code =>
        """
        // На какой линейке .NET мы выполняемся прямо сейчас.
        var desc = RuntimeInformation.FrameworkDescription;
        bool isModern = desc.StartsWith(".NET") && !desc.Contains("Framework");

        output.Line("Текущий рантайм", desc);
        output.Line("Современный .NET (Core-линейка)?", isModern);
        output.Line("64-битный процесс?", Environment.Is64BitProcess);
        """;

    protected override void Demo(DemoResult output)
    {
        var desc = RuntimeInformation.FrameworkDescription;
        bool isModern = desc.StartsWith(".NET") && !desc.Contains("Framework");

        output.Line("Текущий рантайм", desc);
        output.Line("Современный .NET (Core-линейка)?", isModern);
        output.Line("64-битный процесс?", Environment.Is64BitProcess);
    }

    public override Quiz Quiz => new(
        "Главное отличие .NET Framework от современного .NET (Core-линейки)?",
        new[]
        {
            "Framework быстрее",
            "Современный .NET кроссплатформенный и open source, а Framework — только Windows",
            "Они полностью идентичны",
            "Framework поддерживает C#, а .NET — нет"
        },
        1,
        "Framework — Windows-only и легаси; современный .NET кроссплатформенный, открытый и активно развивается.");
}
