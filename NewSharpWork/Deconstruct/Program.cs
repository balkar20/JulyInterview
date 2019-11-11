using System;

namespace Deconstruct
{
    class Program
    {
        static void Main(string[] args)
        {
            (var myX, var myY) = new Point(4,5);
            Console.WriteLine($"{myX } && { myY}");

            (var first, var middle, var last) = LookupName(4);
            Console.WriteLine($"{first} {middle} {last}");
        }

        static (string, string, string) LookupName(long id) // возвращаемый тип - кортеж
        {
    return ("Vlad", "Alex", "Balkarov"); // литерал кортежа
        }
    }

    class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y) { X = x; Y = y; }
        public void Deconstruct(out int x, out int y) { x = X; y = Y; }
    }
}
