using System;

namespace _Specialties
{
    class Animal
    {
        public string Type { set; get; }
    }

    class Program
    {
        public static string DoubleQuestionMark(Animal a)
        {
            Animal temp = new Animal() { Type = "Reptile" };
            return (a.Type) ?? temp.Type;
        }

        static void Main(string[] args)
        {
            Animal animal = new Animal();
            //animal.Type = "Mammal";
            Console.WriteLine("Animal Type: {0}", DoubleQuestionMark(animal));
            Console.ReadLine();
        }
    }
}
