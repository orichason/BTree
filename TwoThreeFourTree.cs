using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    internal class TwoThreeFourTree<T> where T : IComparable<T>
    {
        internal class Node<T>
        {
            public List<T> Keys { get; private set; } = new List<T>();
            public List<Node<T>> Children { get; private set; } = new List<Node<T>>;


            public Node()
            {

            }

            public Node(T key)
            {
                Keys.Add(key);
            }
            public int Count => Keys.Count;

            public bool isFull => Keys.Count == 3;
        }

        public Node<T> Head { get; private set; }

        int count;
        public TwoThreeFourTree()
        {
            count = 0;
        }

        public void Add(T item)
        {
            if (count == 0)
            {
                Head = new Node<T>(item);
            }

            bool inserted = false;
            var current = Head;

            while (!inserted)
            {
                int i = 0;
                if (item.CompareTo(current.Keys[i]) < 0)
                {
                    //keep going here
                }
            }
        }

    }
}
