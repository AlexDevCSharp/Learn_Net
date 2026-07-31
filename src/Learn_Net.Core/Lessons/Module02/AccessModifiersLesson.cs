using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module02;

/// <summary>Тема 29: модификаторы доступа и инкапсуляция.</summary>
public sealed class AccessModifiersLesson : LessonBase
{
    public override string Id => "2.29-access-modifiers";
    public override int Module => 2;
    public override string Title => "Модификаторы доступа";
    public override Level Level => Level.Beginner;
    public override string Category => "ООП";

    public override string Summary =>
        "public/private/protected/internal и зачем прятать состояние за инкапсуляцией.";

    public override string Explanation =>
        """
        Зачем. Модификаторы доступа задают, откуда виден член типа. Это основа инкапсуляции:
        внутреннее состояние прячут, а наружу дают безопасный контролируемый интерфейс.

        Уровни.
        - public — доступен отовсюду.
        - private — только внутри самого типа (по умолчанию для членов класса).
        - protected — внутри типа и его наследников.
        - internal — в пределах той же сборки (по умолчанию для самих типов).
        - protected internal — protected ИЛИ internal (в наследниках или в своей сборке).
        - private protected — protected И internal (наследники в той же сборке).

        Пример инкапсуляции. Поле _balance — private, менять его можно только через метод
        Deposit с проверкой (нельзя внести отрицательную сумму). Снаружи видно только чтение
        через свойство Balance. Так объект сам защищает свои инварианты.

        Правило. Открывайте минимально необходимое. Поля обычно private, наружу — свойства и
        методы. Меньше публичной поверхности — проще менять реализацию, не ломая пользователей.
        """;

    public override string Code =>
        """
        // class Wallet {
        //     private decimal _balance;                 // спрятано
        //     public decimal Balance => _balance;       // только чтение наружу
        //     public bool Deposit(decimal amount) {      // контролируемое изменение
        //         if (amount <= 0) return false;
        //         _balance += amount;
        //         return true;
        //     }
        // }

        var w = new Wallet();
        output.Line("Пополнение 100 принято?", w.Deposit(100));
        output.Line("Пополнение -5 принято?", w.Deposit(-5)); // отклонено проверкой
        output.Line("Баланс", w.Balance);
        // w._balance = 999;  // ← ошибка: _balance private, напрямую не изменить
        """;

    protected override void Demo(DemoResult output)
    {
        var w = new Wallet();
        output.Line("Пополнение 100 принято?", w.Deposit(100));
        output.Line("Пополнение -5 принято?", w.Deposit(-5));
        output.Line("Баланс", w.Balance);
    }

    public override Quiz Quiz => new(
        "Какой модификатор делает член видимым только внутри самого класса?",
        new[]
        {
            "public",
            "private",
            "protected",
            "internal"
        },
        1,
        "private ограничивает доступ самим типом; это основа инкапсуляции состояния.");
}

file sealed class Wallet
{
    private decimal _balance;

    public decimal Balance => _balance;

    public bool Deposit(decimal amount)
    {
        if (amount <= 0) return false;
        _balance += amount;
        return true;
    }
}
