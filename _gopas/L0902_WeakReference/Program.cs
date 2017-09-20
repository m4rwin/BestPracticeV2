using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L0902_WeakReference
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder("Hello world");
            WeakReference wr = new WeakReference(sb);
            sb = null;
            GC.Collect();

            if (wr.IsAlive)
            {
                sb = (StringBuilder)wr.Target;
                Console.WriteLine(sb.ToString());
            }
            else
            {
                Console.WriteLine("Object is dead");
            }
        }
    }
}
