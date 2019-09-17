using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T>
    {
        public int Value { get; set; }
        public BinaryTree<T> Left { get; set; }
        public BinaryTree<T> Right { get; set; }
        public void Add(int val)
        {

        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<Object>.Default);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public class BinaryTree
    {
        private object Elements { get; set; }
        public static BinaryTree<T> Create<T>(params T[] elems)
        {
            return new BinaryTree<T>();
        }
    }
}
