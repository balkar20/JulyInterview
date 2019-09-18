using System;
using System.Collections.Generic;
using System.Linq;
using Generics.BinaryTrees;
using NUnit.Framework;
using NUnitLite;

class Program
{
    static void Main(string[] args)
    {
        var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var tree = BinaryTree.Create(2, 4, 1, 7, 3, 9, 5, 6, 8);

        foreach (var a in arr)
        {
            foreach (var i in tree)
            {
                Console.WriteLine(i);
            }
        }

        //new AutoRun().Execute(args);
    }
}
