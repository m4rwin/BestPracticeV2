using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Type
{
    public class ALL
    {
        public static void ShowExample()
        {
            string[] arr = new string[] { "Tony", "Martin", "Alesh", "Kurtis", "Marie", "Dita" };
            var result = arr.All( x => x.StartsWith("M"));
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
