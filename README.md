# Learn_Net

Единый .NET/C# проект для изучения, повторения и отработки навыков — от азов до сеньорских тем.
Полный учебный план: [CURRICULUM.md](CURRICULUM.md).

## Идея

- **Единый живой проект**, а не набор сниппетов. Само приложение построено на .NET и
  демонстрирует изучаемые концепции на себе.
- **Сквозной домен — интернет-магазин** (`Product`, `Category`, ...). Одни данные проходят через все темы.
- **Каждая тема = класс `ILesson`** (теория + живой демо-запуск + мини-квиз),
  подхватывается авто-реестром через рефлексию — добавил класс, и он появился сам.

## Структура

```
Learn_Net.sln
└── src/
    ├── Learn_Net.Core/         # домен + абстракции уроков + реестр
    │   ├── Domain/             # Product, Category, ShopData
    │   └── Lessons/
    │       ├── Abstractions/   # ILesson, LessonBase, Quiz, DemoResult, Level
    │       ├── Registry/       # LessonRegistry (рефлексия)
    │       ├── Module00/       # уроки модуля 0
    │       └── Module02/       # уроки модуля 2
    └── Learn_Net.Playground/   # консольный раннер: прогоняет все уроки
```

Дальше по плану: `Learn_Net.Api` (ASP.NET Core) и `Learn_Net.Web` (Blazor UI), `Learn_Net.Tests` (xUnit).

## Требования

- **.NET 8 SDK** (сейчас на машине только runtime — SDK нужно установить): <https://dotnet.microsoft.com/download/dotnet/8.0>

## Запуск

```bash
dotnet run --project src/Learn_Net.Playground
```

## Как добавить новый урок

1. Создать класс в `Learn_Net.Core/Lessons/ModuleXX/`, унаследовать от `LessonBase`.
2. Заполнить метаданные (`Id`, `Module`, `Title`, `Level`, `Category`, `Explanation`).
3. Реализовать `Demo(output)` и вернуть `Quiz`.
4. Всё — реестр подхватит урок автоматически.
