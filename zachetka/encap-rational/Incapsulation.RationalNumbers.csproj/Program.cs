using System;
using System.Collections.Generic;
using System.Linq;
using Incapsulation.RationalNumbers;
using NUnitLite;

class Program
{
	static void Main(string[] args)
	{
        //double rational = new Rational(1, 2);
        //Console.WriteLine(rational);
		//new AutoRun().Execute(args);
	    int a = gcd(2, 4);
	    Console.WriteLine(a);
    }

    static int gcd(int a, int b)
    {
        //find the gcd using the Euclid’s algorithm
        while (a != b)
            if (a < b) b = b - a;
            else a = a - b;
        //since at this point a=b, the gcd can be either of them
        //it is necessary to pass the gcd to the main function
        return (a);
    }
}
