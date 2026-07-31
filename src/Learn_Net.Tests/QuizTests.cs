using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Tests;

/// <summary>Тесты квизов: валидность каждого и корректность проверки ответа.</summary>
public class QuizTests
{
    [Theory]
    [MemberData(nameof(TestData.AllLessonIds), MemberType = typeof(TestData))]
    public void Quiz_IsValid(string id)
    {
        var quiz = TestData.Registry.Find(id)!.Quiz;

        Assert.False(string.IsNullOrWhiteSpace(quiz.Question));
        Assert.False(string.IsNullOrWhiteSpace(quiz.Explanation));
        Assert.True(quiz.Options.Count >= 2);
        Assert.InRange(quiz.CorrectIndex, 0, quiz.Options.Count - 1);
        Assert.All(quiz.Options, o => Assert.False(string.IsNullOrWhiteSpace(o)));
    }

    [Theory]
    [MemberData(nameof(TestData.AllLessonIds), MemberType = typeof(TestData))]
    public void Quiz_AcceptsCorrectAnswer_RejectsWrong(string id)
    {
        var quiz = TestData.Registry.Find(id)!.Quiz;

        Assert.True(quiz.IsCorrect(quiz.CorrectIndex));

        var wrong = (quiz.CorrectIndex + 1) % quiz.Options.Count;
        Assert.False(quiz.IsCorrect(wrong));
    }

    [Fact]
    public void Quiz_Throws_WhenCorrectIndexOutOfRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Quiz("Вопрос?", new[] { "a", "b" }, correctIndex: 5, "пояснение"));
    }

    [Fact]
    public void Quiz_Throws_WhenTooFewOptions()
    {
        Assert.Throws<ArgumentException>(() =>
            new Quiz("Вопрос?", new[] { "единственный" }, correctIndex: 0, "пояснение"));
    }
}
