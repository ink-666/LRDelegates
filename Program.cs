```csharp
using System; // Подключает базовые классы C#, например Console, Func, Action и Predicate
using System.Collections.Generic; // Нужен для работы со списками List<T>

// Пространство имён объединяет классы и структуры этой лабораторной работы
namespace DelegatesLab
{
    // ==========================================================
    // ОБЪЯВЛЕНИЕ ДЕЛЕГАТОВ (различные виды)
    // ==========================================================

    // Делегат для уведомлений: принимает строку-сообщение и ничего не возвращает
    public delegate void NotificationHandler(string message);

    // Обобщённый делегат для математических операций: принимает два значения одного типа и возвращает результат того же типа
    public delegate T MathOperation<T>(T a, T b);

    // Обобщённый делегат для фильтрации: принимает элемент и возвращает true или false
    public delegate bool FilterCondition<T>(T item);

    // Обобщённый делегат для преобразования: принимает значение и возвращает преобразованное значение того же типа
    public delegate T Transformer<T>(T input);

    // ==========================================================
    // СТРУКТУРА Point (из предыдущей работы)
    // ==========================================================

    // Структура описывает точку с координатами X и Y
    public struct Point
    {
        // Свойство X хранит координату точки по горизонтали
        public int X { get; set; }

        // Свойство Y хранит координату точки по вертикали
        public int Y { get; set; }

        // Конструктор создаёт точку с заданными координатами
        public Point(int x, int y)
        {
            // Записываем переданное значение x в свойство X
            X = x;

            // Записываем переданное значение y в свойство Y
            Y = y;
        }

        // Метод выводит координаты точки
        public void Display()
        {
            // Печатаем точку в формате (X, Y)
            Console.WriteLine($"   Точка: ({X}, {Y})");
        }

        // Перегруженный метод Display с дополнительной подписью
        public void Display(string prefix)
        {
            // Выводим подпись и координаты точки
            Console.WriteLine($"   {prefix}: ({X}, {Y})");
        }
    }

    // ==========================================================
    // СТРУКТУРА Rectangle (из предыдущей работы)
    // ==========================================================

    // Структура описывает прямоугольник
    public struct Rectangle
    {
        // Ширина прямоугольника
        public double Width { get; set; }

        // Высота прямоугольника
        public double Height { get; set; }

        // Конструктор создаёт прямоугольник с заданной шириной и высотой
        public Rectangle(double width, double height)
        {
            // Записываем ширину
            Width = width;

            // Записываем высоту
            Height = height;
        }

        // Свойство Area вычисляет площадь прямоугольника
        public double Area => Width * Height;

        // Метод выводит информацию о прямоугольнике
        public void Display()
        {
            // F2 выводит площадь с двумя знаками после запятой
            Console.WriteLine($"   Прямоугольник: {Width} x {Height}, площадь: {Area:F2}");
        }
    }

    // ==========================================================
    // КЛАСС Vehicle (из предыдущей работы)
    // ==========================================================

    // Класс описывает транспортное средство
    public class Vehicle
    {
        // Марка автомобиля
        public string Make { get; set; }

        // Модель автомобиля
        public string Model { get; set; }

        // Год выпуска
        public int Year { get; set; }

        // Цена автомобиля
        public decimal Price { get; set; }

        // Конструктор создаёт объект Vehicle с заданными данными
        public Vehicle(string make, string model, int year, decimal price)
        {
            // Записываем марку
            Make = make;

            // Записываем модель
            Model = model;

            // Записываем год выпуска
            Year = year;

            // Записываем цену
            Price = price;
        }

        // virtual означает, что метод можно переопределить в наследниках
        public virtual void DisplayInfo()
        {
            // C выводит цену в формате валюты
            Console.WriteLine($"   {Make} {Model}, {Year} год, {Price:C}");
        }
    }

    // ==========================================================
    // КЛАСС Helper С ПЕРЕГРУЗКОЙ МЕТОДОВ
    // ==========================================================

    // Класс содержит вспомогательные методы
    public class Helper
    {
        // Метод Display для int
        public void Display(int value)
        {
            // Выводим целое число
            Console.WriteLine($"   [Helper] int: {value}");
        }

        // Метод Display для string
        public void Display(string value)
        {
            // Выводим строку
            Console.WriteLine($"   [Helper] string: \"{value}\"");
        }

        // Метод Display для double
        public void Display(double value)
        {
            // Выводим дробное число с двумя знаками после запятой
            Console.WriteLine($"   [Helper] double: {value:F2}");
        }

        // Метод Display для Point
        public void Display(Point point)
        {
            // Выводим координаты точки
            Console.WriteLine($"   [Helper] Point: ({point.X}, {point.Y})");
        }

        // Метод Display для Rectangle
        public void Display(Rectangle rect)
        {
            // Выводим размеры прямоугольника
            Console.WriteLine($"   [Helper] Rectangle: {rect.Width} x {rect.Height}");
        }

        // Метод Display для Vehicle
        public void Display(Vehicle vehicle)
        {
            // Выводим марку и модель автомобиля
            Console.WriteLine($"   [Helper] Vehicle: {vehicle.Make} {vehicle.Model}");
        }

        // Обобщённый метод меняет местами значения любых двух переменных одного типа
        public void Swap<T>(ref T a, ref T b)
        {
            // Временно сохраняем значение первой переменной
            T temp = a;

            // В первую переменную записываем значение второй
            a = b;

            // Во вторую переменную записываем сохранённое старое значение первой
            b = temp;
        }
    }

    // ==========================================================
    // КЛАСС ДЛЯ ДЕМОНСТРАЦИИ ДЕЛЕГАТОВ
    // ==========================================================

    // Класс показывает разные способы использования делегатов
    public class DelegateDemo
    {
        // Поле-делегат для уведомлений; ? означает, что значение может быть null
        public NotificationHandler? Notifier;

        // Делегат для операций с int
        public MathOperation<int>? IntOperation;

        // Делегат для операций с double
        public MathOperation<double>? DoubleOperation;

        // Встроенный делегат Func принимает два int и возвращает int
        public Func<int, int, int>? FuncOperation;

        // Встроенный делегат Action принимает string и ничего не возвращает
        public Action<string>? ActionLogger;

        // Встроенный делегат Predicate принимает int и возвращает bool
        public Predicate<int>? PredicateFilter;

        // Метод складывает два целых числа
        public int Add(int a, int b)
        {
            // Сообщаем, какой метод был вызван
            Console.WriteLine("      → Вызван Add(int, int)");

            // Возвращаем сумму
            return a + b;
        }

        // Метод умножает два целых числа
        public int Multiply(int a, int b)
        {
            // Сообщаем, какой метод был вызван
            Console.WriteLine("      → Вызван Multiply(int, int)");

            // Возвращаем произведение
            return a * b;
        }

        // Метод складывает два дробных числа
        public double AddDouble(double a, double b)
        {
            // Сообщаем, какой метод был вызван
            Console.WriteLine("      → Вызван AddDouble(double, double)");

            // Возвращаем сумму
            return a + b;
        }

        // Метод проверяет, является ли число чётным
        public bool IsEven(int n)
        {
            // Если остаток от деления на 2 равен 0, число чётное
            return n % 2 == 0;
        }

        // Метод для демонстрации многоадресного делегата
        public void Method1() => Console.WriteLine("      Method1 выполнен");

        // Метод для демонстрации многоадресного делегата
        public void Method2() => Console.WriteLine("      Method2 выполнен");

        // Метод для демонстрации многоадресного делегата
        public void Method3() => Console.WriteLine("      Method3 выполнен");

        // Метод принимает список и условие фильтрации в виде делегата
        public List<T> FilterList<T>(List<T> items, FilterCondition<T> condition)
        {
            // Если Notifier не равен null, отправляем уведомление
            Notifier?.Invoke($"Фильтрация списка из {items.Count} элементов");

            // Создаём новый список для элементов, прошедших фильтр
            List<T> result = new List<T>();

            // Перебираем все элементы исходного списка
            foreach (T item in items)
            {
                // Если условие возвращает true, добавляем элемент в результат
                if (condition(item))
                    result.Add(item);
            }

            // Отправляем уведомление о количестве элементов после фильтрации
            Notifier?.Invoke($"После фильтрации осталось {result.Count} элементов");

            // Возвращаем отфильтрованный список
            return result;
        }

        // Метод принимает список и делегат для преобразования каждого элемента
        public List<T> TransformList<T>(List<T> items, Transformer<T> transformer)
        {
            // Если Notifier не равен null, отправляем уведомление
            Notifier?.Invoke($"Преобразование списка из {items.Count} элементов");

            // Создаём список для преобразованных элементов
            List<T> result = new List<T>();

            // Перебираем исходный список
            foreach (T item in items)
            {
                // Преобразуем элемент через делегат и добавляем в результат
                result.Add(transformer(item));
            }

            // Возвращаем преобразованный список
            return result;
        }

        // Метод выполняет назначенную математическую операцию
        public int ExecuteOperation(int a, int b)
        {
            // Отправляем уведомление о начале операции
            Notifier?.Invoke($"Выполняется операция над числами {a} и {b}");

            // Проверяем, назначена ли операция
            if (IntOperation != null)
            {
                // Вызываем делегат как обычный метод
                int result = IntOperation(a, b);

                // Отправляем уведомление с результатом
                Notifier?.Invoke($"Результат: {result}");

                // Возвращаем результат операции
                return result;
            }

            // Если операция не назначена, выводим уведомление об ошибке
            Notifier?.Invoke("Ошибка: операция не назначена!");

            // Возвращаем 0 как значение по умолчанию
            return 0;
        }
    }

    // ==========================================================
    // ГЛАВНЫЙ КЛАСС ПРОГРАММЫ
    // ==========================================================

    // Главный класс программы
    class Program
    {
        // Главный метод, с которого начинается выполнение программы
        static void Main(string[] args)
        {
            // Выводим верхнюю границу заголовка
            Console.WriteLine("==========================================");

            // Выводим название лабораторной работы
            Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №5: ДЕЛЕГАТЫ");

            // Выводим нижнюю границу заголовка и пустую строку
            Console.WriteLine("==========================================\n");

            // Кратко объясняем назначение делегатов
            Console.WriteLine("📌 ЧТО ТАКОЕ ДЕЛЕГАТЫ?");

            // Объясняем, что делегат хранит ссылку на метод
            Console.WriteLine("   Делегаты - это объекты, которые указывают на методы.");

            // Объясняем, зачем нужны делегаты
            Console.WriteLine("   Они позволяют передавать методы как параметры,\n");

            // Создаём объект вспомогательного класса
            Helper helper = new Helper();

            // Создаём объект класса для демонстрации делегатов
            DelegateDemo demo = new DelegateDemo();

            // ==========================================================
            // ЧАСТЬ 1: ПРОСТОЙ ДЕЛЕГАТ (УВЕДОМЛЕНИЯ)
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок первой части
            Console.WriteLine("ЧАСТЬ 1: ПРОСТОЙ ДЕЛЕГАТ (УВЕДОМЛЕНИЯ)");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Объясняем назначение делегата уведомлений
            Console.WriteLine("\n📌 ЗАЧЕМ: Позволяет одним частям программы уведомлять другие");

            // Продолжаем объяснение
            Console.WriteLine("   о событиях, не создавая жестких связей между ними.\n");

            // Назначаем делегату Notifier лямбда-выражение
            demo.Notifier = (message) =>
            {
                // Лямбда выводит сообщение уведомления
                Console.WriteLine($"   [Уведомление] {message}");
            };

            // Вызываем делегат, если он не равен null
            demo.Notifier?.Invoke("Система запущена");

            // Выводим пустую строку
            Console.WriteLine();

            // ==========================================================
            // ЧАСТЬ 2: ДЕЛЕГАТ С ПАРАМЕТРАМИ (МАТЕМАТИЧЕСКИЕ ОПЕРАЦИИ)
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок второй части
            Console.WriteLine("ЧАСТЬ 2: ДЕЛЕГАТ С ПАРАМЕТРАМИ");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Объясняем пользу делегата с параметрами
            Console.WriteLine("\n📌 ЗАЧЕМ: Позволяет подставлять разные операции");

            // Приводим примеры операций
            Console.WriteLine("   (сложение, умножение и т.д.) без изменения кода.\n");

            // Назначаем делегату метод сложения
            demo.IntOperation = demo.Add;

            // Выполняем назначенную операцию над числами 10 и 5
            demo.ExecuteOperation(10, 5);

            // Выводим пустую строку
            Console.WriteLine();

            // Меняем операцию: теперь делегат указывает на метод умножения
            demo.IntOperation = demo.Multiply;

            // Выполняем уже умножение над теми же числами
            demo.ExecuteOperation(10, 5);

            // Выводим пустую строку
            Console.WriteLine();

            // ==========================================================
            // ЧАСТЬ 3: МНОГОАДРЕСНЫЙ ДЕЛЕГАТ (ЦЕПОЧКА ВЫЗОВОВ)
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок третьей части
            Console.WriteLine("ЧАСТЬ 3: МНОГОАДРЕСНЫЙ ДЕЛЕГАТ");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Объясняем смысл многоадресного делегата
            Console.WriteLine("\n📌 ЗАЧЕМ: Один вызов запускает несколько методов.");

            // Объясняем, где это используется
            Console.WriteLine("   Используется в событиях и системах плагинов.\n");

            // Создаём многоадресный делегат типа Action
            Action multiDelegate = null;

            // Добавляем в цепочку первый метод
            multiDelegate += demo.Method1;

            // Добавляем в цепочку второй метод
            multiDelegate += demo.Method2;

            // Добавляем в цепочку третий метод
            multiDelegate += demo.Method3;

            // Подпись перед вызовом цепочки методов
            Console.WriteLine("   Вызов всех методов сразу:");

            // Вызываем все методы из цепочки, если делегат не null
            multiDelegate?.Invoke();

            // Подпись перед повторным вызовом
            Console.WriteLine("\n   После удаления Method2:");

            // Удаляем Method2 из цепочки вызовов
            multiDelegate -= demo.Method2;

            // Вызываем оставшиеся методы
            multiDelegate?.Invoke();

            // Выводим пустую строку
            Console.WriteLine();

            // ==========================================================
            // ЧАСТЬ 4: ОБОБЩЕННЫЙ ДЕЛЕГАТ
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок четвёртой части
            Console.WriteLine("ЧАСТЬ 4: ОБОБЩЕННЫЙ ДЕЛЕГАТ");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Объясняем назначение обобщённых делегатов
            Console.WriteLine("\n📌 ЗАЧЕМ: Позволяет работать с любыми типами данных");

            // Продолжаем объяснение
            Console.WriteLine("   без дублирования кода.\n");

            // Создаём обобщённый делегат для сложения int
            MathOperation<int> intOp = (a, b) => a + b;

            // Создаём обобщённый делегат для сложения double
            MathOperation<double> doubleOp = (a, b) => a + b;

            // Создаём обобщённый делегат для объединения строк
            MathOperation<string> stringOp = (a, b) => a + " " + b;

            // Выводим результат операции с int
            Console.WriteLine($"   int: 5 + 3 = {intOp(5, 3)}");

            // Выводим результат операции с double
            Console.WriteLine($"   double: 5.5 + 3.2 = {doubleOp(5.5, 3.2):F2}");

            // Выводим результат операции со строками
            Console.WriteLine($"   string: 'Hello' + 'World' = '{stringOp("Hello", "World")}'");

            // Выводим пустую строку
            Console.WriteLine();

            // ==========================================================
            // ЧАСТЬ 5: ДЕЛЕГАТ КАК ПАРАМЕТР МЕТОДА (ФИЛЬТРАЦИЯ)
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок пятой части
            Console.WriteLine("ЧАСТЬ 5: ДЕЛЕГАТ КАК ПАРАМЕТР МЕТОДА");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Объясняем, что делегат позволяет менять поведение метода
            Console.WriteLine("\n📌 ЗАЧЕМ: Метод может вести себя по-разному");

            // Уточняем, что делегат работает как стратегия
            Console.WriteLine("   в зависимости от переданного делегата (стратегия).\n");

            // Создаём список чисел от 1 до 10
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Выводим исходный список
            Console.WriteLine($"   Исходный список: [{string.Join(", ", numbers)}]\n");

            // Подпись к первому фильтру
            Console.WriteLine("   Фильтр: четные числа");

            // Передаём в метод FilterList условие: оставить только чётные числа
            var evenNumbers = demo.FilterList(numbers, n => n % 2 == 0);

            // Выводим результат фильтрации
            Console.WriteLine($"   Результат: [{string.Join(", ", evenNumbers)}]\n");

            // Подпись ко второму фильтру
            Console.WriteLine("   Фильтр: числа > 5");

            // Передаём условие: оставить только числа больше 5
            var greaterThanFive = demo.FilterList(numbers, n => n > 5);

            // Выводим результат фильтрации
            Console.WriteLine($"   Результат: [{string.Join(", ", greaterThanFive)}]\n");

            // ==========================================================
            // ЧАСТЬ 6: ДЕЛЕГАТ ДЛЯ ПРЕОБРАЗОВАНИЯ ДАННЫХ
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок шестой части
            Console.WriteLine("ЧАСТЬ 6: ДЕЛЕГАТ ДЛЯ ПРЕОБРАЗОВАНИЯ");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Объясняем назначение преобразующего делегата
            Console.WriteLine("\n📌 ЗАЧЕМ: Позволяет применять разные преобразования");

            // Продолжаем объяснение
            Console.WriteLine("   к элементам коллекции без изменения метода.\n");

            // Подпись к преобразованию
            Console.WriteLine("   Преобразование: возведение в квадрат");

            // Преобразуем каждое число в его квадрат
            var squares = demo.TransformList(numbers, n => n * n);

            // Выводим результат
            Console.WriteLine($"   Результат: [{string.Join(", ", squares)}]\n");

            // Подпись ко второму преобразованию
            Console.WriteLine("   Преобразование: умножение на 2");

            // Умножаем каждое число списка на 2
            var doubled = demo.TransformList(numbers, n => n * 2);

            // Выводим результат
            Console.WriteLine($"   Результат: [{string.Join(", ", doubled)}]\n");

            // ==========================================================
            // ЧАСТЬ 7: ВСТРОЕННЫЕ ДЕЛЕГАТЫ (FUNC, ACTION, PREDICATE)
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок седьмой части
            Console.WriteLine("ЧАСТЬ 7: ВСТРОЕННЫЕ ДЕЛЕГАТЫ");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Объясняем пользу встроенных делегатов
            Console.WriteLine("\n📌 ЗАЧЕМ: Не нужно объявлять свои делегаты");

            // Продолжаем объяснение
            Console.WriteLine("   для стандартных задач.\n");

            // Func принимает параметры и возвращает значение
            Func<int, int, int> addFunc = (a, b) => a + b;

            // Вызываем Func и выводим результат
            Console.WriteLine($"   Func<int,int,int>: 15 + 25 = {addFunc(15, 25)}");

            // Action выполняет действие и ничего не возвращает
            Action<string> printAction = (s) => Console.WriteLine($"   Action: {s}");

            // Вызываем Action
            printAction("Hello from Action!");

            // Predicate принимает значение и возвращает true или false
            Predicate<int> isEvenPredicate = (n) => n % 2 == 0;

            // Проверяем число 6
            Console.WriteLine($"   Predicate: 6 четное? {isEvenPredicate(6)}");

            // Проверяем число 7
            Console.WriteLine($"   Predicate: 7 четное? {isEvenPredicate(7)}");

            // Выводим пустую строку
            Console.WriteLine();

            // ==========================================================
            // ЧАСТЬ 8: ПРИМЕНЕНИЕ К СТРУКТУРАМ
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок восьмой части
            Console.WriteLine("ЧАСТЬ 8: ПРИМЕНЕНИЕ К СТРУКТУРАМ");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Создаём список точек
            List<Point> points = new List<Point>
            {
                // Первая точка
                new Point(1, 2),

                // Вторая точка
                new Point(3, 4),

                // Третья точка
                new Point(5, 6),

                // Четвёртая точка
                new Point(7, 8)
            };

            // Подпись перед выводом отфильтрованных точек
            Console.WriteLine("\n   Точки с X > 3:");

            // Фильтруем точки: оставляем только те, у которых X больше 3
            var filteredPoints = FilterPoints(points, p => p.X > 3);

            // Перебираем отфильтрованные точки
            foreach (var p in filteredPoints)
            {
                // Выводим каждую точку с подписью
                p.Display("     ");
            }

            // ==========================================================
            // ДЕМОНСТРАЦИЯ ПЕРЕГРУЗКИ МЕТОДОВ (ИЗ ПРЕДЫДУЩЕЙ РАБОТЫ)
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок раздела с перегрузкой методов
            Console.WriteLine("ПЕРЕГРУЗКА МЕТОДОВ (ИЗ ПРЕДЫДУЩЕЙ РАБОТЫ)");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Подпись перед демонстрацией перегрузки Display
            Console.WriteLine("\n📌 Перегрузка Display для разных типов:\n");

            // Вызывается версия Display для int
            helper.Display(42);

            // Вызывается версия Display для string
            helper.Display("Привет");

            // Вызывается версия Display для double
            helper.Display(3.14);

            // Вызывается версия Display для Point
            helper.Display(new Point(10, 20));

            // Вызывается версия Display для Rectangle
            helper.Display(new Rectangle(5, 3));

            // Вызывается версия Display для Vehicle
            helper.Display(new Vehicle("Toyota", "Camry", 2022, 2900000));

            // ==========================================================
            // ИТОГИ
            // ==========================================================

            // Выводим разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Выводим заголовок итогов
            Console.WriteLine("ИТОГИ РАБОТЫ");

            // Выводим разделитель
            Console.WriteLine(new string('=', 60));

            // Выводим подпись к списку делегатов
            Console.WriteLine("\n📌 ДЕЛЕГАТЫ В ЭТОЙ РАБОТЕ:");

            // Перечисляем делегат уведомлений
            Console.WriteLine("   1. Уведомления (NotificationHandler)");

            // Перечисляем математический делегат
            Console.WriteLine("   2. Математические операции (MathOperation<T>)");

            // Перечисляем многоадресные вызовы
            Console.WriteLine("   3. Многоадресные вызовы (Action)");

            // Перечисляем обобщённый делегат
            Console.WriteLine("   4. Обобщенные делегаты (Transformer<T>)");

            // Перечисляем делегат как параметр
            Console.WriteLine("   5. Делегаты как параметры (FilterCondition<T>)");

            // Перечисляем встроенные делегаты
            Console.WriteLine("   6. Встроенные делегаты (Func, Action, Predicate)");

            // Выводим подпись к перегрузке методов
            Console.WriteLine("\n📌 ПЕРЕГРУЗКА МЕТОДОВ:");

            // Перечисляем перегрузку для int
            Console.WriteLine("   1. Display(int)");

            // Перечисляем перегрузку для string
            Console.WriteLine("   2. Display(string)");

            // Перечисляем перегрузку для double
            Console.WriteLine("   3. Display(double)");

            // Перечисляем перегрузку для Point
            Console.WriteLine("   4. Display(Point)");

            // Перечисляем перегрузку для Rectangle
            Console.WriteLine("   5. Display(Rectangle)");

            // Перечисляем перегрузку для Vehicle
            Console.WriteLine("   6. Display(Vehicle)");

            // Выводим финальный разделитель
            Console.WriteLine("\n" + new string('=', 60));

            // Сообщаем о завершении программы
            Console.WriteLine("ПРОГРАММА ЗАВЕРШЕНА");

            // Выводим финальный разделитель
            Console.WriteLine(new string('=', 60));
        }

        // Вспомогательный метод фильтрует список точек по переданному условию
        static List<Point> FilterPoints(List<Point> points, Func<Point, bool> condition)
        {
            // Создаём список для точек, которые подходят под условие
            List<Point> result = new List<Point>();

            // Перебираем все точки исходного списка
            foreach (var point in points)
            {
                // Если условие вернуло true, добавляем точку в результат
                if (condition(point))
                    result.Add(point);
            }

            // Возвращаем список подходящих точек
            return result;
        }
    }
}
```