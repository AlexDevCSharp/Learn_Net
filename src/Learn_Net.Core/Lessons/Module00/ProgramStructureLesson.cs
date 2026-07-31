using System.Reflection;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 5: структура программы — Main, top-level statements, namespace, using.</summary>
public sealed class ProgramStructureLesson : LessonBase
{
    public override string Id => "0.05-program-structure";
    public override int Module => 0;
    public override string Title => "Структура программы";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Точка входа Main, top-level statements, namespace, using и сборка.";

    public override string Explanation =>
        """
        Точка входа. Выполнение приложения начинается с метода Main — статического метода,
        который CLR вызывает первым. Классически это static void Main(string[] args).

        Top-level statements. С C# 9 в файле Program.cs можно писать код прямо на верхнем
        уровне, без явного класса и Main — компилятор сам оборачивает его в Main за кулисами.
        Наш Playground использует именно этот стиль.

        Namespace. Пространство имён группирует типы и предотвращает конфликты имён
        (LearnNet.Core.Domain.Category ≠ System.Category). Подключается через using, чтобы не
        писать полные имена. using бывает обычный, статический и с алиасом.

        Сборка (assembly). Скомпилированный проект — это сборка (.dll/.exe): контейнер с IL,
        метаданными и ресурсами. Именно сборку загружает и исполняет CLR.
        """;

    public override string Code =>
        """
        Assembly asm = typeof(ProgramStructureLesson).Assembly;

        output.Line("Сборка (assembly)", asm.GetName().Name);
        output.Line("Namespace этого урока", typeof(ProgramStructureLesson).Namespace);
        output.Line("Аргументов командной строки", Environment.GetCommandLineArgs().Length);
        output.Line("Точка входа сборки", asm.EntryPoint?.Name ?? "(нет — это библиотека)");
        """;

    protected override void Demo(DemoResult output)
    {
        Assembly asm = typeof(ProgramStructureLesson).Assembly;

        output.Line("Сборка (assembly)", asm.GetName().Name);
        output.Line("Namespace этого урока", typeof(ProgramStructureLesson).Namespace);
        output.Line("Аргументов командной строки", Environment.GetCommandLineArgs().Length);
        output.Line("Точка входа сборки", asm.EntryPoint?.Name ?? "(нет — это библиотека)");
    }

    public override Quiz Quiz => new(
        "Что такое top-level statements в C#?",
        new[]
        {
            "Специальный namespace",
            "Возможность писать код без явного класса и Main — компилятор оборачивает его сам",
            "Атрибут для точки входа",
            "Способ объявить глобальные переменные во всех файлах"
        },
        1,
        "Top-level statements позволяют опустить class и Main в Program.cs — компилятор генерирует их автоматически.");
}
