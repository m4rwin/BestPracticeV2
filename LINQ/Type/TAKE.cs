using System;
using System.Linq;

namespace LINQ.Type
{
    public class TAKE
    {
        public static void ShowExample()
        {
            string[] numbers = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "99 luft ballon" };
            var result = numbers.Where((n) => n.Length > 4).Take(3);

            Console.WriteLine("\nTake:");
            Console.WriteLine("Vstupni pole: {0}", string.Join(",", numbers));
            Console.WriteLine("Vyberu slova delsi nez 4 znaky a beru pouze prvni 3 prvky.");
            foreach (var item in result)
            {
                Console.WriteLine("slova: {0}", item);
            }
        }
    }
}
