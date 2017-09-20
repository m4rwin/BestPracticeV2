using System;

namespace L0502_Callbacks
{
    public delegate void WorkCompleteCallBack(string s);

    class Helper
    {
        public static WorkCompleteCallBack Callback;

        public static void DoWork()
        {
            Console.WriteLine("[{0}] Doing my work", DateTime.Now);
            for (int i = 0; i < 100000; i++) { }
            if(Callback != null) Callback(string.Format("[{0}] Done", DateTime.Now));
        }
    }

    class Program
    {
        public static void CallbackFunction(string result) { Console.WriteLine(result); }

        static void Main(string[] args)
        {
            Helper.Callback = CallbackFunction;
            Helper.DoWork();
            Console.ReadLine();
        }
    }
}
