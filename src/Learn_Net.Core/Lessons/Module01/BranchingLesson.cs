using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module01;

/// <summary>Тема 16: ветвления — if/else и тернарный оператор.</summary>
public sealed class BranchingLesson : LessonBase
{
    public override string Id => "1.16-branching";
    public override int Module => 1;
    public override string Title => "Ветвления: if/else и тернарный";
    public override Level Level => Level.Beginner;
    public override string Category => "Управление потоком";

    public override string Summary =>
        "Выбор ветки по условию и компактный тернарный оператор ?: для значений.";

    public override string Explanation =>
        """
        if/else. Базовая развилка: выполняем блок, если условие истинно, иначе — другой блок.
        Цепочка else if проверяет условия по порядку и заходит в первую истинную ветку;
        финальный else — «во всех остальных случаях».

        Условие — это bool. В C# нельзя, как в C, писать if (x) для числа: нужно явное
        сравнение (if (x != 0)). Это защищает от классических ошибок.

        Тернарный оператор ?:. Компактная форма, которая ВОЗВРАЩАЕТ значение:
        условие ? еслиИстина : еслиЛожь. Удобен для присваивания и подстановки: не «сделать
        что-то», а «выбрать одно из двух значений». Обе ветки должны быть совместимого типа.

        Когда что. if/else — когда в ветках выполняются действия. Тернарный — когда нужно
        выбрать значение. Не вкладывайте тернарники глубоко: читаемость важнее краткости.
        """;

    public override string Code =>
        """
        var product = ShopData.Catalog[1]; // Mechanical Keyboard, 79.90

        // тернарный: выбираем значение
        string availability = product.InStock ? "в наличии" : "нет в наличии";

        // if / else if / else: ценовой сегмент
        string tier;
        if (product.Price >= 50) tier = "премиум";
        else if (product.Price >= 20) tier = "средний";
        else tier = "бюджет";

        output.Line("Товар", product.Name);
        output.Line("Наличие (тернарный)", availability);
        output.Line("Ценовой сегмент (if/else)", tier);
        """;

    protected override void Demo(DemoResult output)
    {
        var product = ShopData.Catalog[1];

        string availability = product.InStock ? "в наличии" : "нет в наличии";

        string tier;
        if (product.Price >= 50) tier = "премиум";
        else if (product.Price >= 20) tier = "средний";
        else tier = "бюджет";

        output.Line("Товар", product.Name);
        output.Line("Наличие (тернарный)", availability);
        output.Line("Ценовой сегмент (if/else)", tier);
    }

    public override Quiz Quiz => new(
        "Что делает тернарный оператор условие ? a : b?",
        new[]
        {
            "Выполняет два действия подряд",
            "Возвращает a, если условие истинно, иначе b",
            "Сравнивает a и b",
            "Это цикл по a и b"
        },
        1,
        "Тернарный оператор — выражение: возвращает первое значение при true, второе при false.");
}
