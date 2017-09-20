using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Type
{
    public class CONCAT
    {
        public static void ShowExample()
        {
            string[] arr1 = new string[] { "Martin", "Hromek" };
            string[] arr2 = new string[] { "Eva", "Brezovska" };

            var result = arr1.Concat(arr2);
            List<string> resultAll = result.ToList();

            resultAll.ForEach(a => Console.WriteLine(a));

            Console.ReadLine();
        }
    }
}
