using System;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog tDog = new Dog();
            Cat gCat = new Cat();
            SoundAndType<Cat>(gCat);
            SoundAndType<Dog>(tDog);

            Console.WriteLine("x = " +Mamut.x);

            Console.WriteLine("\nProhazovani hodnoty ruznych typu:");
            int i1 = 5;
            int i2 = 13;
            string s1 = "Hello";
            string s2 = "World";
            Console.WriteLine("i1 = {0}, i2 = {1}, s1 = {2}, s2 = {3}", i1, i2, s1, s2);

            SwitCH<int>(ref i1, ref i2);
            SwitCH<string>(ref s1, ref s2);
            Console.WriteLine("i1 = {0}, i2 = {1}, s1 = {2}, s2 = {3}", i1, i2, s1, s2);


            Console.ReadLine();
        }

        public static void SoundAndType<T>(T obj) where T: Animal
        {
            obj.MakeSound();
            obj.GetTyp();
        }

        // prohodi dve hodnoty typu T - nezalezi na typu
        private static void SwitCH<T>(ref T item1, ref T item2)
        {
            T temp = item1;
            item1 = item2;
            item2 = temp;
        }
    }

    interface Animal
    {
        void MakeSound();
        void GetTyp();
    }

    class Dog : Animal
    {
        public void MakeSound() { Console.WriteLine("Haf Haf."); }
        public void GetTyp() { Console.WriteLine("Type: " +this.GetType().Name ); }
    }

    class Cat : Animal
    {
        public void MakeSound() { Console.WriteLine("Mau Mau."); }
        public void GetTyp() { Console.WriteLine("Type: " + this.GetType().Name); }
    }

    static class Mamut
    {
        public static int x; 
        static Mamut()
        {
            x = 10;
        }
    }
}
