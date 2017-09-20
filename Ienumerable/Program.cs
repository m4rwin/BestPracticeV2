using System;
using System.Collections.Generic;
using System.Linq;

namespace Ienumerable
{
    class Program
    {
        public static List<string> Database;

        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
            time.Start();

            Database = new List<string>() { "Anna", "Diana", "Maria", "Dido", "Rita", "Paula", "Donna", "Tatiana", "Jane", "Nana", "Devon", "Cecil", "Gita", "Daedra" };
            IEnumerable<string> result = Database.Where(name => name.Length > 4);

            for (int i = 1; i <= 1000000; i++)
            {
                Print(i, result.ToList());
            }
            time.Stop();
            Console.WriteLine("Time: {0}ms", time.ElapsedMilliseconds);
            Console.ReadLine();
        }

        // !!!!!!!!!!!!
        // Pozor na pouzivani IEnumerable v parametru funkce
        // V tomto pripade bude enumerace provedena pokazde, kdyz se funkce zavola
        // Je potreba enumeraci zhnotnit pred pouzitim ve funkci
        private static void Print(int iteration, IEnumerable<string> result)
        {
            foreach (var item in result)
            {
                //Console.Write(item + ",");
            }
            //Console.WriteLine("iteration: {0}", iteration);
            //Console.WriteLine("--------------------------");
        }
    }
}
