using System;

namespace Exceptions
{
    class Program
    {
        public void Method1() { throw new OutOfMemoryException(); }
        public void Method2() { Console.WriteLine("Method2 done."); }
        public void Method3() { throw new InvalidOperationException("Opops"); }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("CAUGHT UNHANDLED EXCEPTION.");
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("---------------------------");
            //Console.WriteLine("Re-running...");
            //Program.Main(null);
        }

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Console.WriteLine("Start app...");
            Console.ReadLine();
            Program p = new Program();
            p.Method3();
            p.Method2();

            /*
            try
            {
                p.Method1();
            }
            catch (OutOfMemoryException e) {  }

            p.Method2(); 
            */



            Console.WriteLine("End.");
            Console.ReadLine();
        }
    }
}
