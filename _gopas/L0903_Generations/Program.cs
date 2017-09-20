using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L0903_Generations
{
    /*
    Vytvoreny objekt je umisten do I.generace
    Po uklidu GC jsou objekty bez referenci zahozeny/smazany, objekty ktere preziji jsou presunuty do II.generace
    Po dalsim uklidu GC jsou opet nepotrebne reference smazany a prezivsi reference jsou presunuty do posledni III.generace

    I.generace [index 0]
    II. generace [index 1]
    III. generace [index 2]
    */
    class Program
    {
        static void Main(string[] args)
        {
            object o = new object();
            Console.WriteLine("Max. generation: {0}, Object generation: {1}", GC.MaxGeneration, GC.GetGeneration(o));

            GC.Collect();

            Console.WriteLine("Max. generation: {0}, Object generation: {1}", GC.MaxGeneration, GC.GetGeneration(o));

            GC.Collect();

            Console.WriteLine("Max. generation: {0}, Object generation: {1}", GC.MaxGeneration, GC.GetGeneration(o));

            WeakReference wr = new WeakReference(o);
            o = null;
            GC.Collect();

            if (wr.IsAlive)
            {
                o = (object)wr.Target;
                Console.WriteLine(o.ToString());
            }
            else
            {
                Console.WriteLine("Not alive");
            }
        }
    }
}
