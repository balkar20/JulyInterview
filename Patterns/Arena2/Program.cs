using System;

namespace Arena2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ITest test = new Test(2);
            MethTest(test);
            test.ShowI();
            Console.ReadLine();
        }

        static void MethTest(ITest test)
        {
            test.i = 10;
        }
    }

    public struct Test:ITest
    {
        public Test(int i)
        {
            this.i = i;
        }

        public int i { get; set; }

        public void ShowI()
        {
            Console.WriteLine($"Test i: {this.i}");
        }
    }

    public interface ITest
    {
        int i { get; set; }
        void ShowI();
    }
}
