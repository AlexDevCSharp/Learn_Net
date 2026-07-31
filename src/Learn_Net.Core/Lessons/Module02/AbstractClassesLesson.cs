using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 32: абстрактные классы.</summary>
public sealed class AbstractClassesLesson : LessonBase
{
    public override string Id => "2.32-abstract-classes";
    public override int Module => 2;
    public override string Title => "Абстрактные классы";
    public override Level Level => Level.Beginner;
    public override string Category => "ООП";

    public override string Summary =>
        "Незавершённый базовый класс: часть реализована, часть обязателен переопределить.";

    public override string Explanation =>
        """
        Что это. Абстрактный класс (abstract) — базовый класс, который нельзя инстанцировать
        напрямую (new AbstractType() запрещён). Он задаёт общий каркас, а конкретику оставляет
        наследникам.

        Абстрактные члены. abstract-метод/свойство объявлены без тела — наследник ОБЯЗАН их
        реализовать через override. При этом абстрактный класс может содержать и обычные
        (реализованные) методы, поля, конструкторы — общий код для всех наследников.

        Зачем. Когда есть общее поведение + обязательные «дырки», которые каждый наследник
        заполняет по-своему. Пример: PaymentMethod знает, как сформировать чек (общий метод),
        но КАК именно платить — абстрактно, каждый способ оплаты реализует Pay сам.

        Отличие от интерфейса. Абстрактный класс может хранить состояние (поля) и давать готовую
        реализацию; наследоваться от него можно только от одного (см. тему «класс vs интерфейс»).
        """;

    public override string Code =>
        """
        // abstract class PaymentMethod {
        //     public abstract string Pay(decimal amount);            // без тела — обязателен override
        //     public string Receipt(decimal amount) =>               // общий готовый метод
        //         $"Чек: {Pay(amount)}";
        // }
        // class CardPayment : PaymentMethod {
        //     public override string Pay(decimal a) => $"оплачено картой {a:0.00}";
        // }

        PaymentMethod payment = new CardPayment();   // ссылка абстрактного типа
        output.Line("Pay", payment.Pay(50m));
        output.Line("Receipt (общий метод базы)", payment.Receipt(50m));
        // var x = new PaymentMethod();  // ← ошибка: абстрактный класс не создать через new
        """;

    protected override void Demo(DemoResult output)
    {
        PaymentMethod payment = new CardPayment();
        output.Line("Pay", payment.Pay(50m));
        output.Line("Receipt (общий метод базы)", payment.Receipt(50m));
    }

    public override Quiz Quiz => new(
        "Что нельзя сделать с абстрактным классом?",
        new[]
        {
            "Дать ему обычные реализованные методы",
            "Создать его экземпляр напрямую через new",
            "Объявить абстрактные методы без тела",
            "Хранить в нём поля"
        },
        1,
        "Абстрактный класс нельзя инстанцировать напрямую — только через наследника, реализующего абстрактные члены.");
}

file abstract class PaymentMethod
{
    public abstract string Pay(decimal amount);

    public string Receipt(decimal amount) => $"Чек: {Pay(amount)}";
}

file sealed class CardPayment : PaymentMethod
{
    public override string Pay(decimal amount) => $"оплачено картой {amount:0.00}";
}
