using System.Collections.Generic;
using System.Linq;
using Incapsulation.Weights;
using NUnitLite;

class Program
{
    static void Main(string[] args)
    {
        double[] range1to4 = { 1, 2, 3, 4 };
        Indexer indexer = new Indexer(range1to4, 1, 2);
        new AutoRun().Execute(args);
    }
}
