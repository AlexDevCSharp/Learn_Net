using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 31: полиморфизм — virtual/override/new.</summary>
public sealed class PolymorphismLesson : LessonBase
{
    public override string Id => "2.31-polymorphism";
    public override int Module => 2;
    public override string Title => "Полиморфизм: virtual, override, new";
    public override Level Level => Level.Beginner;
    public override string Category => "ООП";

    public override string Summary =>
        "Вызов через ссылку на базовый тип уходит в реализацию реального типа (virtual/override).";

    public override string Explanation =>
        """
        Что это. Полиморфизм — способность вызывать один и тот же метод у объектов разных типов и
        получать поведение, соответствующее РЕАЛЬНОМУ типу объекта, даже если обращаемся через
        ссылку базового типа.

        virtual / override. Базовый метод помечают virtual, наследник переопределяет его через
        override. При вызове через ссылку базового типа срабатывает override наследника — это
        динамическая диспетчеризация (решается в рантайме по фактическому типу).

        new (сокрытие). Модификатор new НЕ переопределяет, а ПРЯЧЕТ метод базового класса. Тогда
        выбор реализации зависит от типа ССЫЛКИ, а не объекта: через базовую ссылку вызовется
        базовый метод. Это частый источник путаницы — обычно нужен override, а не new.

        Зачем. Полиморфизм позволяет писать код против базового типа/интерфейса и добавлять новые
        реализации, не трогая вызывающий код. Основа расширяемости и паттернов (Strategy и др.).
        """;

    public override string Code =>
        """
        // class Notification { public virtual string Format() => "общее уведомление"; }
        // class EmailNotification : Notification { public override string Format() => "email"; }
        // class SmsNotification  : Notification { public override string Format() => "sms"; }

        // ссылки базового типа, объекты — разные наследники
        Notification[] items = { new EmailNotification(), new SmsNotification(), new Notification() };

        foreach (Notification n in items)
            output.Line("Format() через базовую ссылку", n.Format()); // диспетчеризация по факт. типу
        """;

    protected override void Demo(DemoResult output)
    {
        Notification[] items = { new EmailNotification(), new SmsNotification(), new Notification() };

        foreach (Notification n in items)
            output.Line("Format() через базовую ссылку", n.Format());
    }

    public override Quiz Quiz => new(
        "Метод помечен virtual в базе и override в наследнике. Что вызовется через ссылку базового типа на объект-наследник?",
        new[]
        {
            "Всегда базовая реализация",
            "Реализация наследника (override) — диспетчеризация по реальному типу",
            "Ошибка компиляции",
            "Зависит от порядка объявления"
        },
        1,
        "virtual/override дают динамическую диспетчеризацию: выбирается реализация фактического типа объекта.");
}

file class Notification
{
    public virtual string Format() => "общее уведомление";
}

file sealed class EmailNotification : Notification
{
    public override string Format() => "email";
}

file sealed class SmsNotification : Notification
{
    public override string Format() => "sms";
}
