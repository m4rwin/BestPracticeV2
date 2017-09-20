using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Type
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}, {2}", this.FirstName, this.LastName, this.Age);
        }
    }

    public class LOOKUP
    {
        static List<Person> list = new List<Person>()
        {
            new Person() { FirstName = "Martin", LastName = "Hromek", Age=29 },
            new Person() { FirstName = "Eva", LastName = "Brezovska", Age = 25 },
            new Person() { FirstName = "Martin", LastName = "Hrozny", Age = 14 }
        };

        internal static void ShowExample()
        {
            ILookup<string, Person> MyFirstLookUp = list.ToLookup(item => item.FirstName.Substring(0, 3) + item.LastName.Substring(0, 3));


            foreach (Person person in MyFirstLookUp["MarHro"])
            {
                Console.WriteLine("name: " + person);
            }
        }
    }
}
