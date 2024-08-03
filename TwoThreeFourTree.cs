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

                Root = SplitNode(Root);
                Insert(Root, key);
            }

            else
            {
                Insert(Root, key);
            }
        }

        private void SplitChildNode(Node<T> parent, Node<T> child)
        {
            parent.Keys.Add(child.Keys[1]);
            child.Keys.Remove(child.Keys[1]);
        }

        private void Insert(Node<T> parent, Node<T> child, T key)
        {
            if (child.isLeaf)
            {
                child.Keys.Add(key);
                child.Keys.Sort();
            }

            else
            {
                if (child.isFull)
                {
                    int direction = GetDirection(parent, key);
                    var newParent = SplitNode(parent, direction, child);

                    if (key.CompareTo(newParent.Keys[direction]) < 0)
                    {
                        Insert(SplitNode(parent, direction, child), newParent.Children[direction], key);
                    }

                    else
                    {
                        Insert(SplitNode(parent, direction, child), newParent.Children[direction + 1], key);
                    }

                    //finished here. Need to test splitting as well as base case for recursion
                }
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

        private Node<T> SplitNode(Node<T> parent, int index, Node<T> nodeToSplit)
        {
            T rightKey = nodeToSplit.Keys[2];
            T middleKey = nodeToSplit.Keys[1];

            parent.Keys.Insert(index, middleKey);
            parent.Children.Insert(index + 1, new Node<T>(rightKey));

            return parent;
        }

        private int GetDirection(Node<T> node, T key)
        {
            for (int i = 0; i < node.Count; i++)
            {
                if (key.CompareTo(node.Keys[i]) < 0)
                {
                    return i;
                }
            }

            return node.Count;
        }
    }
}
