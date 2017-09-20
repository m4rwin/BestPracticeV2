using System;
using System.Linq;

namespace LINQ.Type
{
    public class GROUP_JOIN
    {
        public static void ShowExample()
        {
            DATA data = new DATA();

            // SQL
            var result = from c in data.customerList
                         join o in data.orderList on c.CustomerId equals o.OrderId into g
                         select new
                         {
                             Cus = c.Name, 
                             Grp = g
                         };

            // LAMBDA
            var result2 = data.customerList.GroupJoin(data.orderList,
                customer => customer.CustomerId,
                order => order.OrderId,
                (customer, g) => new
                {
                    Cus = customer.Name,
                    Grp = g.ToList()
                });


            Console.WriteLine("\nGroupJoin:");
            Console.WriteLine("Vstupni pole: customer a order list");
            Console.WriteLine("Spojim [Join] a sloucim do skupiny [Group] podle CustomerId a OrderId.");
            foreach (var item in result2)
            {
                Console.WriteLine("Group: {0} with list: {1}", item.Grp, item.Cus);
            }
        }
    }
}
