using System;
using System.IO;
using System.Text;

namespace L1001_Streams
{
    class Program
    {
        static void Main(string[] args)
        {
            string MyDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string FilePathAndName = Path.Combine(MyDocPath, "dataUTF8.txt");


            using (FileStream fs = new FileStream(FilePathAndName, FileMode.Create, FileAccess.Write))
            {
                Encoding encoding = Encoding.UTF8;
                //Console.WriteLine("CanRead: {0}, CanWrite: {1}, CanSeek: {2}, CanTimeout: {3}", fs.CanRead, fs.CanWrite, fs.CanSeek, fs.CanTimeout);

                //using (BinaryWriter writer = new BinaryWriter(fs))
                using (StreamWriter writer = new StreamWriter(fs, encoding))
                {
                    writer.Write(10);
                    writer.Write("Hello you motherfucker....");
                    fs.Flush();
                }
            }

            using(FileStream fs = new FileStream(FilePathAndName, FileMode.Open, FileAccess.Read))
            {
                //using(BinaryReader reader= new BinaryReader(fs))
                using (StreamReader reader = new StreamReader(fs))
                {
                    Console.WriteLine(reader.ReadLine());
                    Console.WriteLine(reader.ReadLine());
                }
            }
            
        }
    }
}
