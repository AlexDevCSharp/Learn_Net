using LearnNet.Core.Lessons.Registry;

namespace LearnNet.Tests;

/// <summary>Общий реестр уроков и источники данных для параметризованных тестов.</summary>
public static class TestData
{
    public static readonly LessonRegistry Registry = LessonRegistry.Default();

    /// <summary>Источник для [MemberData]: по одному Id урока на строку — читаемые имена тестов.</summary>
    public static IEnumerable<object[]> AllLessonIds =>
        Registry.Lessons.Select(l => new object[] { l.Id });
}
