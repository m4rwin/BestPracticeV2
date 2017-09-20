using System;
using System.Linq;

namespace UserAttribute
{
    [ClassMetaData("Martin Hromek")]
    [ClassMetaData("Eva Brezovska")]
    public class MyTestClass
    { }

    public class Program
    {
        static void Main(string[] args)
        {
            ClassMetaDataAttribute[] attributes = (ClassMetaDataAttribute[])(typeof(MyTestClass))
            .GetCustomAttributes(typeof(ClassMetaDataAttribute), true);

            string result = attributes.Aggregate("", (output, next) => (output.Length > 0) ? (output + ", " + next.Author) : next.Author);

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
