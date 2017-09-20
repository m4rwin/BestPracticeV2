using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

namespace CSharp60
{
    class Person
    {
        // 1. Inicializator pro auto-properties
        // 2. Staci pouze getter u auto-properties, kdyz setter nezadefinuju tak je automaticky read-only
        public string Name { get; } = "Martin";
        public List<string> List;

        // 3. Zkraceny zapis metody
        public override string ToString() => Name;

        // 5. Elvis operator
        public static int? GetLength(string s)
        {
            return s?.Length;
        }

        // 6. Null Coalesing - neni novinka v C# 6
        public static int GetLength2(string s)
        {
            return s?.Length ?? 0;
        }

        // 7. String interpolation
        public static string StringInterpolation(string s)
        {
            return $"Prave je {DateTime.Now} hodin";
        }

        // 8. nameof operator
        public static string GetName()
        {
            return nameof(Name);
        }

        // 9. Exception filter
        public static void ExceptionFiler()
        {
            try
            {

            }
            catch (ArgumentException e) when (e.HelpLink == "Abc")
            {
                WriteLine("HelpLink je Abc");
            }
        }

        // 10. Await je nyni mozne pouzit i v catch kodu
        public async static Task AwaitCatch()
        {
            try
            {

            }
            catch (Exception e)
            {
                await Task.FromResult(10);
            }
        }
    }

    class Program
    {
        #region 1. String Formating
        public static void PrintText(string name)
        {
            Console.WriteLine(string.Format("Jmenu se {0} a dnes je {1}", name, DateTime.Now));
        }

        public static void PrintTextNew(string name)
        {
            Console.WriteLine($"Jmenu se {name} a dnes je {DateTime.Now}");
        } 
        #endregion

        static void Main(string[] args)
        {
            PrintText("Martin");
            PrintTextNew("Martin");

            #region 2. Using static ...
            ReadLine(); 
            #endregion
        }
    }
}
