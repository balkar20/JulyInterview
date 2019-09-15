using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Weights
{
    public class Indexer
    {
        public Indexer(double[] arr, int start, int length)
        {
            this.subArr = GetSubArr(arr, start, length);
            this.Length = length;
        }

        private double[] subArr { get; }
        public int Length { get; set; }


        public int this[int index]
        {
            get { return (int)subArr[index]; }
            set { subArr[index] = value; }
        }

        double[] GetSubArr(double[] arr, int start, int length)
        {
            List<double> newArr = new List<double>();
            for (int i = 0; i < length; i++)
            {
                for (int k = 0; k < arr.Length; k++)
                {
                    if (k >= start && k < arr.Length - (length + start))
                    {
                        newArr.Add(arr[k]);
                    }
                }
            }

            var result = newArr.ToArray();
            return result;
        }
    }
}
