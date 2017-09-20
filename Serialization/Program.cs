using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Human Martin = new Human()
            {
                Genre = "Male",
                Forename = "Martin",
                Surname = "Hromek",
                Age = 29,
                Proffesion = "Developer",
                Partner = new Human() { Genre = "Female", Forename = "Eva", Surname = "Brezovska", Age = 26, Proffesion = "Social Worker" }
            };

            Console.WriteLine(Martin.ToString()+Environment.NewLine);
            Serialization1(Martin);
            //Serialization2(Martin);
            
        }

        public static void Serialization1(Human human)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Human));
            StringBuilder sb = new StringBuilder();

            /* SERIALIZATION */
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, human);
            }
            // XML file
            //Console.WriteLine("SB: " +sb.ToString());
            /* END SERIALIZATION */



            /* DESERIALIZATION */
            Human newMartin = new Human();
            using (StringReader reader = new StringReader(sb.ToString()))
            {
                newMartin = serializer.Deserialize(reader) as Human;
            }
            Console.WriteLine(newMartin.ToString() + Environment.NewLine);
            /* END DESERIALIZATION */
        }

        public static void Serialization2(Human human)
        {
            byte[] bytes = new byte[1024];
            string result = string.Empty;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                // 1. SOAP
                //SoapFormatter formatter = new SoapFormatter();
                //formatter.Serialize(ms, human);

                // 2. Binary
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, human);

                result = Encoding.UTF8.GetString(bytes, 0, (int)ms.Position);
            }

            Console.WriteLine("Human: {0}", human);
            Console.WriteLine("Serializovany do SOAP:\n{0}", result);
        }
    }
}
