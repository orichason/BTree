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
        bool rootSplit;
        public TwoThreeFourTree()
        {
            Root = new();
            rootSplit = false;
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

            return Search(key.Children[direction], value);
        }

        private bool FoundKey(Node keys, T key)
        {
            return keys.Keys.Contains(key);
        }
        public bool Insert(T value)
        {
            if (Root.isFull)
            {
                if (rootSplit)
                {
                    SplitRoot(false);
                }

                else
                {
                    SplitRoot(true);
                    rootSplit = true;
                }
            }

            if (!rootSplit)
            {
                Root.Keys.Add(value);
                Root.Keys.Sort();
            }

            else
            {
                int direction = GetDirection(Root, value);
                Insert(Root, Root.Children[direction], value);

            }
            Count++;
            return true;
        }
        private Node Insert(Node parent, Node child, T key)
        {

            if (child.isLeaf)
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
                }

                else
                {
                    child.Keys.Add(key);
                    child.Keys.Sort();

                    return child;
                }
            }


            else
            {
                int direction = GetDirection(parent, key);

                parent = parent.Children[direction];

                Insert(parent, parent.Children[direction], key);
            }

            return parent;
        }

        private Node SplitNode(Node parent, int index, Node nodeToSplit)
        {
            T rightKey = nodeToSplit.Keys[2];
            T middleKey = nodeToSplit.Keys[1];

            parent.Keys.Insert(index, middleKey);
            nodeToSplit.Keys.Remove(middleKey);
            nodeToSplit.Keys.Remove(rightKey);

            parent.Children.Insert(index + 1, new Node(rightKey));

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

            else
            {
                newRoot.Children.Add(leftChild);
                leftChild.Children.Add(Root.Children[0]);
                leftChild.Children.Add(Root.Children[1]);

                Node rightChild = new(rightKey);
                newRoot.Children.Add(rightChild);
                rightChild.Children.Add(Root.Children[2]);
                rightChild.Children.Add(Root.Children[3]);

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
