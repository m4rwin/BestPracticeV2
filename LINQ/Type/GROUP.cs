using System;
using System.Linq;

namespace LINQ.Type
{
    public class GROUP
    {
        public static void ShowExample()
        {
            DATA data = new DATA();

            // SQL
            var result = from o in data.orderList
                         group o by o.OrderId into g
                         select new { ID = g.Key, List = g.ToList() };

            // LAMBDA
            var result2 = data.orderList.GroupBy(o => o.OrderId,
                (key, g) => new
                {
                    ID = key,
                    List = g.ToList()
                });


            Console.WriteLine("\nGroup:");
            Console.WriteLine("Vstupni pole: order list");
            Console.WriteLine("Sloucim do skupiny [GroupBy] podle OrderId.");
            foreach (var item in result2)
            {
                Console.WriteLine("Key: {0} with list: {1}", item.ID, string.Join(",",  item.List));
            }
        }
    }
}
