using System;
namespace Ranges
{
    class Program
    {
        static void Main(string[] args)
        {
            //T[] values1 = arrayOfT[..index2]; // диапазон от начала массива до index2
            //T[] values2 = arrayOfT[index1..]; // диапазон от index1 до конца массива

            var values = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var range1 = new Range(4, 8);
            int[] arr1 = values[range1]; // [4, 5, 6, 7]

            int[] arr2 = values[..^5]; // [0, 1, 2, 3, 4]
            int[] arr3 = values[^2..]; // [8, 9]

            string name = "Fox News"[0..3]; // "Fox"

            foreach (var val in values[3..6])
                Console.WriteLine(val); // выведет в консоль 3, 4 и 5
        }
    }
}
