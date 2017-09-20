using System;
using System.Linq;

namespace LINQ.Type
{
    public class SELECT
    {
        public static void ShowExample()
        {
            string[] words = { "aPPle", "caRRoT", "MANgo", "KiWi", "GREp", "CITrUS"};
            var result = words.Select(w => new
            {
                origin = w,
                upper = w.ToUpper(),
                lower = w.ToLower()
            });

            Console.WriteLine("\nSelect:");
            Console.WriteLine("Vstupni pole: {0}", string.Join(",", words));
            Console.WriteLine("Prevedu slova na male a na velke pismena.");
            foreach (var item in result)
            {
                Console.WriteLine("origin:{0}, upper:{1}, lower:{2}", item.origin, item.upper, item.lower);
            }
        }
    }
}
