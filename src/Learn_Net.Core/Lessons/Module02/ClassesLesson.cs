using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

// Алиас: у ILesson есть строковое свойство Category, оно перекрывает доменный enum
// внутри этого класса, поэтому обращаемся к enum через отдельное имя.
using ProductCategory = LearnNet.Core.Domain.Category;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 25: классы и объекты.</summary>
public sealed class ClassesLesson : LessonBase
{
    public override string Id => "2.25-classes";
    public override int Module => 2;
    public override string Title => "Классы и объекты";
    public override Level Level => Level.Beginner;
    public override string Category => "ООП";

    public override string Summary =>
        "Класс как шаблон и объект как экземпляр — на примере Product.";

    public override string Explanation =>
        """
        Что это. Класс — тип, объединяющий данные (поля/свойства) и поведение (методы).
        Объект — конкретный экземпляр класса, созданный через new и живущий в памяти со
        своим собственным состоянием. Один класс — сколько угодно объектов.

        Из чего состоит. Поля хранят внутреннее состояние; свойства дают контролируемый
        доступ к данным (можно валидировать, вычислять на лету, делать read-only); методы
        описывают, что объект умеет; конструктор задаёт начальное состояние при создании.

        Зачем. Классы — основа ООП и моделирования предметной области. В нашем магазине
        Product инкапсулирует всё, что мы знаем о товаре (Id, имя, цена, остаток, категория),
        и умеет отвечать на вопросы вроде «есть ли в наличии» через свойство InStock.

        Важно помнить. Класс — reference-тип (см. урок «value vs reference»): переменная
        хранит ссылку на объект, а не сам объект. Два new — всегда два разных объекта.
        """;

    protected override void Demo(DemoResult output)
    {
        var product = new Product(99, "Gaming Mouse", 45.00m, 3, ProductCategory.Electronics);

        output.Line("Создан объект", product);
        output.Line("Вычисляемое свойство InStock", product.InStock);
        output.Line("Свойство Category (enum)", product.Category);

        var cheapest = ShopData.Catalog.MinBy(p => p.Price);
        output.Line();
        output.Line("Самый дешёвый товар каталога", cheapest);
    }

    public override Quiz Quiz => new(
        "Чем класс отличается от объекта?",
        new[]
        {
            "Это синонимы",
            "Класс — описание/шаблон, объект — конкретный экземпляр в памяти",
            "Объект — шаблон, класс — экземпляр",
            "Класс всегда статический, объект — нет"
        },
        1,
        "Класс определяет структуру и поведение; объект — конкретный экземпляр класса.");
}
