# Learn_Net — Учебный план (Curriculum)

> Единый .NET-проект для изучения, повторения и отработки C#/.NET.
> Цель: подготовка к интервью, поддержание формы, площадка для новых фишек.

## Концепция

- **Единый живой проект**, а не набор сниппетов. Само приложение (платформа обучения) построено на .NET и **демонстрирует концепции на себе**: дошёл до DI — приложение уже на DI; дошёл до async — эндпоинты асинхронные.
- **Сквозной домен: интернет-магазин.** Сущности: `Product`, `Category`, `Customer`, `Cart`, `CartItem`, `Order`, `OrderItem`, `Payment`, `Review`, `Inventory`, `Discount`. Одни и те же данные проходят через все темы.
- **Каждая тема = отдельный `ILesson`** (теория + живой запуск демо + мини-квиз), собираемый в авто-реестр через рефлексию.
- **Стек:** .NET 8+, Blazor для UI (UI тоже становится частью обучения).

## Технологическая структура (план)

```
Learn_Net.sln
├── Learn_Net.Core        // домен (магазин) + уроки (ILesson) + авто-реестр
├── Learn_Net.Api         // ASP.NET Core Web API
├── Learn_Net.Web         // Blazor UI (дерево тем, демо, квизы, режим интервью)
├── Learn_Net.Tests       // xUnit
└── Learn_Net.Playground  // консоль для быстрых экспериментов
```

## Уровни (Level)

- 🟢 Beginner — Модули 0–3
- 🟡 Intermediate — Модули 4–7
- 🔴 Advanced — Модули 8–10

---

## Модуль 0 — Платформа и основы языка 🟢
1. Что такое .NET — CLR, BCL, runtime vs SDK
2. .NET Framework vs Core vs .NET 8+, кроссплатформенность
3. Компиляция: IL, JIT vs AOT, assembly, метаданные
4. Garbage Collector — поколения, финализация, `IDisposable` (обзор)
5. Структура программы — `Main`, top-level statements, namespace, `using`
6. Value vs reference типы, стек vs куча
7. Boxing / unboxing
8. Примитивы — `int`/`long`/`double`/`decimal`, диапазоны, overflow, `checked`
9. `char` и Unicode
10. Строки — immutability, интернирование
11. `StringBuilder` и производительность
12. Форматирование и интерполяция строк, culture
13. Переменные — `var`, `const`, `readonly`, области видимости, `default`
14. Операторы — арифметика, сравнение, логика
15. Null-операторы — `?.`, `??`, `??=`

## Модуль 1 — Управление потоком и методы 🟢
16. `if`/`else` и тернарный оператор
17. `switch` statement vs `switch` expression
18. Циклы `for`/`while`/`do`
19. `foreach` — как работает под капотом
20. Методы — сигнатура, возврат, перегрузка
21. Локальные функции
22. Параметры — по значению vs `ref`/`out`/`in`
23. `params`, опциональные и именованные параметры
24. Рекурсия и стек вызовов

## Модуль 2 — ООП: фундамент 🟢
25. Классы и объекты, `this`
26. Свойства — auto, `init`, expression-bodied, вычисляемые
27. Конструкторы — цепочка `this()`/`base()`, статические
28. Primary constructors
29. Модификаторы доступа — `public`/`private`/`protected`/`internal`
30. Наследование — `base`, `sealed`
31. Полиморфизм — `virtual`/`override`/`new`
32. Абстрактные классы
33. Интерфейсы — контракты, множественная реализация
34. Default interface methods и explicit implementation
35. Абстрактный класс vs интерфейс (частый вопрос)

## Модуль 3 — Типы и структуры 🟢
36. `static` классы и члены
37. `enum` и `[Flags]` (напр. `OrderStatus`)
38. `struct` и value-семантика
39. `readonly struct` / `ref struct`
40. `record` (class) — value equality, `with`
41. `record struct`
42. Nested и `partial` типы
43. `Equals`/`GetHashCode`/`ToString`
44. Operator overloading и `implicit`/`explicit` преобразования

