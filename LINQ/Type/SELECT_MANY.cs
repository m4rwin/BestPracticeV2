using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.Type
{
    #region HELP CLASS
    public class PHONENUMBER
    {
        public string Number { get; set; }
        public override string ToString()
        {
            //return base.ToString();
            return Number;
        }
    }

    public class PERSON
    {
        public string Name { set; get; }
        public List<PHONENUMBER> PhoneNumbers { set; get; }
    }
    #endregion

    public class SELECT_MANY
    {
        public static void ShowExample()
        {
            #region DATA
            PERSON p1 = new PERSON() { Name = "Martin Hromek" };
            p1.PhoneNumbers = new List<PHONENUMBER>();
            p1.PhoneNumbers.Add(new PHONENUMBER() { Number = "+420775656055" });

            PERSON p2 = new PERSON() { Name = "Eva Brezovska" };
            p2.PhoneNumbers = new List<PHONENUMBER>();
            p2.PhoneNumbers.Add(new PHONENUMBER() { Number = "+420723195654" });

            List<PERSON> list = new List<PERSON>();
            list.Add(p1);
            list.Add(p2);
            #endregion

            // SELECT
            IEnumerable<IEnumerable<PHONENUMBER>> phoneList = list.Select(p => p.PhoneNumbers);



            // SELECT MANY
            Console.WriteLine("\nSelectMany:");
            Console.WriteLine("Vstupni pole: phone list");
            Console.WriteLine("Spojim [SelectMany] seznam telefonich cisle.");
            IEnumerable<PHONENUMBER> phonesList = list.SelectMany(p => p.PhoneNumbers);
            Console.WriteLine("SelectMany: {0}", string.Join(",", phonesList));
            

            // SELECT MANY WITH ANONYMOUS CLASS
            var dictionary = list.SelectMany(p => p.PhoneNumbers, (parent, child) => new { name = parent.Name, parent.PhoneNumbers });
        }
    }
}
