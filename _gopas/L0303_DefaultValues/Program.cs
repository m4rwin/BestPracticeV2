using System;

namespace L0303_DefaultValues
{
    class MyClass<T>
    {
        private T Item { set; get; }

        public MyClass()
        {
            Item = default(T);
        }

        public T GetValue()
        {
            return Item;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass<int> mc = new MyClass<int>();
            Console.WriteLine("my value: {0}", mc.GetValue());
            Console.ReadLine();
        }
    }
}
