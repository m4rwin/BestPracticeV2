using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace L1101_XmlSerialization
{
    [Serializable()]
    public class Employee : IDeserializationCallback
    {
        private int Id { set; get; }

        [XmlElement(ElementName = "FName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "LName")]
        public string LastName { get; set; }

        public readonly decimal Salary;
        public Department Department { get; set; }

        [NonSerialized()]
        private DateTime timestamp;
        public DateTime Timestamp
        {
            get { return timestamp; }
            private set { timestamp = value; }
        }

        public Employee()
        {
            Timestamp = DateTime.Now;
        }

        public Employee(int id, string fname, string lname, Department dep)
            :this()
        {
            Id = id;
            FirstName = fname;
            LastName = lname;
            //Salary = salary;
            Department = dep;
        }

        public override string ToString()
        {
            return string.Format("[{5}] {0} {1} {2} {3} {4}", Id, FirstName, LastName, Salary, Department.ToString(), Timestamp);
        }

        public void OnDeserialization(object sender)
        { 
            Timestamp = DateTime.Now;
        }
    }

    [Serializable()]
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Id, Name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Department dep = new Department(506, "IT Heaven");
            Employee e = new Employee(101, "Martin", "Hromek", dep);

            Console.WriteLine("Serializing...");

            //XmlSerialization(e);
            //BinarySerialization(e);
            JsonSerializovat();

            Console.WriteLine("Finish.");
        }

        private static void XmlSerialization(Employee e)
        {
            Console.WriteLine("Original object: {0}", e.ToString());

            using (FileStream stream = new FileStream("c:\\temp\\employee.xml", FileMode.Create, FileAccess.Write))
            {
                XmlSerializer formatter = new XmlSerializer(e.GetType());
                formatter.Serialize(stream, e);
            }

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Deserializing...");
            using (FileStream stream = new FileStream("c:\\temp\\employee.xml", FileMode.Open, FileAccess.Read))
            {
                XmlSerializer formatter = new XmlSerializer(e.GetType());
                Employee e2 = (Employee)formatter.Deserialize(stream);
                Console.WriteLine("Desirialized object: {0}", e2.ToString());
            }
        }

        private static void BinarySerialization(Employee e)
        {
            Console.WriteLine("Original object: {0}", e.ToString());

            using (FileStream stream = new FileStream("c:\\temp\\employee.bin", FileMode.Create, FileAccess.Write))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, e);
            }

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Deserializing...");

            using(FileStream stream = new FileStream("c:\\temp\\employee.bin", FileMode.Open, FileAccess.Read))
            {
                IFormatter formatter = new BinaryFormatter();
                Employee e2 = (Employee)formatter.Deserialize(stream);
                Console.WriteLine("Desirialized object: {0}", e2.ToString());
            }
        }

        private static void JsonSerializovat()
        {
            var e = new
            {
                id = 1,
                FirstName = "Martin",
                LastName = "Hromek",
                Salary = 65000,
                Department = new Department
                {
                    Id = 21,
                    Name = "CAD Team"
                }
            };

            Console.WriteLine("Original object: {0}", e.ToString());

            System.Web.Script.Serialization.JavaScriptSerializer formatter = new System.Web.Script.Serialization.JavaScriptSerializer();
            string JSON = formatter.Serialize(e);
            Console.WriteLine("object: " +JSON);
        }
    }
}
