using System;

namespace Delegates
{
    class Program
    {
        delegate T MyDelegate<T>(T a, T b);
        delegate void SecondDelegate();

        static void Main(string[] args)
        {
            /*
             * EXAMPLE 1
             * [pole delegatu]
             */
            /*
            List<MyDelegate<double>> list = new List<MyDelegate<double>>();
            list.Add(MathOp.Add);
            list.Add(MathOp.Div);

            double A = 10;
            double B = 5;
            double RESULT = 0;

            foreach (MyDelegate<double> del in list)
            {
                RESULT = del(A, B);
                Console.WriteLine(string.Format("Result = {0}", RESULT));
            }
            */

            /*
             * EXAMPLE 2
             * [Multicast delegat - pomoci jednoho delegata zavolam vice metod]
             */
            SecondDelegate del = MathOp.Add;
            del += MathOp.Div;

            del();

            Console.ReadLine();
        }
    }
}
