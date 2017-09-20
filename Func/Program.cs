using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncAndAction
{
    public class Program
    {

        #region Func
        // version 1
        // yield variant
        public static IEnumerable<string> Method1(IEnumerable<string> list)
        {
            foreach (var item in list)
            {
                if (item[0].Equals('P'))
                {
                    yield return item;
                }
            }
        }

        // version 2
        // LINQ variant
        public static IEnumerable<string> Method2(IEnumerable<string> list)
        {
            return list.Where(ch => ch[0].Equals('P'));
        }

        // version 3
        // Func variant
        public static IEnumerable<string> Method3(IEnumerable<string> list, Func<string, bool> SameChar)
        {
            //yield return list.ToList().ForEach(i => SameChar(i));
            foreach (var item in list)
            {
                if (SameChar(item)) yield return item;
            }
        }
        #endregion

        #region Action
        private static void PrintLowercase(string text)
        {
            Console.WriteLine(text.ToLower());
        }

        private static void PrintUppercase(string text)
        {
            Console.WriteLine(text.ToUpper());
        }

        private static void TestAction(List<string> words, bool isLower)
        {
            Action<string> my_first_action;

            if (isLower) { my_first_action = delegate(string s) { PrintLowercase(s); }; }
            else { my_first_action = t => PrintUppercase(t); };

            foreach (var word in words)
            {
                my_first_action(word); 
            }
        }
        #endregion

        #region Main
        static void Main(string[] args)
        {
            List<string> list = new List<string>() { "Martin", "Pavel", "Eva", "Pert", "Karel", "Tony" };

            // Func Test
            //(Method1(list)).ToList().ForEach(Console.WriteLine);
            //(Method2(list)).ToList().ForEach(Console.WriteLine);
            //(Method3(list, ch => ch[0].Equals('P'))).ToList().ForEach(Console.WriteLine);

            // Action Test
            TestAction(list, false);

            Console.WriteLine("End");
            Console.ReadLine();
        } 
        #endregion
    }
}
