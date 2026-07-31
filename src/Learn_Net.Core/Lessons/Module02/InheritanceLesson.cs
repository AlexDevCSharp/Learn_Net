using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 30: наследование — base, sealed.</summary>
public sealed class InheritanceLesson : LessonBase
{
    public override string Id => "2.30-inheritance";
    public override int Module => 2;
    public override string Title => "Наследование: base и sealed";
    public override Level Level => Level.Beginner;
    public override string Category => "ООП";

    public override string Summary =>
        "Расширение класса, вызов базовой реализации через base и запрет наследования sealed.";

    public override string Explanation =>
        """
        Что это. Наследование позволяет создать класс на основе другого: наследник получает поля,
        свойства и методы базового класса и добавляет/переопределяет своё. Отношение «является»:
        DiscountedProduct ЯВЛЯЕТСЯ Product.

        base. Из наследника обращаются к базовому классу через base: base(...) в конструкторе
        вызывает конструктор родителя, а base.Method() — его реализацию метода (полезно, когда
        переопределение хочет дополнить, а не заменить поведение).

        Единичное наследование. Класс в C# может наследоваться только от ОДНОГО класса
        (множественного наследования классов нет — есть только у интерфейсов). Все классы в
        конечном счёте наследуются от object.

        sealed. Модификатор sealed на классе запрещает наследоваться от него дальше. Используют
        для безопасности инвариантов и небольшой оптимизации (вызовы можно девиртуализировать).
        """;

    public override string Code =>
        """
        // class Product { public string Name {get;} public decimal BasePrice {get;}
        //     ctor(name, price); public virtual decimal FinalPrice() => BasePrice; }
        //
        // sealed class DiscountedProduct : Product {          // наследник, дальше не наследуют
        //     private readonly decimal _pct;
        //     public DiscountedProduct(string n, decimal p, decimal pct) : base(n, p) => _pct = pct;
        //     public override decimal FinalPrice() => base.FinalPrice() * (1 - _pct / 100m);
        // }

        var normal = new BaseProduct("Cable", 10m);
        var sale = new DiscountedProduct("Keyboard", 80m, 25);

        output.Line("Обычный товар FinalPrice", normal.FinalPrice());
        output.Line("Со скидкой 25% (base×...)", sale.FinalPrice());
        output.Line("Наследник — это Product?", sale is BaseProduct);
        """;

    protected override void Demo(DemoResult output)
    {
        var normal = new BaseProduct("Cable", 10m);
        var sale = new DiscountedProduct("Keyboard", 80m, 25);

        output.Line("Обычный товар FinalPrice", normal.FinalPrice());
        output.Line("Со скидкой 25% (base×...)", sale.FinalPrice());
        output.Line("Наследник — это Product?", sale is BaseProduct);
    }

    public override Quiz Quiz => new(
        "Для чего в наследнике используют base(...) в конструкторе?",
        new[]
        {
            "Чтобы создать новый объект того же класса",
            "Чтобы вызвать конструктор базового класса и передать ему аргументы",
            "Чтобы запретить наследование",
            "Чтобы объявить статическое поле"
        },
        1,
        "base(...) вызывает конструктор родителя; base.Method() — его реализацию метода.");
}

file class BaseProduct
{
    public string Name { get; }
    public decimal BasePrice { get; }

    public BaseProduct(string name, decimal basePrice)
    {
        Name = name;
        BasePrice = basePrice;
    }

    public virtual decimal FinalPrice() => BasePrice;
}

file sealed class DiscountedProduct : BaseProduct
{
    private readonly decimal _pct;

    public DiscountedProduct(string name, decimal basePrice, decimal pct)
        : base(name, basePrice) => _pct = pct;

    public override decimal FinalPrice() => base.FinalPrice() * (1 - _pct / 100m);
}
