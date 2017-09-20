#define LOGMODE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Contracts
{
    class Program
    {
        private List<int> list;

        public Program()
        {
            list = new List<int>();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Add(58, 10);
            int result = p.AddOne(70);

            p.Log(string.Join("-", p.list));
            p.Log("END");
            Console.ReadLine();
        }

        public void Add(int a, int b)
        {
            #region contracts
            Contract.Requires(a < 100, "A is greater than 100");
            Contract.Ensures((a + b) < 100, "Sum i greater than 100");
            #endregion

            list.Add(a + b);
        }

        public int AddOne(int a)
        {
            #region contracts
            Contract.Ensures(Contract.Result<int>() > 60);
            #endregion

            return a++;
        }

        [Conditional("LOGMODE")]
        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }


    }
}
