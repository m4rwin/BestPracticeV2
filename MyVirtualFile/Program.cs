using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MyVirtualFile
{
    [Serializable]
    class Animal
    {
        public string Name { get; set; } = "Jezevec obecny";

        public Animal() { }
    }
    class Program
    {
        private static string OriginalFile { get; } = "todo.txt";
        private static string FileString { get; } = "Ahoj jmenuji se Al3sh a jsem profesionalni e-sportovec.";
        private static MemoryMappedFile MappedFile { get; set; }

        static void Main(string[] args)
        {
            //Console.WriteLine( Save2Memory() );
            string file = CreateTemporaryFile();
            ReadFile(file);

            Console.ReadLine();
        }

        public static void ReadFile(string fileName)
        {
            try
            {
                var st = File.OpenRead(fileName);
                string content = new StreamReader(st, Encoding.UTF8).ReadToEnd();

                Console.WriteLine(content);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found...");
            }
        }

        public static bool Save2Memory()
        {
            Animal a = new Animal();
            try
            {
                //MappedFile = MemoryMappedFile.CreateFromFile(OriginalFile);
                //MemoryMappedViewStream Stream = MappedFile.CreateViewStream();
                //MemoryMappedViewAccessor Accesor = MappedFile.CreateViewAccessor();
                

                using (FileStream stream = new FileStream(OriginalFile, FileMode.Create, FileAccess.Write))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, a);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string CreateTemporaryFile()
        {
            TempFileCollection tfc = new TempFileCollection();
            string fileName = tfc.AddExtension("txt");

            // Create and use the tmp file
            using (FileStream fs1 = File.OpenWrite(fileName))
            {
                using (StreamWriter sw1 = new StreamWriter(fs1))
                {
                    sw1.WriteLine("Test string " +DateTime.Now);
                }
            }
            
            return fileName;
        }
    }
}
