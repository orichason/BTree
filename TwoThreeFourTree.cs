using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    public class TwoThreeFourTree<T> where T : IComparable<T>
    {
        public class Node
        {
            public List<T> Keys { get; private set; } = new List<T>();
            public List<Node> Children { get; private set; } = new List<Node>();


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

        public Node Root { get; private set; }

        public int Count { get; private set; }
        public TwoThreeFourTree()
        {
            Root = new();
            Count = 0;
        }

        public Node Search(T value)
        {
            return Search(Root, value);
        }

        private Node Search(Node key, T value)
        {
            if (key == null) return null;

            if (FoundKey(key, value)) return key;

            int direction = GetDirection(key, value);

            if (key.isLeaf)
            {
                return null;
            }

            return Search(key.Children[direction], value); //fix for nodes that dont exist
        }

        private bool FoundKey(Node keys, T key)
        {
            return keys.Keys.Contains(key);
        }
        public bool Insert(T value)
        {

            if (Root.isFull)
            {
                var newRoot = new Node();
                newRoot.Children.Add(Root);
                Root = SplitNode(newRoot, 0, Root);
            }

            else if (!Root.isFull && Root.isLeaf)
            {
                Root.Keys.Add(value);
                Root.Keys.Sort();
                Count++;
                return true;
            }

            int direction = GetDirection(Root, value);

            Insert(Root, Root.Children[direction], value);

            Count++;
            return true;
        }
        private Node Insert(Node parent, Node child, T key)
        {
            if (child.isFull)
            {
                int direction = GetDirection(parent, key);

                var newParent = SplitNode(parent, direction, child);

                if (key.CompareTo(newParent.Keys[direction]) < 0)
                {
                    Insert(newParent, newParent.Children[direction], key);
                }

                else
                {
                    Insert(newParent, newParent.Children[direction + 1], key);
                }

                return newParent;
            }

            int childDirection = GetDirection(child, key);

            if (child.isLeaf)
            {
                child.Keys.Insert(childDirection, key);

                return child;
            }

            else
            {
                Insert(child, child.Children[childDirection], key);
            }

            return parent;
        }

        private Node SplitNode(Node parent, int index, Node nodeToSplit)
        {
            T leftKey = nodeToSplit.Keys[0];
            T middleKey = nodeToSplit.Keys[1];
            T rightKey = nodeToSplit.Keys[2];

            parent.Keys.Insert(index, middleKey);
            parent.Children.RemoveAt(index); //remove nodeToSplit

            var newLeftChild = new Node(leftKey);
            parent.Children.Insert(index, newLeftChild);

            var newRightChild = new Node(rightKey);
            parent.Children.Insert(index + 1, newRightChild);

            if (!nodeToSplit.isLeaf)
            {
                newLeftChild.Children.Add(nodeToSplit.Children[0]);
                newLeftChild.Children.Add(nodeToSplit.Children[1]);

                newRightChild.Children.Add(nodeToSplit.Children[2]);
                newRightChild.Children.Add(nodeToSplit.Children[3]);
            }
            return parent;
        }

        private void SplitRoot(bool firstSplit)
        {
            T leftKey = Root.Keys[0];
            T rightKey = Root.Keys[2];
            T middleKey = Root.Keys[1];

            Node newRoot = new(middleKey);
            Node leftChild = new(leftKey);

            if (firstSplit)
            {
                newRoot.Children.Add(new Node(leftKey));
                newRoot.Children.Add(new Node(rightKey));

                Root = newRoot;
            }
        }

        private int GetDirection(Node node, T key)
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
