using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.BinaryTrees
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using static System.Linq.Enumerable;


    public class BinaryTree<T>
        : IEnumerable<T> where T : IComparable
    {
        //private  T _value;
        //private BinaryTree<T> _left, _right;

        public T Value { get; set; }
        public BinaryTree<T> Left { get; set; }
        public BinaryTree<T> Right { get; set; }

        private bool isEmpy;

        public BinaryTree(T value)
        {
            Value = value;
        }
            
        public BinaryTree()
        {
            isEmpy = true;
        }

        public BinaryTree(IEnumerable<T> values)
        {
            using (var enumerator = values.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentException("The collection is empty.");
                }

                Value = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    Add(enumerator.Current);
                }
            }
        }

        public BinaryTree<T> Add(T value)
        {
            if (isEmpy)
            {
                Value = value;
                isEmpy = !isEmpy;
            }
            else
            {
                int compare = value.CompareTo(this.Value);
                if (compare == 0)
                {
                    Left = Add(value, Left);
                }
                if (compare < 0)
                {
                    Left = Add(value, Left);
                }
                else if (compare > 0)
                {
                    Right = Add(value, Right);
                }
            }
            
            return this;
        }

        private static BinaryTree<T> Add(T value, BinaryTree<T> tree)
            => tree?.Add(value) ?? new BinaryTree<T>(value);

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerate(this.Left)
                .Concat(Repeat(this.Value, 1))
                .Concat(Enumerate(this.Right))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static IEnumerable<T> Enumerate(BinaryTree<T> tree)
        {
            if (tree == null)
            {
                yield break;
            }

            foreach (var x in tree)
            {
                yield return x;
            }
        }
    }
    //public class BinaryTree<T> : IEnumerable<T> where  T : IComparable
    //{

    //    public BinaryTree()
    //    {

    //    }

    //    public T Value { get; set; }
    //    public BinaryTree<T> Left { get; set; }
    //    public BinaryTree<T> Right { get; set; }

    //    public void Add(T val)
    //    {

    //    }

    //    private T[] GetOrderedArray()
    //    {
    //        T[] result = new T[] { };
    //        //if (this.Current.Parent == null && this.Left == null && this.Right == null)
    //        //{
    //        //    result = new T[] { };
    //        //}

    //        return result;
    //    }



    //    public override int GetHashCode()
    //    {
    //        return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<Object>.Default);
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    //IEnumerator IEnumerable.GetEnumerator()
    //    //{
    //    //    return null;
    //    //}

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return new BinaryTreeEnumerator(this);
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        T[] val = (T[])obj;
    //        if (GetOrderedArray().Equals(Value))
    //        {
    //            return true;
    //        }

    //        return false;
    //    }

    //    class BinaryTreeEnumerator : IEnumerator<T>
    //    {
    //        BinaryTree<T> current;

    //        public BinaryTreeEnumerator(BinaryTree<T> tree)
    //        {
    //            current = tree;
    //        }

    //        /// <summary>
    //        /// The MoveNext function traverses the tree in sorted order.
    //        /// </summary>
    //        /// <returns>True if we found a valid entry, False if we have reached the end</returns>
    //        public bool MoveNext()
    //        {
    //            bool result = true;
    //            if (current.Left == null)
    //            {
    //                return false;
    //            }

    //            return result;
    //        }

    //        public T Current { get; }

    //        object IEnumerator.Current { get; }

    //        public void Dispose() { }
    //        public void Reset() { current = null; }
    //    }
    //}

    public class BinaryTree
    {
        private object Elements { get; set; }
        public static BinaryTree<T> Create<T>(params T[] elems) where T : IComparable
        {
            return new BinaryTree<T>();
        }
    }
}
