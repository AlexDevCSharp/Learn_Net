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
    ├── Learn_Net.Core/         # домен + абстракции уроков + реестр (net8.0)
    │   ├── Domain/             # Product, Category, OrderStatus, ShopData
    │   └── Lessons/
    │       ├── Abstractions/   # ILesson, LessonBase, Quiz, DemoResult, Level
    │       ├── Registry/       # LessonRegistry (рефлексия)
    │       ├── Module00/       # уроки модуля 0 (основы языка)
    │       ├── Module01/       # уроки модуля 1 (управление потоком)
    │       └── Module02/       # уроки модуля 2 (ООП)
    ├── Learn_Net.Playground/   # консольный раннер: интерактивный прогон уроков (net8.0)
    ├── Learn_Net.Web/          # Blazor UI: дерево тем, теория, код, демо, квизы (net10.0)
    └── Learn_Net.Tests/        # xUnit: инварианты уроков и доменная логика (net10.0)
```

Дальше по плану: наполнение модулей 3–10 и, при необходимости, `Learn_Net.Api` (ASP.NET Core).

## Требования

- **.NET SDK** (проверено на 10.0.x; собирает `net8.0` и `net10.0`): <https://dotnet.microsoft.com/download>

## Запуск

Консольный тренажёр:

```bash
dotnet run --project src/Learn_Net.Playground
```

Веб (Blazor), затем открыть http://localhost:5221 :

```bash
dotnet run --project src/Learn_Net.Web --launch-profile http
```

Тесты:

```bash
dotnet test
```

## Как добавить новый урок

1. Создать класс в `Learn_Net.Core/Lessons/ModuleXX/`, унаследовать от `LessonBase`.
2. Заполнить метаданные (`Id`, `Module`, `Title`, `Level`, `Category`, `Summary`, `Explanation`, `Code`).
3. Реализовать `Demo(output)` и вернуть `Quiz`.
4. Всё — реестр подхватит урок автоматически, а тесты проверят инварианты.
