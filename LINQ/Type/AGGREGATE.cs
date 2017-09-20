using System;
using System.Linq;

namespace LINQ.Type
{
    public class AGGREGATE
    {
        public static void ShowExample()
        {
            string[] arr = new string[] { "a", "b", "c", "d", "e", "f" };
            var result = arr.Aggregate((x,y) => x+ "-" +y+ "-");
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
