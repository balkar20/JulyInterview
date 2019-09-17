using System;
using System.Collections.Generic;
using System.Linq;
using Inheritance.DataStructure;
using NUnitLite;

class Program
{
	static void Main(string[] args)
	{
	    Category A11 = new Category("A", MessageType.Incoming, MessageTopic.Subscribe);
	    Category A21 = new Category("A", MessageType.Outgoing, MessageTopic.Subscribe);
	    Category A12 = new Category("A", MessageType.Incoming, MessageTopic.Error);
	    Category B11 = new Category("B", MessageType.Incoming, MessageTopic.Subscribe);
	    Category A11_copy = new Category("A", MessageType.Incoming, MessageTopic.Subscribe);
        Category[] Descending()
	    {
	        return new[] { A11, A12, A21, B11 };
	    }

        Console.WriteLine();
        //Console.WriteLine(A11.CompareTo(A21));
        //Console.WriteLine(A11.CompareTo(B11));

        //Console.WriteLine($"copy : {A11.CompareTo(B11)}");
        //Console.WriteLine(MessageType.Incoming.ToString().CompareTo(MessageType.Outgoing.ToString()));
        //Console.WriteLine(MessageTopic.Subscribe.ToString().CompareTo(MessageTopic.Error.ToString()));
        //Console.WriteLine($"___________________________________________");
        //Console.WriteLine($"___________________________________________");
        //Console.WriteLine($"___________________________________________");
        //Console.WriteLine($"A11 >= A12: {A11 >= A12}"  );
        //Console.WriteLine($"A11 <= A12: {A11 <= A12}"  );
        //Console.WriteLine($"A11 == A12: {A11 == A12}"  );
        //Console.WriteLine($"A11.CompareTo(A12): {A11.CompareTo(A12)}");

        //Console.WriteLine($"___________________________________________");
        //   Console.WriteLine($"___________________________________________");
        //   Console.WriteLine($"___________________________________________");
        //Console.WriteLine($"A11 > A21: {A11 > A21}");
        //Console.WriteLine($"A11 < A21: {A11 < A21}");
        //Console.WriteLine($"A11 == A21: {A11 == A21}");
        //Console.WriteLine($"A11.CompareTo(A12): {A11.CompareTo(A21)}");
        //Console.WriteLine($"___________________________________________");
        //   Console.WriteLine($"___________________________________________");
        //   Console.WriteLine($"___________________________________________");
        //Console.WriteLine($"A11 > B11: {A11 > B11}");
        //Console.WriteLine($"A11 < B11: {A11 < B11}");
        //Console.WriteLine($"A11 == B11: {A11 == B11}");
        //Console.WriteLine($"A11.CompareTo(A12): {A11.CompareTo(B11)}");
        //new AutoRun().Execute(args);
    }
}
