using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace Generics.BinaryTrees
{
    public class BinaryTree<T>
        : IEnumerable<T> where T : IComparable
    {
        public T Value { get; set; }
        public BinaryTree<T> Left { get; set; }
        public BinaryTree<T> Right { get; set; }

        private bool isEmpty;

        public BinaryTree(T value)
        {
            Value = value;
        }
            
        public BinaryTree()
        {
            isEmpty = true;
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
            if (isEmpty)
            {
                Value = value;
                isEmpty = !isEmpty;
            }
            else
            {
                int compare = value.CompareTo(Value);
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
            if (isEmpty)
            {
                return Empty<T>().GetEnumerator();
            }
            return Enumerate(Left)
                .Concat(Repeat(Value, 1))
                .Concat(Enumerate(Right))
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

    public class BinaryTree
    {
        public static BinaryTree<T> Create<T>(params T[] elems) where T : IComparable
        {
            return new BinaryTree<T>(elems);
        }
    }
}
