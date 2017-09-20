using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Type
{
    public class CONTAIN
    {
        public static void ShowExample()
        {
            string[] arr = new string[] { "Martin", "Hromek" };

            var result = arr.Contains("MarTIN", StringComparer.InvariantCultureIgnoreCase);

            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
