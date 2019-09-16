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
            CheckIsValid(arr, start, length);
            this.Arr = arr;
            this.start = start;
            this.SubArr = GetSubArr(arr, start, length);
            this.Length = length;
        }
        private double[] Arr { get; set; }
        private double[] SubArr { get; }
        private int start;
        public int Length { get; set; }


        public int this[int index]
        {
            get { return (int)GetSubArr(this.Arr, this.start, this.Length)[index]; }
            set
            {
                var val = value;
                SubArr[index] = value;
                Arr[index + SubArr.Length - start] = value;
            }
        }

        double[] GetSubArr(double[] arr, int start, int length)
        {
            List<double> newArr = new List<double>();
                for (int i = 0; i < arr.Length; i++)
                {
                    int u = arr.Length - length + start;
                    bool b = i < u;
                    if (i >= start && b)
                    {
                        newArr.Add(arr[i]);
                    }
                }

            var result = newArr.ToArray();
            return result;
        }

        void CheckIsValid(double[] arr, int start, int length)
        {
            if (length != 0)
            {
                bool a = start < 0;
            bool b = start > arr.Length - 1;
            bool c = length > arr.Length;
            bool d = length  > arr.Length - start;
            if (start < 0 ||
                start > arr.Length - 1 || 
                length > arr.Length || 
                length > arr.Length - start ||
                length < 0)
            {
                throw new ArgumentException();
            }
            }
            
        }
    }
}
