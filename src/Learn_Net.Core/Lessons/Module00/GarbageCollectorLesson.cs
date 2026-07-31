using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 4: сборщик мусора (GC) — поколения и управление памятью.</summary>
public sealed class GarbageCollectorLesson : LessonBase
{
    public override string Id => "0.04-garbage-collector";
    public override int Module => 0;
    public override string Title => "Garbage Collector: память и поколения";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Как .NET сам освобождает память и что такое поколения 0/1/2.";

    public override string Explanation =>
        """
        Что это. GC (Garbage Collector) — автоматический сборщик мусора. Объекты создаются в
        управляемой куче (managed heap), а GC периодически находит те, до которых больше нет
        ссылок, и освобождает их память. Вручную delete звать не нужно.

        Поколения. Для скорости куча делится на поколения: 0 — только что созданные, короткоживущие
        объекты; 1 — пережившие одну сборку; 2 — долгожители. GC чаще всего собирает gen 0 (это
        быстро), реже — старшие. Идея: большинство объектов умирают молодыми. Исключение — крупные
        объекты (>85 КБ): они попадают в отдельную Large Object Heap и учитываются как поколение 2.

        Недетерминированность. Момент сборки выбирает сам GC — нельзя точно предсказать, когда
        объект будет освобождён. Поэтому для внешних ресурсов (файлы, сокеты, соединения) GC не
        годится: их освобождают детерминированно через IDisposable/using (отдельная тема).

        Подвох. GC управляет ТОЛЬКО памятью управляемых объектов. Финализаторы дороги и
        недетерминированы — полагаться на них для освобождения ресурсов не стоит.
        """;

    public override string Code =>
        """
        long before = GC.GetTotalMemory(forceFullCollection: false);

        // маленький короткоживущий объект — поколение 0
        var small = new byte[100];
        output.Line("Поколение маленького объекта", GC.GetGeneration(small)); // 0

        // большой объект (>85 КБ) идёт в Large Object Heap и числится как поколение 2
        var big = new byte[10_000_000];
        output.Line("Поколение большого объекта (LOH)", GC.GetGeneration(big)); // 2

        long after = GC.GetTotalMemory(forceFullCollection: false);
        output.Line("Память до (байт)", before);
        output.Line("Память после аллокации ~10 МБ", after);
        output.Line("Сборок поколения 0", GC.CollectionCount(0));

        GC.KeepAlive(small);
        GC.KeepAlive(big); // не дать оптимизатору выкинуть объекты раньше времени
        """;

    protected override void Demo(DemoResult output)
    {
        long before = GC.GetTotalMemory(forceFullCollection: false);

        var small = new byte[100];
        output.Line("Поколение маленького объекта", GC.GetGeneration(small));

        var big = new byte[10_000_000];
        output.Line("Поколение большого объекта (LOH)", GC.GetGeneration(big));

        long after = GC.GetTotalMemory(forceFullCollection: false);
        output.Line("Память до (байт)", before);
        output.Line("Память после аллокации ~10 МБ", after);
        output.Line("Сборок поколения 0", GC.CollectionCount(0));

        GC.KeepAlive(small);
        GC.KeepAlive(big);
    }

    public override Quiz Quiz => new(
        "В какое поколение GC попадают только что созданные короткоживущие объекты?",
        new[]
        {
            "В поколение 2",
            "В поколение 0",
            "Сразу в Large Object Heap",
            "Ни в какое — они не отслеживаются"
        },
        1,
        "Новые объекты создаются в поколении 0; GC чаще всего собирает именно его.");
}
