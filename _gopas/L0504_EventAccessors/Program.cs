using System;

namespace L0504_EventAccessors
{
    public class Worker
    {
        private EventHandler Handler;
        public event EventHandler WorkDone
        {
            add
            {
                if(Handler == null)
                    Handler += value;
            }
            remove { Handler -= value; }
        }

        public void DoWork()
        {
            Console.WriteLine("Doing my job...");
            if (Handler != null) { Handler(this, EventArgs.Empty); }
        }
    }

    class Program
    {
        public static void WorkerListener(object sender, EventArgs e)
        {
            Console.WriteLine("Done");             
        }

        static void Main(string[] args)
        {
            Worker w = new Worker();
            w.WorkDone += WorkerListener;
            w.WorkDone += WorkerListener;
            w.WorkDone += WorkerListener;
            w.DoWork();

            Console.ReadLine();
        }
    }
}
