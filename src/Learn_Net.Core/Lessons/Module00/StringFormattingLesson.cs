using System.Globalization;
using LearnNet.Core.Domain;
using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 12: форматирование строк и культура (culture).</summary>
public sealed class StringFormattingLesson : LessonBase
{
    public override string Id => "0.12-formatting";
    public override int Module => 0;
    public override string Title => "Форматирование строк и culture";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "Спецификаторы формата и почему :C выводит то ₴, то $ — зависит от культуры.";

    public override string Explanation =>
        """
        Что это. Форматирование превращает число/дату в строку по шаблону. Спецификаторы:
        C — валюта, N — число с разделителями, F — фиксированная точность, P — проценты,
        X — hex. Пишутся в интерполяции после двоеточия: $"{price:C}", $"{qty:N0}".

        Про culture. Ключевой момент: C, N, даты и разделители зависят от CultureInfo —
        текущей культуры потока (CultureInfo.CurrentCulture), которую задаёт ОС. Поэтому
        один и тот же :C на украинской системе печатает ₴, на американской — $, на
        немецкой — € с запятой как десятичным разделителем. Это и объясняет, почему в нашем
        консольном демо цена выводилась в ₴.

        Как управлять. Передавай культуру явно: value.ToString("C", new CultureInfo("en-US")).
        Для машинного вывода (логи, JSON, файлы), где формат не должен зависеть от машины,
        используй CultureInfo.InvariantCulture — стабильный культуронезависимый формат.

        Правило. Для пользователя — его культура; для хранения и обмена данными — Invariant.
        """;

    public override string Code =>
        """
        var price = ShopData.Catalog[1].Price; // 79.90m

        output.Line("Текущая культура ОС", CultureInfo.CurrentCulture.Name);
        output.Line("Цена :C (текущая)", price.ToString("C", CultureInfo.CurrentCulture));
        output.Line("Цена :C (en-US)", price.ToString("C", new CultureInfo("en-US")));
        output.Line("Цена :C (de-DE)", price.ToString("C", new CultureInfo("de-DE")));
        output.Line("Цена :C (Invariant)", price.ToString("C", CultureInfo.InvariantCulture));

        output.Line("Число :N2", 1234567.891.ToString("N2", CultureInfo.InvariantCulture));
        output.Line("Проценты :P0", 0.2.ToString("P0", CultureInfo.InvariantCulture));
        output.Line("Hex :X", 255.ToString("X"));
        """;

    protected override void Demo(DemoResult output)
    {
        var price = ShopData.Catalog[1].Price; // 79.90m

        output.Line("Текущая культура ОС", CultureInfo.CurrentCulture.Name);
        output.Line("Цена :C (текущая культура)", price.ToString("C", CultureInfo.CurrentCulture));
        output.Line("Цена :C (en-US)", price.ToString("C", new CultureInfo("en-US")));
        output.Line("Цена :C (de-DE)", price.ToString("C", new CultureInfo("de-DE")));
        output.Line("Цена :C (Invariant)", price.ToString("C", CultureInfo.InvariantCulture));

        output.Line();
        output.Line("Число :N2", 1234567.891.ToString("N2", CultureInfo.InvariantCulture));
        output.Line("Проценты :P0", 0.2.ToString("P0", CultureInfo.InvariantCulture));
        output.Line("Hex :X", 255.ToString("X"));
    }

    public override Quiz Quiz => new(
        "Почему один и тот же {price:C} печатает то ₴, то $?",
        new[]
        {
            "Это баг форматирования",
            "Спецификатор C зависит от CultureInfo текущего потока/ОС",
            "decimal сам выбирает валюту",
            "Зависит от знака числа"
        },
        1,
        "Валютный и числовой форматы берут символы и разделители из CultureInfo.CurrentCulture.");
}
