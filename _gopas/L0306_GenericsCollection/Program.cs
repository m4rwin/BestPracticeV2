using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

namespace L0306_GenericsCollection
{
    class MyId
    {
        public string ID { set; get; }
        public int Code { set; get; }

        public MyId(string id, int c) { ID = id; Code = c; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", ID, Code);
        }

        public override bool Equals(object obj)
        {
            MyId tmp = obj as MyId;
            if (tmp == null) return false;
            if ((tmp.ID == this.ID) && (tmp.Code == this.Code)) return true;
            return false;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Method_Array();
            //Method_ArrayList();
            //Method_List();
            //Method_Queue();
            //Method_Stack();
            //Method_Dictionary();
            Method_CustomDictionary();

            watch.Stop();
            Console.WriteLine("time: {0} ms.", watch.ElapsedMilliseconds);
            Console.ReadLine();
        }

        private static void Method_Array()
        {
            int[] list = new int[1];
            for (int i = 0; i < 100000; i++)
            {
                Array.Resize<int>(ref list, list.Length + 1);
            }
        }

        private static void Method_ArrayList()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(0);
            }
        }

        private static void Method_List()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(0);
            }
        }

        private static void Method_Queue()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("B");
            queue.Enqueue("E");
            queue.Enqueue("H");

            Console.WriteLine(queue.Dequeue()); // vyzvednuti prvku
            Console.WriteLine(queue.Dequeue()); // vyzvednuti prvku
            Console.WriteLine(queue.Peek()); // nahlednuti na nasledujici prvek bez vyzvednuti
        }

        private static void Method_Stack()
        {
            Stack<string> stack = new Stack<string>();
            stack.Push("Kamil");
            stack.Push("Petr");
            stack.Push("Radim");
            stack.Push("Tomas");
            stack.Push("Jose");

            Console.WriteLine(stack.Pop()); // vyzvednuti prvku
            Console.WriteLine(stack.Peek()); // nahlednuti na nasledujici prvek bez vyzvednuti
        }

        private static void Method_Dictionary()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("do", "delat");
            dic.Add("go", "jit");
            dic.Add("wait", "cekat");
            dic.Add("stand", "stat");

            //Console.WriteLine(dic["go"]);

            foreach (KeyValuePair<string, string> item in dic)
            {
                Console.WriteLine("{0} -> {1}", item.Key, item.Value);
            }
        }

        private static void Method_CustomDictionary()
        {
            Dictionary<MyId, string> dic = new Dictionary<MyId, string>();
            dic.Add(new MyId("id1", 1), "delat");
            dic.Add(new MyId("id2", 2), "jit");
            dic.Add(new MyId("id3", 3), "cekat");
            dic.Add(new MyId("id4", 4), "stat");

            Console.WriteLine(dic[new MyId("id3", 3)]);
        }
    }
}
