using System;

namespace L0501_Delegates
{
    class Program
    {
        public delegate int MyDelegate1(int a, int b);
        public delegate void MyNewDelegate();

        public static int Sum(int a, int b) { return a + b; }
        public static int Max(int a, int b) { return (a > b) ? a : b; }
        public static void DoSomething() { Console.WriteLine("Do Something."); }
        public static void DoSomethingElse() { Console.WriteLine("Do Something Else."); }

        static void Main(string[] args)
        {
            //MyDelegate1 d = new MyDelegate1(Sum);
            MyDelegate1 d = Max;
            int i = d(5, 9);
            int max = d(6, 98);

            MyNewDelegate nd;
            nd = DoSomething;
            nd += DoSomethingElse;

            //nd();
            // or
            // |
            // |
            // V
            foreach (MyNewDelegate item in nd.GetInvocationList())
            {
                item();
            }

            Console.ReadLine();
        }
    }
}
