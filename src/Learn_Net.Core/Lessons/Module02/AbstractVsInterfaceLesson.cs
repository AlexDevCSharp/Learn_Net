using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 35: абстрактный класс vs интерфейс.</summary>
public sealed class AbstractVsInterfaceLesson : LessonBase
{
    public override string Id => "2.35-abstract-vs-interface";
    public override int Module => 2;
    public override string Title => "Абстрактный класс vs интерфейс";
    public override Level Level => Level.Intermediate;
    public override string Category => "ООП";

    public override string Summary =>
        "Когда брать абстрактный класс, а когда интерфейс — классический вопрос собеседования.";

    public override string Explanation =>
        """
        Главные различия.
        - Наследование: от классов — только ОДНО (один абстрактный базовый), интерфейсов можно
          реализовать СКОЛЬКО УГОДНО.
        - Состояние: абстрактный класс может иметь поля и хранить состояние; интерфейс — нет
          (только контракт, свойства без backing-поля, константы, default-методы).
        - Реализация: абстрактный класс может дать готовый код и конструкторы; интерфейс —
          в основном контракт (плюс default-методы с C# 8).

        Смысловое отличие. Абстрактный класс моделирует «что ЭТО такое» (общая сущность и
        поведение для родственных типов), интерфейс — «что объект УМЕЕТ» (способность, которую
        могут иметь совсем разные типы). Book ЯВЛЯЕТСЯ CatalogEntry и УМЕЕТ Discount.

        Как выбирать. Нужен общий код/состояние и тесная иерархия «is-a» → абстрактный класс.
        Нужна способность, которую разделяют несмежные типы, или несколько «ролей» на одном
        классе → интерфейс. Часто их комбинируют: абстрактный базовый + интерфейсы-способности.
        """;

    public override string Code =>
        """
        // abstract class CatalogEntry {          // общее состояние/поведение — "что это"
        //     public abstract string Sku();
        //     public string Label() => $"[{Sku()}]";
        // }
        // interface IDiscountable {              // способность — "что умеет"
        //     decimal Discount(decimal price);
        // }
        // class Book : CatalogEntry, IDiscountable {   // ОДИН класс + интерфейс(ы)
        //     public override string Sku() => "BOOK-1";
        //     public decimal Discount(decimal price) => price * 0.9m;
        // }

        var book = new Book();
        output.Line("Label() из абстрактного базового", book.Label());   // [BOOK-1]
        output.Line("Discount(100) из интерфейса", book.Discount(100m)); // 90
        output.Line("book — это CatalogEntry?", book is CatalogEntry);
        output.Line("book — это IDiscountable?", book is IDiscountable);
        """;

    protected override void Demo(DemoResult output)
    {
        var book = new Book();
        output.Line("Label() из абстрактного базового", book.Label());
        output.Line("Discount(100) из интерфейса", book.Discount(100m));
        output.Line("book — это CatalogEntry?", book is CatalogEntry);
        output.Line("book — это IDiscountable?", book is IDiscountable);
    }

    public override Quiz Quiz => new(
        "Ключевое различие абстрактного класса и интерфейса?",
        new[]
        {
            "Интерфейс быстрее работает",
            "От класса наследуются только от одного, интерфейсов можно реализовать много; класс может хранить состояние",
            "Абстрактный класс нельзя использовать как тип переменной",
            "Интерфейс может иметь конструкторы, а класс — нет"
        },
        1,
        "Один базовый класс vs много интерфейсов; абстрактный класс хранит состояние и общий код, интерфейс — контракт способностей.");
}

file abstract class CatalogEntry
{
    public abstract string Sku();
    public string Label() => $"[{Sku()}]";
}

file interface IDiscountable
{
    decimal Discount(decimal price);
}

file sealed class Book : CatalogEntry, IDiscountable
{
    public override string Sku() => "BOOK-1";
    public decimal Discount(decimal price) => price * 0.9m;
}
