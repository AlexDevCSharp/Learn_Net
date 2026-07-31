using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Tests;

/// <summary>Параметризованные тесты: инварианты, которым обязан удовлетворять КАЖДЫЙ урок.</summary>
public class LessonContractTests
{
    private static ILesson Get(string id) =>
        TestData.Registry.Find(id) ?? throw new InvalidOperationException($"Урок {id} не найден");

    [Theory]
    [MemberData(nameof(TestData.AllLessonIds), MemberType = typeof(TestData))]
    public void Lesson_HasRequiredMetadata(string id)
    {
        var lesson = Get(id);

        Assert.False(string.IsNullOrWhiteSpace(lesson.Title));
        Assert.False(string.IsNullOrWhiteSpace(lesson.Summary));
        Assert.False(string.IsNullOrWhiteSpace(lesson.Explanation));
        Assert.False(string.IsNullOrWhiteSpace(lesson.Code));
        Assert.False(string.IsNullOrWhiteSpace(lesson.Category));
    }

    [Theory]
    [MemberData(nameof(TestData.AllLessonIds), MemberType = typeof(TestData))]
    public void Lesson_ModuleInValidRange(string id)
    {
        Assert.InRange(Get(id).Module, 0, 10);
    }

    [Theory]
    [MemberData(nameof(TestData.AllLessonIds), MemberType = typeof(TestData))]
    public void Lesson_Demo_RunsWithoutThrowing_AndProducesOutput(string id)
    {
        var result = Get(id).RunDemo();

        Assert.NotNull(result);
        Assert.NotEmpty(result.Lines);
    }
}
