using System;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        public Task<int> ComputeAsync()
        {
            return Task.Run(() =>
            {
                //System.Threading.Thread.Sleep(3000);
                for (int i = 0; i < 10000; i++)
                {
                    Console.Write(".");
                }
                return 42;
            });
        }

        public async void GetResult()
        {
            int number = await ComputeAsync();
            Console.WriteLine("Number: {0}", number);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.GetResult();
            Console.ReadLine();
        }
    }
}
