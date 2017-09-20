using System;

namespace DesignPatterns.Singleton.Singleton
{
    #region Classic Singleton
    public sealed class Singleton
    {
        #region Private Properties
        public string DateCreation { private set; get; }
        #endregion

        private Singleton()
        {
            DateCreation = DateTime.Now.ToString(("o"));
        }
        private static readonly Singleton instance = new Singleton();

        public static Singleton UniqueInstance
        {
            get { return instance; }
        }
    }
    #endregion

    #region Generics Singleton
    public sealed class GenericsSingleton<T> where T : class, new()
    {
        private GenericsSingleton()
        {
        }

        private static readonly T instance = new T();
        public static T UniqueInstance { get { return instance; } }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            #region Classic Singleton Test
            Singleton s1 = Singleton.UniqueInstance;
            Singleton s2 = Singleton.UniqueInstance;
            Console.WriteLine("s1: {0}{2}s2: {1}", s1.DateCreation, s2.DateCreation, Environment.NewLine);
            #endregion

            Console.WriteLine("---");

            #region Generics Singleton Test
            Test1 i1 = GenericsSingleton<Test1>.UniqueInstance;
            Test1 i2 = GenericsSingleton<Test1>.UniqueInstance;
            Console.WriteLine("s1: {0}{2}s2: {1}", i1.DateCreation, i2.DateCreation, Environment.NewLine);
            #endregion

            Console.WriteLine("---");

            #region End
            Console.ReadLine(); 
            #endregion
        }
    }

    public class Test1
    {
        public string DateCreation { get; }

        public Test1() { DateCreation = DateTime.Now.ToString(("o")); }
    }
}
