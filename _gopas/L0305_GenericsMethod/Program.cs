using System;
using System.Linq;

namespace L0305_GenericsMethod
{
    interface ISomeInterface { }

    class SomeClass { }

    //class BaseClass<T> where T : IComparable { }

    //class MyClass<T> : BaseClass<T> where T : IComparable
    //{
    //    void SomeMethod(T value)
    //    {
    //        ISomeInterface obj1 = (ISomeInterface)value;

    //        //SomeClass obj2 = (SomeClass)value;

    //        if (value is SomeClass)
    //        {
    //            object tmp = (object)value;
    //            SomeClass obj2 = (SomeClass)tmp;
    //        }
    //    }
    //}

    public class BaseClass<T> where T : new()
    {
        public virtual T SomeMethod()
        {
            return new T();
        }
    }

    public class MyClass<T> : BaseClass<T> where T : new()
    {
        public override T SomeMethod()
        {
            return new T();
        }
    }

    class MyClass2
    {
        public string MyMethod<T>(T value) where T : struct
        {
            return default(T).ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //MyClass2 mc = new MyClass2();
            //Console.WriteLine( mc.MyMethod<int>(5) );
            //Console.ReadLine();
            string[] list = new string[] { "warr", "sham", "dru", "hun", "mag", };


            //Array.Sort(list);
            //int index = Array.IndexOf(list, "hun");
            //Console.WriteLine(index);

            string result = list.Aggregate((part, next) => part + " " + next);
            Console.WriteLine(result);

            result = list.Aggregate((part, next) => next + " " + part);
            Console.WriteLine(result);
            
            Console.ReadLine();

        }
    }
}
