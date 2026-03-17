using System;
using System.Collections.Generic;

namespace DelegatesLab
{
    // ==========================================================
    // ОБЪЯВЛЕНИЕ ДЕЛЕГАТОВ (различные виды)
    // ==========================================================
    
    // 1. Простой делегат для уведомлений
    public delegate void NotificationHandler(string message);
    
    // 2. Делегат для математических операций
    public delegate T MathOperation<T>(T a, T b);
    
    // 3. Делегат для фильтрации
    public delegate bool FilterCondition<T>(T item);
    
    // 4. Делегат для преобразования
    public delegate T Transformer<T>(T input);
    
    // ==========================================================
    // СТРУКТУРА Point (из предыдущей работы)
    // ==========================================================
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public void Display()
        {
            Console.WriteLine($"   Точка: ({X}, {Y})");
        }
        
        // Перегрузка метода Display
        public void Display(string prefix)
        {
            Console.WriteLine($"   {prefix}: ({X}, {Y})");
        }
    }
    
    // ==========================================================
    // СТРУКТУРА Rectangle (из предыдущей работы)
    // ==========================================================
    public struct Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }
        
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }
        
        public double Area => Width * Height;
        
        public void Display()
        {
            Console.WriteLine($"   Прямоугольник: {Width} x {Height}, площадь: {Area:F2}");
        }
    }
    
    // ==========================================================
    // КЛАСС Vehicle (из предыдущей работы)
    // ==========================================================
    public class Vehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        
        public Vehicle(string make, string model, int year, decimal price)
        {
            Make = make;
            Model = model;
            Year = year;
            Price = price;
        }
        
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"   {Make} {Model}, {Year} год, {Price:C}");
        }
    }
    
    // ==========================================================
    // КЛАСС Helper С ПЕРЕГРУЗКОЙ МЕТОДОВ
    // ==========================================================
    public class Helper
    {
        // Перегрузка Display для разных типов
        public void Display(int value)
        {
            Console.WriteLine($"   [Helper] int: {value}");
        }
        
        public void Display(string value)
        {
            Console.WriteLine($"   [Helper] string: \"{value}\"");
        }
        
        public void Display(double value)
        {
            Console.WriteLine($"   [Helper] double: {value:F2}");
        }
        
        public void Display(Point point)
        {
            Console.WriteLine($"   [Helper] Point: ({point.X}, {point.Y})");
        }
        
        public void Display(Rectangle rect)
        {
            Console.WriteLine($"   [Helper] Rectangle: {rect.Width} x {rect.Height}");
        }
        
        public void Display(Vehicle vehicle)
        {
            Console.WriteLine($"   [Helper] Vehicle: {vehicle.Make} {vehicle.Model}");
        }
        
        // Обобщенный метод Swap
        public void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
    
    // ==========================================================
    // КЛАСС ДЛЯ ДЕМОНСТРАЦИИ ДЕЛЕГАТОВ
    // ==========================================================
    public class DelegateDemo
    {
        // 1. Использование делегата для уведомлений
        public NotificationHandler? Notifier;
        
        // 2. Использование делегата для операций
        public MathOperation<int>? IntOperation;
        public MathOperation<double>? DoubleOperation;
        
        // 3. Использование встроенных делегатов
        public Func<int, int, int>? FuncOperation;
        public Action<string>? ActionLogger;
        public Predicate<int>? PredicateFilter;
        
        // Методы для демонстрации
        
        public int Add(int a, int b)
        {
            Console.WriteLine("      → Вызван Add(int, int)");
            return a + b;
        }
        
        public int Multiply(int a, int b)
        {
            Console.WriteLine("      → Вызван Multiply(int, int)");
            return a * b;
        }
        
        public double AddDouble(double a, double b)
        {
            Console.WriteLine("      → Вызван AddDouble(double, double)");
            return a + b;
        }
        
        public bool IsEven(int n)
        {
            return n % 2 == 0;
        }
        
        // Демонстрация многоадресного делегата
        public void Method1() => Console.WriteLine("      Method1 выполнен");
        public void Method2() => Console.WriteLine("      Method2 выполнен");
        public void Method3() => Console.WriteLine("      Method3 выполнен");
        
        // Метод с делегатом как параметром
        public List<T> FilterList<T>(List<T> items, FilterCondition<T> condition)
        {
            Notifier?.Invoke($"Фильтрация списка из {items.Count} элементов");
            
            List<T> result = new List<T>();
            foreach (T item in items)
            {
                if (condition(item))
                    result.Add(item);
            }
            
            Notifier?.Invoke($"После фильтрации осталось {result.Count} элементов");
            return result;
        }
        
        // Метод с делегатом для преобразования
        public List<T> TransformList<T>(List<T> items, Transformer<T> transformer)
        {
            Notifier?.Invoke($"Преобразование списка из {items.Count} элементов");
            
            List<T> result = new List<T>();
            foreach (T item in items)
            {
                result.Add(transformer(item));
            }
            
            return result;
        }
        
        // Выполнение операции с уведомлением
        public int ExecuteOperation(int a, int b)
        {
            Notifier?.Invoke($"Выполняется операция над числами {a} и {b}");
            
            if (IntOperation != null)
            {
                int result = IntOperation(a, b);
                Notifier?.Invoke($"Результат: {result}");
                return result;
            }
            
            Notifier?.Invoke("Ошибка: операция не назначена!");
            return 0;
        }
    }
    
    // ==========================================================
    // ГЛАВНЫЙ КЛАСС ПРОГРАММЫ
    // ==========================================================
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №5: ДЕЛЕГАТЫ");
            Console.WriteLine("==========================================\n");
            
            Console.WriteLine("📌 ЧТО ТАКОЕ ДЕЛЕГАТЫ?");
            Console.WriteLine("   Делегаты - это объекты, которые указывают на методы.");
            Console.WriteLine("   Они позволяют передавать методы как параметры,\n");
            
            // Создаем объекты
            Helper helper = new Helper();
            DelegateDemo demo = new DelegateDemo();
            
            // ==========================================================
            // ЧАСТЬ 1: ПРОСТОЙ ДЕЛЕГАТ (УВЕДОМЛЕНИЯ)
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ЧАСТЬ 1: ПРОСТОЙ ДЕЛЕГАТ (УВЕДОМЛЕНИЯ)");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\n📌 ЗАЧЕМ: Позволяет одним частям программы уведомлять другие");
            Console.WriteLine("   о событиях, не создавая жестких связей между ними.\n");
            
            // Настраиваем уведомления
            demo.Notifier = (message) => 
            {
                Console.WriteLine($"   [Уведомление] {message}");
            };
            
            demo.Notifier?.Invoke("Система запущена");
            Console.WriteLine();
            
            // ==========================================================
            // ЧАСТЬ 2: ДЕЛЕГАТ С ПАРАМЕТРАМИ (МАТЕМАТИЧЕСКИЕ ОПЕРАЦИИ)
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ЧАСТЬ 2: ДЕЛЕГАТ С ПАРАМЕТРАМИ");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\n📌 ЗАЧЕМ: Позволяет подставлять разные операции");
            Console.WriteLine("   (сложение, умножение и т.д.) без изменения кода.\n");
            
            // Назначаем сложение
            demo.IntOperation = demo.Add;
            demo.ExecuteOperation(10, 5);
            
            Console.WriteLine();
            
            // Меняем на умножение
            demo.IntOperation = demo.Multiply;
            demo.ExecuteOperation(10, 5);
            Console.WriteLine();
            
            // ==========================================================
            // ЧАСТЬ 3: МНОГОАДРЕСНЫЙ ДЕЛЕГАТ (ЦЕПОЧКА ВЫЗОВОВ)
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ЧАСТЬ 3: МНОГОАДРЕСНЫЙ ДЕЛЕГАТ");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\n📌 ЗАЧЕМ: Один вызов запускает несколько методов.");
            Console.WriteLine("   Используется в событиях и системах плагинов.\n");
            
            // Создаем цепочку
            Action multiDelegate = null;
            multiDelegate += demo.Method1;
            multiDelegate += demo.Method2;
            multiDelegate += demo.Method3;
            
            Console.WriteLine("   Вызов всех методов сразу:");
            multiDelegate?.Invoke();
            
            Console.WriteLine("\n   После удаления Method2:");
            multiDelegate -= demo.Method2;
            multiDelegate?.Invoke();
            Console.WriteLine();
            
            // ==========================================================
            // ЧАСТЬ 4: ОБОБЩЕННЫЙ ДЕЛЕГАТ
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ЧАСТЬ 4: ОБОБЩЕННЫЙ ДЕЛЕГАТ");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\n📌 ЗАЧЕМ: Позволяет работать с любыми типами данных");
            Console.WriteLine("   без дублирования кода.\n");
            
            // Обобщенный делегат для разных типов
            MathOperation<int> intOp = (a, b) => a + b;
            MathOperation<double> doubleOp = (a, b) => a + b;
            MathOperation<string> stringOp = (a, b) => a + " " + b;
            
            Console.WriteLine($"   int: 5 + 3 = {intOp(5, 3)}");
            Console.WriteLine($"   double: 5.5 + 3.2 = {doubleOp(5.5, 3.2):F2}");
            Console.WriteLine($"   string: 'Hello' + 'World' = '{stringOp("Hello", "World")}'");
            Console.WriteLine();
            
            // ==========================================================
            // ЧАСТЬ 5: ДЕЛЕГАТ КАК ПАРАМЕТР МЕТОДА (ФИЛЬТРАЦИЯ)
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ЧАСТЬ 5: ДЕЛЕГАТ КАК ПАРАМЕТР МЕТОДА");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\n📌 ЗАЧЕМ: Метод может вести себя по-разному");
            Console.WriteLine("   в зависимости от переданного делегата (стратегия).\n");
            
            // Создаем список чисел
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine($"   Исходный список: [{string.Join(", ", numbers)}]\n");
            
            // Фильтрация: четные числа
            Console.WriteLine("   Фильтр: четные числа");
            var evenNumbers = demo.FilterList(numbers, n => n % 2 == 0);
            Console.WriteLine($"   Результат: [{string.Join(", ", evenNumbers)}]\n");
            
            // Фильтрация: числа больше 5
            Console.WriteLine("   Фильтр: числа > 5");
            var greaterThanFive = demo.FilterList(numbers, n => n > 5);
            Console.WriteLine($"   Результат: [{string.Join(", ", greaterThanFive)}]\n");
            
            // ==========================================================
            // ЧАСТЬ 6: ДЕЛЕГАТ ДЛЯ ПРЕОБРАЗОВАНИЯ ДАННЫХ
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ЧАСТЬ 6: ДЕЛЕГАТ ДЛЯ ПРЕОБРАЗОВАНИЯ");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\n📌 ЗАЧЕМ: Позволяет применять разные преобразования");
            Console.WriteLine("   к элементам коллекции без изменения метода.\n");
            
            // Преобразование: квадраты чисел
            Console.WriteLine("   Преобразование: возведение в квадрат");
            var squares = demo.TransformList(numbers, n => n * n);
            Console.WriteLine($"   Результат: [{string.Join(", ", squares)}]\n");
            
            // Преобразование: умножение на 2
            Console.WriteLine("   Преобразование: умножение на 2");
            var doubled = demo.TransformList(numbers, n => n * 2);
            Console.WriteLine($"   Результат: [{string.Join(", ", doubled)}]\n");
            
            // ==========================================================
            // ЧАСТЬ 7: ВСТРОЕННЫЕ ДЕЛЕГАТЫ (FUNC, ACTION, PREDICATE)
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ЧАСТЬ 7: ВСТРОЕННЫЕ ДЕЛЕГАТЫ");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\n📌 ЗАЧЕМ: Не нужно объявлять свои делегаты");
            Console.WriteLine("   для стандартных задач.\n");
            
            // Func - возвращает значение
            Func<int, int, int> addFunc = (a, b) => a + b;
            Console.WriteLine($"   Func<int,int,int>: 15 + 25 = {addFunc(15, 25)}");
            
            // Action - не возвращает значение
            Action<string> printAction = (s) => Console.WriteLine($"   Action: {s}");
            printAction("Hello from Action!");
            
            // Predicate - возвращает bool
            Predicate<int> isEvenPredicate = (n) => n % 2 == 0;
            Console.WriteLine($"   Predicate: 6 четное? {isEvenPredicate(6)}");
            Console.WriteLine($"   Predicate: 7 четное? {isEvenPredicate(7)}");
            Console.WriteLine();
            
            // ==========================================================
            // ЧАСТЬ 8: ПРИМЕНЕНИЕ К СТРУКТУРАМ
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ЧАСТЬ 8: ПРИМЕНЕНИЕ К СТРУКТУРАМ");
            Console.WriteLine(new string('=', 60));
            
            // Создаем список точек
            List<Point> points = new List<Point>
            {
                new Point(1, 2),
                new Point(3, 4),
                new Point(5, 6),
                new Point(7, 8)
            };
            
            // Фильтруем точки
            Console.WriteLine("\n   Точки с X > 3:");
            var filteredPoints = FilterPoints(points, p => p.X > 3);
            foreach (var p in filteredPoints)
            {
                p.Display("     ");
            }
            
            // ==========================================================
            // ДЕМОНСТРАЦИЯ ПЕРЕГРУЗКИ МЕТОДОВ (ИЗ ПРЕДЫДУЩЕЙ РАБОТЫ)
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ПЕРЕГРУЗКА МЕТОДОВ (ИЗ ПРЕДЫДУЩЕЙ РАБОТЫ)");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\n📌 Перегрузка Display для разных типов:\n");
            
            helper.Display(42);
            helper.Display("Привет");
            helper.Display(3.14);
            helper.Display(new Point(10, 20));
            helper.Display(new Rectangle(5, 3));
            helper.Display(new Vehicle("Toyota", "Camry", 2022, 2900000));
            
            // ==========================================================
            // ИТОГИ
            // ==========================================================
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ИТОГИ РАБОТЫ");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\n📌 ДЕЛЕГАТЫ В ЭТОЙ РАБОТЕ:");
            Console.WriteLine("   1. Уведомления (NotificationHandler)");
            Console.WriteLine("   2. Математические операции (MathOperation<T>)");
            Console.WriteLine("   3. Многоадресные вызовы (Action)");
            Console.WriteLine("   4. Обобщенные делегаты (Transformer<T>)");
            Console.WriteLine("   5. Делегаты как параметры (FilterCondition<T>)");
            Console.WriteLine("   6. Встроенные делегаты (Func, Action, Predicate)");
            
            Console.WriteLine("\n📌 ПЕРЕГРУЗКА МЕТОДОВ:");
            Console.WriteLine("   1. Display(int)");
            Console.WriteLine("   2. Display(string)");
            Console.WriteLine("   3. Display(double)");
            Console.WriteLine("   4. Display(Point)");
            Console.WriteLine("   5. Display(Rectangle)");
            Console.WriteLine("   6. Display(Vehicle)");
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ПРОГРАММА ЗАВЕРШЕНА");
            Console.WriteLine(new string('=', 60));
        }
        
        // Вспомогательный метод для фильтрации точек
        static List<Point> FilterPoints(List<Point> points, Func<Point, bool> condition)
        {
            List<Point> result = new List<Point>();
            foreach (var point in points)
            {
                if (condition(point))
                    result.Add(point);
            }
            return result;
        }
    }
}