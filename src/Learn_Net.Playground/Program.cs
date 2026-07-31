using System.Text;
using LearnNet.Core.Lessons.Abstractions;
using LearnNet.Core.Lessons.Registry;

Console.OutputEncoding = Encoding.UTF8;

var registry = LessonRegistry.Default();
var lessons = registry.Lessons;

while (true)
{
    ShowMenu(lessons);

    Console.Write("\nВыбери урок (номер): ");
    var input = Console.ReadLine();

    if (!int.TryParse(input, out var choice) || choice < 0 || choice > lessons.Count)
    {
        Console.WriteLine("Не понял выбор, попробуй ещё раз.");
        continue;
    }

    if (choice == 0)
        break;

    ShowLesson(lessons[choice - 1]);
}

Console.WriteLine("Пока! Возвращайся оттачивать .NET.");

static void ShowMenu(IReadOnlyList<ILesson> lessons)
{
    Console.WriteLine();
    Console.WriteLine($"═══ Learn_Net — {lessons.Count} уроков ═══");
    for (var i = 0; i < lessons.Count; i++)
    {
        var l = lessons[i];
        Console.WriteLine($"  {i + 1,2}. [M{l.Module}] {l.Title}  ({l.Level})");
        Console.WriteLine($"      {l.Summary}");
    }
    Console.WriteLine("   0. Выход");
}

static void ShowLesson(ILesson lesson)
{
    var rule = new string('─', 64);

    Console.WriteLine();
    Console.WriteLine(rule);
    Console.WriteLine($"▶ {lesson.Title}   ({lesson.Level}, модуль {lesson.Module})");
    Console.WriteLine(rule);

    Console.WriteLine();
    Console.WriteLine("ТЕОРИЯ");
    Console.WriteLine(lesson.Explanation);

    Console.WriteLine();
    Console.WriteLine("ДЕМО (живой запуск)");
    Console.WriteLine(lesson.RunDemo());

    RunQuiz(lesson.Quiz);

    Console.WriteLine();
    Console.Write("Enter — вернуться к списку...");
    Console.ReadLine();
}

static void RunQuiz(Quiz quiz)
{
    Console.WriteLine();
    Console.WriteLine("КВИЗ");
    Console.WriteLine(quiz.Question);
    for (var i = 0; i < quiz.Options.Count; i++)
        Console.WriteLine($"   {i + 1}) {quiz.Options[i]}");

    Console.Write("Твой ответ (номер): ");
    var raw = Console.ReadLine();

    if (int.TryParse(raw, out var answer) && quiz.IsCorrect(answer - 1))
        Console.WriteLine("✅ Верно!");
    else
        Console.WriteLine($"❌ Неверно. Правильный ответ: {quiz.CorrectIndex + 1}) {quiz.CorrectOption}");

    Console.WriteLine($"💡 {quiz.Explanation}");
}
