namespace LearnNet.Core.Lessons.Abstractions;

/// <summary>
/// Собирает вывод демо-запуска урока в виде строк — чтобы одинаково показывать
/// его и в консоли (Playground), и позже в Blazor UI.
/// </summary>
public sealed class DemoResult
{
    private readonly List<string> _lines = new();

    public IReadOnlyList<string> Lines => _lines;

    public DemoResult Line(string text = "")
    {
        _lines.Add(text);
        return this;
    }

    public DemoResult Line(string label, object? value)
    {
        _lines.Add($"{label}: {value}");
        return this;
    }

    public override string ToString() => string.Join(Environment.NewLine, _lines);
}
