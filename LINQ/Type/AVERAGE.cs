using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Type
{
    public class AVERAGE
    {
        public static void ShowExample()
        {            
            int[] arr = new int[] { 2,4,2,4 };
            var result = arr.Average();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
