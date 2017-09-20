using System;

namespace L0203_SystemObject
{
    public class Employee : System.Object
    {
        public string FName;
        public string LName;

        public override string ToString()
        {
            return string.Format("{0} {1}", this.FName, this.LName);
        }

        public override bool Equals(object obj)
        {
            Employee tmp = obj as Employee;
            if(tmp == null) { return false; }
            return ((this.FName == tmp.FName) && (this.LName == tmp.LName));
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee e1 = new Employee() { FName = "Eva", LName = "Brezovska" };
            Employee e2 = new Employee() { FName = "Martin", LName = "Hromek" };
            Console.WriteLine("hash: " +e1.GetHashCode());
            Console.WriteLine("hash: " + e2.GetHashCode());
            Console.WriteLine("RefEql: " +object.ReferenceEquals(e1, e2));
            Console.WriteLine("Eql: " +e1.Equals(e2));
            Console.ReadLine();
        }
    }
}
