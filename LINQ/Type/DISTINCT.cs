using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Type
{
    public class DISTINCT
    {
        public static void ShowExample()
        {
            string[] arr = new string[]{ "g", "a", "c", "d", "g", "b" };
            string[] abr = new string[] { "KOKO", "JUMBO" };
            var result = arr.Distinct();
            //result.ToList().ForEach(a => Console.WriteLine(a));

            var rr = arr.Union(abr);
            rr.ToList().ForEach(a =>  Console.WriteLine(a));

            Console.ReadLine();
        }
    }
}
