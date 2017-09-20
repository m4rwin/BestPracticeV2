using System;
using System.Linq;

namespace LINQ.Type
{
    public class WHERE
    {
        public static void ShowExample()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortDigits = digits.Where((digit, index) => digit.Length < index);

            Console.WriteLine("\nWhere:");
            Console.WriteLine("Vstupni pole: {0}", string.Join(",", digits));
            Console.WriteLine("Vyhledam pouze slova jejich delka slova je vetsi nez jejich index v poli.");
            foreach (var d in shortDigits)
            {
                Console.Write("{0},", d);
            }
            Console.WriteLine();
        }
    }
}
