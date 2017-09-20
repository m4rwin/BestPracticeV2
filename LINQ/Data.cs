using System.Collections.Generic;

namespace LINQ
{
    public class DATA
    {
        public List<CUSTOMER> customerList = new List<CUSTOMER>();
        public List<ORDER> orderList = new List<ORDER>();

        public DATA()
        {
            
            customerList.Add(new CUSTOMER() { CustomerId = 1, Name = "Dell" });
            customerList.Add(new CUSTOMER() { CustomerId = 2, Name = "Microsoft" });
            customerList.Add(new CUSTOMER() { CustomerId = 3, Name = "Hitachi" });

            orderList.Add(new ORDER() { OrderId = 1, OrderName = "keyboard" });
            orderList.Add(new ORDER() { OrderId = 2, OrderName = "headphone" });
            orderList.Add(new ORDER() { OrderId = 3, OrderName = "tamagochi" });
            orderList.Add(new ORDER() { OrderId = 1, OrderName = "chair" });
        }
    }

    public class CUSTOMER
    {
        public int CustomerId { set; get; }
        public string Name { set; get; }
    }

    public class ORDER
    {
        public int OrderId { set; get; }
        public string OrderName { set; get; }
        public override string ToString()
        {
            //return base.ToString();
            return OrderName;
        }
    }
}
