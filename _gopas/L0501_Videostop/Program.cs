using System;

namespace L0501_Videostop
{
    public delegate void MyDelegate();

    class Cube
    {
        private static Random R = new Random();
        public int Value { set; get; }
        public void Next() {
            if (Value >= 6)
                Value = 1;
            else
                Value++;
        }

        public Cube()
        {
            Value = R.Next(1, 7);
        }
    }

    class Program
    {
        public static void Print(Cube c1, Cube c2, Cube c3)
        {
            Console.WriteLine("{0} | {1} | {2}", c1.Value, c2.Value, c3.Value);
        }

        static void Main(string[] args)
        {
            Cube c1 = new Cube();
            Cube c2 = new Cube();
            Cube c3 = new Cube();

            MyDelegate d = c1.Next;
            d += c2.Next;
            d += c3.Next;


            Delegate[] arr = d.GetInvocationList();
            Random R = new Random();

            while(true)
            {
                ((MyDelegate)arr[R.Next(0, 3)])();
                Print(c1, c2, c3);
                Console.ReadLine();
            }
        }
    }
}
