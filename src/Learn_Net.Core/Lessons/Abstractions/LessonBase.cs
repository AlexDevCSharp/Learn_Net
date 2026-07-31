namespace LearnNet.Core.Lessons.Abstractions;

/// <summary>
/// Базовый класс, снимающий шаблонный код: наследнику достаточно описать
/// метаданные, реализовать <see cref="Demo"/> и вернуть <see cref="Quiz"/>.
/// </summary>
public abstract class LessonBase : ILesson
{
    public abstract string Id { get; }
    public abstract int Module { get; }
    public abstract string Title { get; }
    public abstract Level Level { get; }
    public abstract string Category { get; }
    public abstract string Summary { get; }
    public abstract string Explanation { get; }
    public abstract string Code { get; }
    public abstract Quiz Quiz { get; }

    public DemoResult RunDemo()
    {
        var output = new DemoResult();
        Demo(output);
        return output;
    }

    /// <summary>Тело демо: пишем строки в <paramref name="output"/>.</summary>
    protected abstract void Demo(DemoResult output);
}
