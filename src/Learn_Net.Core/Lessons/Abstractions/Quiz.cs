namespace LearnNet.Core.Lessons.Abstractions;

/// <summary>Мини-тест на тему урока: вопрос, варианты, правильный индекс и пояснение.</summary>
public sealed class Quiz
{
    public string Question { get; }
    public IReadOnlyList<string> Options { get; }
    public int CorrectIndex { get; }
    public string Explanation { get; }

    public Quiz(string question, string[] options, int correctIndex, string explanation)
    {
        if (options is null || options.Length < 2)
            throw new ArgumentException("Нужно минимум два варианта ответа.", nameof(options));
        if (correctIndex < 0 || correctIndex >= options.Length)
            throw new ArgumentOutOfRangeException(nameof(correctIndex));

        Question = question;
        Options = options;
        CorrectIndex = correctIndex;
        Explanation = explanation;
    }

    public bool IsCorrect(int answer) => answer == CorrectIndex;

    public string CorrectOption => Options[CorrectIndex];
}
