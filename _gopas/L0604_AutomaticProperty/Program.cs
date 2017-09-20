using System;

namespace L0604_AutomaticProperty
{
    class Person
    {
        public virtual string FirstName { get; set; } = "Tonda";
        public virtual string LastName  { get; set; }
    }

    class Employee : Person
    {
        public override string LastName
        {
            get
            {
                return base.LastName.ToUpper();
            }

            set
            {
                base.LastName = value;
            }
        }

        public override string FirstName
        {
            get
            {
                return base.FirstName;
            }

            set
            {
                base.FirstName = value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person();
            p.FirstName = "Karel";
            p.LastName = "Sykora";
            Console.WriteLine("{0} {1}", p.FirstName, p.LastName);

            var p2 = new Person();
            p2.LastName = "Sykora";
            Console.WriteLine("{0} {1}", p2.FirstName, p2.LastName);
        }
    }
}
