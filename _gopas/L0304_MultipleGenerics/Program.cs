using System;
using MyNode = L0304_MultipleGenerics.Node<int, string>;
using MyList = L0304_MultipleGenerics.LinkedList<int, string>;

namespace L0304_MultipleGenerics
{
    public class Node<K, T>
    {
        public K Key { set; get; }
        public T Value { set; get; }
        public Node<K,T> NextNode { set; get; }

        public Node()
        {
            Key = default(K);
            Value = default(T);
            NextNode = null;
        }

        public Node(K k, T v, Node<K,T> next)
        {
            Key = k;
            Value = v;
            NextNode = next;
        }
    }

    public class LinkedList<K, T> where K : IComparable
    {
        private Node<K,T> LastNode { set; get; }
        private Node<K,T> RootNode { set; get; }

        public LinkedList()
        {
            LastNode = new Node<K, T>();
            RootNode = LastNode;
        }

        public void AddNode(K key, T value)
        {
            Node<K, T> newItem = new Node<K, T>(key, value, null);
            LastNode.NextNode = newItem;
            LastNode = newItem;
        }

        public Node<K,T> GetRoot()
        {
            return RootNode.NextNode;
        }

        public  T Find(K key)
        {
            Node<K, T> current = RootNode;
            while(current.NextNode != null)
            {
                //if (current.Key == key)
                if(  ((IComparable)(current.Key)).CompareTo(key) == 0)
                    return current.Value;
                else
                    current = current.NextNode;
            }
            return default(T);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //MyNode root = new MyNode(5, "root", null);
            //MyNode child1 = new MyNode(3, "child1", root);
            //MyNode child2 = new MyNode(1, "child2", child1);

            //MyNode tmp = child2;
            //while(tmp != null)
            //{
            //    Console.WriteLine(tmp.Key +" - " +tmp.Value);
            //    tmp = tmp.NextNode;
            //}

            MyList list = new MyList();
            list.AddNode(1, "root");
            list.AddNode(2, "child1");
            list.AddNode(3, "child2");

            MyNode node = list.GetRoot();
            while(node != null)
            {
                Console.WriteLine(node.Key +" : "+ node.Value);
                node = node.NextNode;
            }
            Console.WriteLine("Find number 2: {0}", list.Find(2));

            Console.ReadLine();
        }
    }
}
