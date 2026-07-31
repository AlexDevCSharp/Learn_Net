using LearnNet.Core.Lessons.Abstractions;

namespace LearnNet.Core.Lessons.Module00;

/// <summary>Тема 9: char и Unicode.</summary>
public sealed class CharUnicodeLesson : LessonBase
{
    public override string Id => "0.09-char-unicode";
    public override int Module => 0;
    public override string Title => "char и Unicode";
    public override Level Level => Level.Beginner;
    public override string Category => "Основы языка";

    public override string Summary =>
        "char — это 16-битная UTF-16 единица, а эмодзи занимает целых два char.";

    public override string Explanation =>
        """
        Что это. char — value-тип размером 16 бит: одна кодовая единица UTF-16. Строка — это
        последовательность char'ов. Для символов из «базовой» части Unicode один char = один
        символ.

        Суррогатные пары. Юникод больше 65 536 символов, а 16 бит столько не вмещает. Символы
        за пределами базовой плоскости (эмодзи, редкие иероглифы) кодируются ДВУМЯ char'ами —
        суррогатной парой. Поэтому "😀".Length == 2, хотя визуально это один символ.

        Rune. Чтобы работать с настоящими кодовыми точками (code points), а не с UTF-16
        единицами, есть тип System.Text.Rune и метод EnumerateRunes(). Он считает символы
        правильно — эмодзи как одну руну.

        Подвох. Никогда не «режьте» строки по индексам char вслепую (Substring, s[i]) — можно
        разорвать суррогатную пару и получить битый символ. Для посимвольной обработки —
        руны или StringInfo.
        """;

    public override string Code =>
        """
        char c = 'A';
        output.Line("Символ", c);
        output.Line("Код (UTF-16)", (int)c);
        output.Line("Это буква?", char.IsLetter(c));

        string emoji = "😀";
        output.Line("Длина строки в char", emoji.Length);                  // 2 — суррогатная пара
        output.Line("Длина в кодовых точках (Rune)", emoji.EnumerateRunes().Count()); // 1
        """;

    protected override void Demo(DemoResult output)
    {
        char c = 'A';
        output.Line("Символ", c);
        output.Line("Код (UTF-16)", (int)c);
        output.Line("Это буква?", char.IsLetter(c));

        string emoji = "😀";
        output.Line("Длина строки в char", emoji.Length);
        output.Line("Длина в кодовых точках (Rune)", emoji.EnumerateRunes().Count());
    }

    public override Quiz Quiz => new(
        "Почему \"😀\".Length возвращает 2, а не 1?",
        new[]
        {
            "Это баг .NET",
            "Эмодзи вне базовой плоскости кодируется суррогатной парой — двумя char (UTF-16)",
            "Length всегда возвращает чётное число",
            "Строка хранит завершающий нулевой символ"
        },
        1,
        "char — это UTF-16 единица; символы вне BMP занимают два char, отсюда Length == 2.");
}
