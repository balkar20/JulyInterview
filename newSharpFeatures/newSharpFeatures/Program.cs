using System;

namespace newSharpFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            //(null as Person)!.Adress;
            Console.WriteLine("Hello World!");
        }
    }

    class  Person
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Info { get; set; }
    }
}
