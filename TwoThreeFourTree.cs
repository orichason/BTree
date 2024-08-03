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
            public List<Node<T>> Children { get; private set; } = new List<Node<T>>();


            public Node()
            {

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
        bool rootSplit;
        public TwoThreeFourTree()
        {
            Root = new();
            rootSplit = false;
            count = 0;
        }

        public void Insert(T key)
        {
            Node<T> current = Root;
            if (Root.isFull)
            {
                //split  root and make new root
                SplitRoot();
                rootSplit = true;
            }

            if (rootSplit)
            {
                int direction = GetDirection(Root, key);
                Insert(Root, Root.Children[direction], key);
            }

            else
            {
                Insert(Root, current, key);

                current.Keys.Add(key);
                current.Keys.Sort();

            }
        }
        private Node<T> Insert(Node<T> parent, Node<T> child, T key)
        {
            if (child.Keys.Count == 0) return child;

            //if (child.isLeaf && rootSplit == false)
            //{
            //    child.Keys.Add(key);
            //    child.Keys.Sort();
            //}


            int direction = GetDirection(parent, key);

            if (child.isFull)
            {
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

            else
            {
                Insert(parent, parent.Children[direction], key);
            }


            return child;
        }

        private Node<T> SplitNode(Node<T> parent, int index, Node<T> nodeToSplit)
        {
            T rightKey = nodeToSplit.Keys[2];
            T middleKey = nodeToSplit.Keys[1];

            parent.Keys.Insert(index, middleKey);
            parent.Children.Insert(index + 1, new Node<T>(rightKey));

            return parent;
        }

        private void SplitRoot()
        {
            T leftKey = Root.Keys[0];
            T rightKey = Root.Keys[2];
            T middleKey = Root.Keys[1];

            Node<T> newRoot = new(middleKey);
            newRoot.Children.Add(new Node<T>(leftKey));
            newRoot.Children.Add(new Node<T>(rightKey));

            Root = newRoot;
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
