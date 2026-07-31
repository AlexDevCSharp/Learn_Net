namespace LearnNet.Tests;

/// <summary>Тесты авто-реестра: что уроки грузятся, Id уникальны, порядок по модулям.</summary>
public class LessonRegistryTests
{
    [Fact]
    public void Registry_LoadsLessons()
    {
        Assert.NotEmpty(TestData.Registry.Lessons);
    }

    [Fact]
    public void Lesson_Ids_AreUnique()
    {
        var ids = TestData.Registry.Lessons.Select(l => l.Id).ToList();

        Assert.Equal(ids.Count, ids.Distinct().Count());
    }

    [Fact]
    public void Lessons_AreOrderedByModule()
    {
        var modules = TestData.Registry.Lessons.Select(l => l.Module).ToList();

        Assert.Equal(modules.OrderBy(m => m), modules);
    }

    [Fact]
    public void Find_ReturnsLesson_ForKnownId()
    {
        var first = TestData.Registry.Lessons[0];

        Assert.Same(first, TestData.Registry.Find(first.Id));
    }

    [Fact]
    public void Find_ReturnsNull_ForUnknownId()
    {
        Assert.Null(TestData.Registry.Find("нет-такого-урока"));
    }
}
