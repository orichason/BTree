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
            public List<T> Keys { get; private set; }
            public List<Node<T>> Children { get; private set; }


            public Node()
            {
                Keys = new List<T>();
                Children = new List<Node<T>>();
            }

            public Node(T key)
            {
                Keys.Add(key);
            }
            public int Count => Keys.Count;

            public bool isFull => Keys.Count == 3;

            public bool isLeaf => Children.Count == 0;
        }

        public Node<T> Root { get; private set; }

        int count;
        public TwoThreeFourTree()
        {
            count = 0;
        }

        public void Insert(T key)
        {
            if (Root.isFull)
            {
                //split  root and make new root

                Node<T> newRoot = new();
                
            }

            
        }

        private void SplitChildNode(Node<T> parent, Node<T> child)
        {
            parent.Keys.Add(child.Keys[1]);
            child.Keys.Remove(child.Keys[1]);
        }

        private void Insert(Node<T> node, T key)
        {
            if (node.isLeaf)
            {
                node.Keys.Add(key);
                node.Keys.Sort();
            }

            else
            {

            }
        }


        private void AddToNode(Node<T> node, T key)
        {
            node.Keys.Add(key);
        }

        //private void SortNode(ref Node<T> node)
        //{
        //    //Bubble sort

        //    for (int i = 0; i < node.Count; i++)
        //    {
        //        for (int j = i + 1; j < node.Count; j++)
        //        {
        //            if (node.Keys[i].CompareTo(node.Keys[j]) > 0)
        //            {
        //                T temp = node.Keys[i];
        //                node.Keys[i] = node.Keys[j];
        //                node.Keys[j] = temp;
        //            }
        //        }
        //    }
        //}

        private Node<T> SplitNode(Node<T> node)
        {
            Node<T> parent = new(node.Keys[1]);
            Node<T> leftChild = new(node.Keys[0]);
            Node<T> rightChild = new(node.Keys[2]);

            parent.Children.Add(leftChild);
            parent.Children.Add(rightChild);

            return parent;
        }

        
    }
}
