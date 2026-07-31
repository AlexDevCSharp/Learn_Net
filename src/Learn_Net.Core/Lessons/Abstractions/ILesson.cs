namespace LearnNet.Core.Lessons.Abstractions;

/// <summary>
/// Контракт одного урока. Каждая тема учебного плана реализует ILesson и
/// автоматически подхватывается реестром через рефлексию.
/// </summary>
public interface ILesson
{
    /// <summary>Стабильный идентификатор, напр. "0.10-strings" (модуль.номер-slug).</summary>
    string Id { get; }

    /// <summary>Номер модуля из CURRICULUM.md (0..10).</summary>
    int Module { get; }

    string Title { get; }
    Level Level { get; }
    string Category { get; }

    /// <summary>Одна строка для списков и дерева тем — о чём урок.</summary>
    string Summary { get; }

    /// <summary>Развёрнутая теория: что это, зачем, как работает, подводные камни.</summary>
    string Explanation { get; }

    /// <summary>Код демо — тот же C#, что выполняется в RunDemo(), для показа на UI.</summary>
    string Code { get; }

    /// <summary>Живой запуск примера — возвращает собранный вывод.</summary>
    DemoResult RunDemo();

    /// <summary>Мини-тест для самопроверки.</summary>
    Quiz Quiz { get; }
}
