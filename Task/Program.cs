using System;
using System.Threading.Tasks;

namespace TaskParalelism
{
    class Program
    {
        public static void From2ToIteration(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Console.Write(" {0}", i);
            }
        }

        public static void PrintEnd()
        {
            Console.WriteLine("\nWaiting for Tasks");
            Console.WriteLine("End of program");
        }

        static void Main(string[] args)
        {
            var task1 = Task.Factory.StartNew(() => From2ToIteration(1, 10));
            task1.ContinueWith((t) => { Console.WriteLine("<Task1>");});

            var task2 = Task.Factory.StartNew(() => From2ToIteration(90,100));
            task2.ContinueWith((t) => { Console.WriteLine("<Task2>"); });

            var task3 = Task.Factory.StartNew(() => From2ToIteration(-50, -40));
            task1.ContinueWith((t) => { Console.WriteLine("<Task3>"); });

            // wait
            //task1.Wait();
            //task2.Wait();
            //task3.Wait();

            // wait

            var task4 = Task.Factory.ContinueWhenAll(new Task[] { task1, task2, task3 },
                (t) => PrintEnd());

            

            Console.ReadLine();
        }
    }
}
