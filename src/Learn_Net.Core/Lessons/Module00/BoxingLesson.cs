using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 7: boxing и unboxing.</summary>
public sealed class BoxingLesson : LessonBase
{
    public override string Id => "0.07-boxing";
    public override int Module => 0;
    public override string Title => "Boxing и unboxing";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Как value-тип попадает в кучу через object и почему это стоит денег.";

    public override string Explanation =>
        """
        Что это. Boxing — «упаковка» value-типа (int, struct, enum) в объект на куче,
        когда его присваивают переменной ссылочного типа (object или интерфейс). Unboxing —
        обратная распаковка с явным приведением к исходному типу.

        Как работает. int живёт в стеке/inline. Как только пишем object o = 42, среда
        выделяет объект в куче, копирует туда значение и возвращает ссылку. Чтобы достать
        значение обратно — (int)o, и тип должен совпасть точно, иначе InvalidCastException.

        Зачем знать. Boxing незаметен, но не бесплатен: это аллокация в куче и нагрузка на
        GC. В горячем цикле или при работе со старыми не-обобщёнными API (ArrayList) это бьёт
        по перформансу. Дженерики (List<int>) как раз позволяют избежать boxing.

        Подвох. Два раза упакованный один и тот же int — это ДВА разных объекта на куче,
        поэтому ReferenceEquals для них вернёт false, хотя значения равны.
        """;

    public override string Code =>
        """
        int value = 42;

        object boxed = value;      // boxing: значение скопировано в объект на куче
        int unboxed = (int)boxed;  // unboxing: обратно в int

        output.Line("Исходный int", value);
        output.Line("Распакованный int", unboxed);
        output.Line("Значения равны?", value == unboxed);

        object boxedA = value;
        object boxedB = value;
        // два раза упаковали один int — это ДВА разных объекта на куче
        output.Line("Тот же объект?", ReferenceEquals(boxedA, boxedB)); // False
        """;

    protected override void Demo(DemoResult output)
    {
        int value = 42;

        object boxed = value;      // boxing: значение скопировано в объект на куче
        int unboxed = (int)boxed;  // unboxing: обратно в int

        output.Line("Исходный int", value);
        output.Line("Распакованный int", unboxed);
        output.Line("Значения равны?", value == unboxed);

        object boxedA = value;
        object boxedB = value;
        output.Line();
        output.Line("Два раза упаковали один int — тот же объект?", ReferenceEquals(boxedA, boxedB));
    }

    public override Quiz Quiz => new(
        "Что произойдёт при object o = 5; — сколько объектов в куче создастся?",
        new[]
        {
            "Ноль — int всегда в стеке",
            "Один — произойдёт boxing, значение копируется в объект на куче",
            "Два — под int и под object",
            "Ошибка компиляции"
        },
        1,
        "Присваивание value-типа переменной object вызывает boxing — одна аллокация на куче.");
}
