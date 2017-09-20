using System;
using System.Collections.Generic;

namespace L0204_Inheritance2
{
    public class Shape
    {
        public virtual double Surface() { return 0.0; }
    }

    public class Rectangle:Shape
    {
        public double A { set; get; }
        public double B { set; get; }

        public Rectangle(double a, double b) { A = a; B = b; }

        public override double Surface() { return A * B; }
    }

    public class Block : Rectangle
    {
        public double C { set; get; }

        public Block(double a, double b, double c)
            :base(a,b)
        { C = c; }

        public override double Surface() { return A * B * C; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Shape r = new Rectangle(10, 30);
            Shape b = new Block(55, 99, 10);

            List<Shape> list = new List<Shape>();
            list.Add(r);
            list.Add(b);

            list.ForEach(s => Console.WriteLine("Shape: {0}", s.Surface()));
            Console.ReadLine();
        }
    }
}
