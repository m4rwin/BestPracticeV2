using System;
using System.Linq;

namespace LINQ.Type
{
    public class ORDER_BY
    {
        public static void ShowExample()
        {
            DATA data = new DATA();

            // SQL
            var result = from o in data.orderList
                         orderby o.OrderName
                         select new { ID = o.OrderId, Name = o.OrderName };

            // LAMBDA
            /*
            var result2 = data.orderList.OrderBy(o => o.OrderName, () => new
            {
                ID = o.
                Name = o.
            });
            */    

            Console.WriteLine("\nOrder by:");
            Console.WriteLine("Vstupni pole: order list");
            Console.WriteLine("Seradim pole [OrderBy] podle OrderId.");
            foreach (var item in result)
            {
                Console.WriteLine("Id: {0}, name: {1}", item.ID, item.Name);
            }
        }
    }
}