## Модуль 4 — Коллекции и обобщения 🟡
45. Массивы — одномерные, многомерные, jagged
46. `List<T>` и внутреннее устройство (capacity)
47. `Dictionary<K,V>` и хеширование
48. `HashSet<T>`
49. `Queue`/`Stack`/`LinkedList`
50. Generics — параметры типа
51. Ограничения generics (`where`)
52. Ковариантность / контравариантность
53. `IEnumerable`/`IEnumerator`
54. Итераторы — `yield return`/`yield break`
55. `IComparable`/`IComparer` и сортировка

## Модуль 5 — Функциональные возможности 🟡
56. Делегаты — `Func`/`Action`/`Predicate`
57. Multicast делегаты
58. События (`event`)
59. Лямбды
60. Замыкания (closures) и захват переменных
61. Extension methods (как устроен LINQ)
62. LINQ — фильтрация и проекция (`Where`/`Select`)
63. LINQ — сортировка и множества (`OrderBy`/`Distinct`/`Union`)
64. LINQ — группировка и соединения (`GroupBy`/`Join`)
65. LINQ — агрегация (`Sum`/`Count`/`Aggregate`)
66. LINQ — отложенное выполнение и материализация
67. `IQueryable` vs `IEnumerable`

## Модуль 6 — Надёжность и ресурсы 🟡
68. Исключения — `try`/`catch`/`finally`
69. Иерархия исключений, `throw` vs `throw ex`
70. Пользовательские исключения
71. `IDisposable`/`using` и `IAsyncDisposable`
72. Nullable value types
73. Nullable reference types (`?`, `!`)
74. Pattern matching — type/property/relational
75. Кортежи (tuples), `ValueTuple`, деконструкция

## Модуль 7 — Асинхронность и параллелизм 🟡
76. `Thread` и `ThreadPool`
77. `Task` и `Task<T>`
78. `async`/`await` — модель выполнения
79. `SynchronizationContext` и `ConfigureAwait`
80. `CancellationToken` и отмена
81. Обработка ошибок в async, `AggregateException`
82. `Parallel.For`/`ForEach`
83. PLINQ
84. `lock`, race conditions, deadlock
85. `Interlocked` и `volatile`
86. Concurrent-коллекции (`ConcurrentDictionary`)
87. `Channel<T>` — producer/consumer

## Модуль 8 — Инфраструктура (приложение учит на себе) 🔴
88. Dependency Injection — IoC, время жизни (singleton/scoped/transient)
89. Конфигурация — `appsettings.json`, `IOptions`
90. Логирование — `ILogger`, уровни, структурное
91. Работа с файлами и `Stream`, async I/O
92. Сериализация — `System.Text.Json`, кастомные конвертеры
93. `HttpClient` и `IHttpClientFactory`
94. EF Core — `DbContext`, миграции
95. EF Core — запросы, отслеживание, связи
96. ASP.NET Core Web API — minimal API / контроллеры, роутинг
97. Middleware и request pipeline

## Модуль 9 — Blazor (UI как обучение) 🔴
98. Компоненты `.razor` и жизненный цикл
99. Параметры и биндинг (`[Parameter]`, two-way)
100. События и `EventCallback`
101. State и `StateHasChanged`
102. DI в Blazor
103. Формы и валидация (`EditForm`, data annotations)

## Модуль 10 — Качество и паттерны 🔴
104. Unit-тесты — xUnit, Arrange-Act-Assert
105. `[Theory]` и параметризованные тесты
106. Моки — Moq / NSubstitute
107. SOLID (на живом коде проекта)
108. Repository pattern
109. Factory pattern
110. Strategy pattern (напр. расчёт скидок)
111. Observer pattern (события заказа)
112. Decorator pattern
113. Mediator / CQRS (обзор)
114. Рефлексия — `Type`, динамический вызов (наш авто-реестр уроков)
115. Кастомные атрибуты
116. Фишки — Source Generators, `Span<T>`/`Memory<T>`

---

**Итого: 116 тем** по 11 модулям. Каждая → отдельный `ILesson` на домене интернет-магазина.

## Идеи для UI (Blazor)
- Дерево тем слева (по модулям/уровням), справа: теория → «Run demo» → мини-квиз.
- Прогресс-трекер (пройдено / отработано), как в Duolingo.
- Режим интервью: случайная тема + вопрос + таймер.
- Позже — мини-игры («собери код», карточки на скорость).
