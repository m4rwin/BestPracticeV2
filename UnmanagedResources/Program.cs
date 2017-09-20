using System;
using System.Runtime.InteropServices;

namespace UnmanagedResources
{
    class Program
    {
        #region DLL
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr SecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);
        #endregion

        #region SYSTEM HANDLE
        IntPtr _handle = IntPtr.Zero;
        #endregion

        #region C-TOR
        public Program(string filename)
        {
            Console.WriteLine("Program");
            _handle = CreateFile(filename,
                0x80000000, // pristup jen pro cteni
                1, // sdilene cteni
                IntPtr.Zero,
                3, // otevrit existujici
                0,
                IntPtr.Zero);

            Close();
        }
        #endregion

        #region D-TOR
        ~Program()
        {
            Console.WriteLine("~Program-start");
            if (_handle != IntPtr.Zero) 
            {
                Console.WriteLine("CloseHandle by ~Program");
                CloseHandle(_handle); 
            }
            Console.WriteLine("~Program-end");
        }
        #endregion

        #region METHODS
        public void Close()
        {
            Console.WriteLine("Close-start");
            if (_handle != IntPtr.Zero)
            {
                Console.WriteLine("CloseHandle by Close");
                GC.SuppressFinalize(this);
                CloseHandle(_handle);
            }
            Console.WriteLine("Close-end");
        }
        #endregion

        static void Main(string[] args)
        {
            Program p = new Program("martin_test.txt");
            Console.ReadLine();
        }
    }
}
