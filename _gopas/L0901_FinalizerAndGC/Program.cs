using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L0901_FinalizerAndGC
{
    public class FileStream : IDisposable
    {
        private bool Disposed { set; get; }

        public void Open() { Console.WriteLine("Opening file"); }
        public void Close() { Dispose(); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dis)
        {
            // Pokud jsme volani z metody Dispose()
            // muzeme uvolnit i vlastnene IDisposable prvky, z destruktoru ne.
            if (dis)
            {
                // uvolnujeme tzv. managed resources
                Console.WriteLine("Releasing managed sources");
            }

            if (!Disposed)
            {
                // v kazdem pripade vsak ulolnujeme unmanaged resources
                // CloseHandle(handle);
                // handle = IntPtr.Zero;
                Console.WriteLine("Releasing unmanaged sources");
                Disposed = true;
            }
        }

        ~FileStream()
        {
            Dispose(false);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Try/finally option
            //FileStream fs = new FileStream();
            //try
            //{
            //    fs.Open();
            //    throw new Exception("Invalid FileStream operation...");
            //}
            //finally
            //{
            //    if(fs != null) fs.Close();
            //}



            // 2. Using option
            //using (FileStream fs = new FileStream())
            //{
            //    fs.Open();
            //    throw new Exception("Invalid FileStream operation...");
            //}

            // Run without exceptions...
            FileStream fs = new FileStream();
            fs.Open();
            fs.Close();
        }
    }
}
