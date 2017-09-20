using System;

namespace L0702_AnonymousMethodsAndLambas
{
    class Program
    {
        public delegate void ListArrayDelegate(int[] list);

        public static void ListArray(int[] list)
        {
            foreach (int item in list)
            {
                Console.WriteLine(item);
            }
        }

        public static int Sum(int a, int b) { return a + b; }
        public static int Multiply(int a, int b) { return a * b; }
        public delegate int IntDelegate(int a, int b);

        public static void ShowResult(int a, int b, IntDelegate del)
        {
            Console.WriteLine(del(a, b));
        }

        public static void ShowResult(string a, string b, Func<string, string, string> del)
        {
            Console.WriteLine(del(a, b));
        }

        public static void ShowResult2(DateTime d, int i, Func<DateTime, int, DateTime> dd)
        {
            Console.WriteLine(dd(d, i));
        }

        static void Main(string[] args)
        {
            int[] list = { 8, 5, 98, 12, 35, 14 };


            //ListArrayDelegate d = ListArray;
            ListArrayDelegate d = delegate (int[] arr)
            {
                foreach (int item in arr)
                {
                    Console.WriteLine(item);
                }
            };



            //ListArray(list);
            //d(list);

            
            //int[] ll = Array.FindAll<int>(list, i => i > 30);

            //Array.ForEach<int>(ll, item =>
            //{
            //    Console.WriteLine(item);
            //});

            ShowResult(10, 20, (a, b) => a + b);
            ShowResult(10, 20, (a, b) => a / b);
            ShowResult("Martin", "Hromek", (a, b) => a + " . " + b);
            ShowResult2(DateTime.Now, 3, (a, b) => a.AddDays(b));
        }
    }
}
