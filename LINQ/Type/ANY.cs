using System;
using System.Linq;

namespace LINQ.Type
{
    public class ANY
    {
        public static void ShowExample()
        {
            string[] arr = new string[] { "Tony", "Martin", "Alesh", "Kurtis", "Marie", "Dita" };
            var result = arr.Any(x => x.StartsWith("M"));
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
