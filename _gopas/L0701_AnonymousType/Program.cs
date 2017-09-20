using System;

namespace L0701_AnonymousType
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee0 = new { Id = 107, Genre = "Male", Name = "Martin Hroemk" };
            var employee1 = new { Id = 107, Name = "Martin Hroemk", Genre = "Male" };
            var employee2 = new { Id = 369, Name = "Eva Brezovska", Genre = "Female" };

            // anonymni typ je imutable - nemenny
            //employee.Name = "John Rambo";

            Console.WriteLine(employee0.Equals(employee2));
            
        }
    }
}
