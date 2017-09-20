using System;

namespace L0201_CLSCompliant
{
    [System.CLSCompliant(false)]
    public class Program
    {
        static void Main(string[] args)
        {
            uint a = 1;
            Console.WriteLine(a);
            Console.ReadLine();
        }

        public static ulong DoSomething()
        {
            uint a = 1;
            return a;
        }
    }
}
