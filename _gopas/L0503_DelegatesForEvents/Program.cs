using System;
using System.Collections.Generic;

namespace L0503_DelegatesForEvents
{
    public delegate void ChangedEventHandler(object sender, MyArgs e);

    public enum ChangedStatus
    {
        Added,
        Changed, 
        Cleared
    }

    public class MyArgs : EventArgs
    {
        public ChangedStatus Status { get; set; }

        public MyArgs(ChangedStatus s)
        {
            Status = s;
        }
    }

    class MyList : List<int>
    {
        public event ChangedEventHandler Changed;

        protected virtual void OnChanged(MyArgs e)
        {
            if (Changed != null) Changed(this, e);
        }

        new public void Add(int item)
        {
            base.Add(item);
            OnChanged(new MyArgs(ChangedStatus.Added));
        }

        new public void Clear()
        {
            base.Clear();
            OnChanged(new MyArgs(ChangedStatus.Cleared));
        }

        new public int this[int index]
        {
            set
            {
                base[index] = value;
                OnChanged(new MyArgs(ChangedStatus.Changed));
            }
        }
    }

    class Program
    {
        private static void EventListener(object sender, EventArgs e)
        {
            MyList tmpList = sender as MyList;
            if (tmpList == null) return;

            Console.WriteLine();
            Console.WriteLine("Items:");
            tmpList.ForEach((item) => Console.WriteLine("\t" +item));
        }

        private static void EventListener2(object sender, MyArgs e)
        {
            Console.WriteLine("Status: {0}", e.Status);
        }

        static void Main(string[] args)
        {
            MyList myList = new MyList();
            
            myList.Add(55);
            myList.Add(666);
            myList.Changed += EventListener;
            myList.Changed += EventListener2;
            myList.Add(123);
            myList.Add(13);
            //myList.Clear();
            myList[1] = -666;

            Console.ReadLine();
        }

        
    }
}
