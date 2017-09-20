using System;

namespace L0605_ExtensionMethods
{
    public static class MyEx
    {
        public static string ToSentence(this string value)
        {
            value = value[0].ToString().ToUpper() + value.Substring(1);
            if (value[value.Length - 1] != '.')
                value += '.';

            return value;
        }

        public static string ToSentence(this string value, bool MakeDot)
        {
            value = value[0].ToString().ToUpper() + value.Substring(1);
            if (value[value.Length - 1] != '.' && MakeDot)
                value += '.';

            return value;
        }

        public static string ToString(this Employee e, bool ShowId)
        {
            if (ShowId)
                return e.ToString();
            else
                return string.Format("{0} {1}", e.FirstName, e.LastName);
        }

        // pujde prelozit a spusti, ale
        // nelze zastinit originalni metodu tridy Employee
        public static string ToString(this Employee e)
        {
            return "This is extended method.";
        }
    }

    public class Employee
    {
        private int Id { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Employee(int id, string fName, string lName)
        {
            Id = id;
            FirstName = fName;
            LastName = lName;
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1} {2}", Id, FirstName, LastName);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Write a sentense: ");
            //string sentence = Console.ReadLine();
            //Console.WriteLine();
            //Console.WriteLine("The sentence is: {0}", sentence.ToSentence(false));

            Employee e = new Employee(55, "Martin", "Hromek");
            Console.WriteLine(e.ToString(false));
        }
    }
}
