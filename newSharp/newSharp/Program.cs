using System;
#nullable enable

namespace Nullables
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person("Vlad", "Balkarov", null);

            var person2 = (null as Person)!?.FullName;

            Console.WriteLine($"{person2}");
            Console.WriteLine($"{person.FirstName} {person.FullName}");
        }
    }

    class Person
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public Person(string first, string last, string middle) =>
          (FirstName, LastName, MiddleName) = (first, last, middle);
        public string FullName =>
          $"{FirstName} {MiddleName?[0]} {LastName}";
    }
}
