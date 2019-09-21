using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.TreeTraversal
{
    public class Traversal
    {
        public static IEnumerable<T> GetBinaryTreeValues<T>(BinaryTree<T> data)
        {
            T root = data.Value;
            return new List<T>();
        }

        public static IEnumerable<T> GetEndJobs<T>(T data)
        {
            return new List<T>();
        }

        public static IEnumerable<Product> GetProducts<T>(T data) //where T : ProductCategory
        {
            return new List<Product>();
        }

        private static IEnumerable<T> GetTreeValues<T, TIntermediate>(T data, Func<T,TIntermediate> func)
        {
            var result = new List<T>();
            T root = data;
            return result;
        }
    }
}
