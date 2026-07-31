using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 6: value- vs reference-типы.</summary>
public sealed class ValueVsReferenceLesson : LessonBase
{
    public override string Id => "0.06-value-vs-reference";
    public override int Module => 0;
    public override string Title => "Value vs reference типы";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Копируется само значение или ссылка на объект — и почему это важно.";

    public override string Explanation =>
        """
        Что это. Все типы в .NET делятся на два лагеря. Value-типы (int, double, bool,
        char, а также struct и enum) хранят само значение. Reference-типы (class, string,
        массивы, коллекции, делегаты) хранят ссылку — адрес объекта в куче (heap).

        Ключевое различие. Оно проявляется при присваивании и передаче в метод: value-тип
        копируется целиком (у каждой переменной свой независимый экземпляр), а reference-тип
        копирует только ссылку — обе переменные указывают на ОДИН объект, поэтому изменение
        через одну видно через другую.

        Зачем знать. Это объясняет кучу «неожиданного» поведения: почему правка списка,
        переданного в метод, видна снаружи; почему сравнение двух объектов по умолчанию
        сравнивает ссылки, а не содержимое. На собеседовании спрашивают почти всегда.

        Смежный нюанс. Boxing: когда value-тип кладут в переменную типа object, он
        «упаковывается» в объект на куче — это отдельная тема и своя цена по перформансу.
        """;

    protected override void Demo(DemoResult output)
    {
        // value-тип: копия независима.
        int a = 5;
        int b = a;
        b += 10;
        output.Line("value  a", a);
        output.Line("value  b", b);

        // reference-тип: обе переменные указывают на одну корзину.
        var cart = new List<Product> { ShopData.Catalog[0] };
        var sameCart = cart;
        sameCart.Add(ShopData.Catalog[1]);

        output.Line();
        output.Line("reference  cart.Count", cart.Count);
        output.Line("reference  одна и та же корзина?", ReferenceEquals(cart, sameCart));
    }

    public override Quiz Quiz => new(
        "Присвоили один List другому и добавили элемент. Что видит первая переменная?",
        new[]
        {
            "Ничего — изменилась только копия",
            "Изменение — обе переменные ссылаются на один объект",
            "Исключение InvalidOperationException",
            "List автоматически клонируется"
        },
        1,
        "List — reference-тип: копируется ссылка, а не сам объект.");
}
