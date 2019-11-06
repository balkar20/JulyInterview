using System;


namespace Indexes
{
    class Program
    {
        static void Main(string[] args)
        {
            var indexFirst = new Index(0); // индекс первого элемента
            var indexLast = new Index(1, fromEnd: true); // индекс последнего элемента
            var index1 = new Index(5); // индекс 5 элемента
            var index2 = new Index(2, fromEnd: true); // индекс 2 элемента с конца

            var values = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int v1 = values[indexFirst]; // 0
            int v2 = values[0]; // 0
            bool areSame1 = indexFirst.Equals(0); // true

            int v3 = values[indexLast]; // 9
            int v4 = values[^2]; // 8
            bool areSame2 = indexLast.Equals(index2); // false
            Console.WriteLine($"indexLast == index2 : {areSame2}");
        }
    }
}
