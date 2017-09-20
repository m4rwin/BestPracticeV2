using System;
using System.Text;

namespace L0603_NullableTypes
{
    class Program
    {
        private static StringBuilder MakeStringBuilder(string s)
        {
            if (!string.IsNullOrEmpty(s))
                return new StringBuilder();
            else
                return null;
        }

        static void Main(string[] args)
        {
            int? i = null;

            // variant 1
            //i = i.GetValueOrDefault(-1) + 1;

            // variant 2
            //i = i + 1 ?? -1;

            //if(i.HasValue)
            //    Console.WriteLine("i = {0}", i.Value);
            //else
            //    Console.WriteLine("i = null");

            StringBuilder sb = MakeStringBuilder("");
            int? length = sb?.Length ?? 0;
            Console.WriteLine("length = {0}", length);
        }
    }
}
