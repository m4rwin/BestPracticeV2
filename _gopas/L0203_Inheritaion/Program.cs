using System;

namespace L0203_Inheritaion
{
    public class Person
    {
        public string FName { set; get; }
        public string LName { set; get; }

        public Person(string f, string l)
        {
            FName = f;
            LName = l;
        }
    }

    public class Employee : Person
    {
        public string Department { set; get; }

        public Employee(string f, string l, string d)
            : base(f, l)
        {
            Department = d;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} - {2}", this.FName, this.LName, this.Department);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee e1 = new Employee("Martin", "Hromek", "C# Programmer");
            Console.WriteLine(e1.ToString());
            Console.ReadLine();
        }
    }
}
