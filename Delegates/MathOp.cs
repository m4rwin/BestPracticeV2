using System;

namespace Delegates
{
    public static class MathOp
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Div(double a, double b)
        {
            return a / b;
        }

        public static void Add()
        {
            Console.WriteLine("I am Add function");
        }

        public static void Div()
        {
            Console.WriteLine("I am Div function");
        }
    }
}
