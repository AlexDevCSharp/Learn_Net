using System.Reflection;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Registry;

/// <summary>
/// Находит все реализации <see cref="ILesson"/> через рефлексию, создаёт экземпляры
/// и упорядочивает по модулю и Id. Добавил новый класс-урок — он подхватился сам.
/// </summary>
public sealed class LessonRegistry
{
    public IReadOnlyList<ILesson> Lessons { get; }

    public LessonRegistry(params Assembly[] assemblies)
    {
        var sources = assemblies.Length > 0
            ? assemblies
            : new[] { typeof(LessonRegistry).Assembly };

        Lessons = sources
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(ILesson).IsAssignableFrom(t)
                        && t is { IsAbstract: false, IsInterface: false })
            .Select(t => (ILesson)Activator.CreateInstance(t)!)
            .OrderBy(l => l.Module)
            .ThenBy(l => l.Id, StringComparer.Ordinal)
            .ToList();
    }

    /// <summary>Реестр по сборке Core (где живут уроки).</summary>
    public static LessonRegistry Default() => new();

    public IEnumerable<IGrouping<int, ILesson>> ByModule() =>
        Lessons.GroupBy(l => l.Module).OrderBy(g => g.Key);

    public ILesson? Find(string id) =>
        Lessons.FirstOrDefault(l => l.Id == id);
}
