using System;
using System.Linq;

namespace LINQ.Type
{
    public class JOIN
    {
        public static void ShowExample()
        {
            DATA data = new DATA();

            // SQL
            var result = from c in data.customerList
                         join o in data.orderList on c.CustomerId equals o.OrderId
                         select new { c.Name, o.OrderName };

            // LAMBDA
            var result2 = data.customerList.Join(data.orderList,
                c => c.CustomerId,
                o => o.OrderId,
                (c, o) => new 
                { 
                    c.Name, 
                    o.OrderName
                });


            Console.WriteLine("\nJoin:");
            Console.WriteLine("Vstupni pole: customer list a order list");
            Console.WriteLine("Spojim [Join]  podle CustomerId a OrderId.");
            foreach (var group in result)
            {
                Console.WriteLine("Customer: {0} bought {1}", group.Name, group.OrderName);
            }
        }
    }
}
