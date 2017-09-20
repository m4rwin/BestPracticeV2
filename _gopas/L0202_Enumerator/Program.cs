using System;

namespace L0202_Enumerator
{
    class Program
    {
        [Flags()]
        enum Color : long {
            Blue = 1,
            Red = 2,
            Green = 4,
            White = 8,
            Black = 16
        }

        static void Main(string[] args)
        {
            Color color = Color.Red | Color.White;

            if (color.HasFlag(Color.Blue)) { Console.WriteLine("Color is blue"); }
            if (color.HasFlag(Color.Red)) { Console.WriteLine("Color is red"); }
            if (color.HasFlag(Color.Green)) { Console.WriteLine("Color is green"); }
            if (color.HasFlag(Color.White)) { Console.WriteLine("Color is white"); }
            if (color.HasFlag(Color.Black)) { Console.WriteLine("Color is black"); }

            Console.ReadLine();
        }
    }
}
