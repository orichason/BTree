namespace BTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TwoThreeFourTree<char> BTree = new();

            BTree.Insert('m');
            BTree.Insert('c');
            BTree.Insert('k');
            BTree.Insert('b');
            BTree.Insert('l');
            BTree.Insert('n');
            BTree.Insert('o');
            BTree.Insert('h');
            BTree.Insert('a');
            BTree.Insert('p');
            BTree.Insert('z');

            TwoThreeFourTree<char>.Node foundO = BTree.Search('x');
            ;
        }
    }
}
