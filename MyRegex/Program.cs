using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;

namespace MyRegex
{
    /*
    * . nahrazuje libovolny znak
    * ^ kontrola od zacatku retezce
    * $ kontrola od konce retezce
    * {N}  kde N udává počet opakování
    * {N, M}  kde N je minimální počet opakování a M maximální
    * (?) je alternativou k {0, 1}
    * * {0-∞} 
    * + {1-∞}
    *
    * ZKRATKY
    * -------
    * \d   [0-9]
    * \D   [^0-9]
    * \w   pro jakekoliv pismeno, cislici nebo podtrzitko
    * \s   je pro bile znaky
    */
    class Program
    {
        Regex Regex { set;get; } = new Regex(@"(\w{1,3}\-)*\d{1,3}\-\d{1,3}\-\w{4,5}\-\d+");
        private int Success { set; get; } = 0;
        private int Failed { set; get; } = 0;
        private int NoPrefix { set; get; } = 0;
        private List<Result> ListOfResults { set; get; }
        private Result CurrentItem { set; get; }

        public IEnumerable<FileInfo> FindFiles(string path)
        {
            string[] pattern = new string[] { "*.par", "*.dft" };
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);


            foreach (var item in files)
            {
                FileInfo fi = new FileInfo(item);

                if (fi.Extension.ToLower().Equals(".lnk") ||
                    fi.Extension.ToLower().Equals(".db") ||
                    fi.Extension.ToLower().Equals(".catpart") ||
                    fi.Extension.ToLower().Equals(".catdrawing") ||
                    fi.Extension.ToLower().Equals(".catproduct") ||
                    fi.Extension.ToLower().Equals(".jpg") ||
                    fi.Extension.ToLower().Equals(".bmp") ||
                    fi.Extension.ToLower().Equals(".pdf")) { continue; }

                yield return fi;
            }
        }

        public void ParseFilename(FileInfo file)
        {
            CurrentItem = new Result();

            string[] parts = file.Name.Split('_');
            Match match = Regex.Match(parts[0]);

            if (parts.Length == 1)
            {
                CurrentItem.ERP = file.Name.Substring(0, file.Name.Length-file.Extension.Length);
            }
            else { CurrentItem.ERP = parts[0]; }

            if (match.Success)
            {
                CurrentItem.Res = "Success";
                Success++;
            }
            else
            {
                CurrentItem.Res = "Fail";
                Failed++;
            }

            
            CurrentItem.FileName = file.Name;
            CurrentItem.Extension = file.Extension;
            CurrentItem.DirectoryName = file.DirectoryName;
            CurrentItem.FullName = file.FullName;
            CurrentItem.CreationTime = file.CreationTimeUtc.ToString();
            CurrentItem.LastWriteTime = file.LastWriteTimeUtc.ToString();
            CurrentItem.LastAccessTime = file.LastAccessTimeUtc.ToString();
            ListOfResults.Add(CurrentItem);
        }


        static void Main(string[] args)
        {
            CultureInfo ci = new CultureInfo("de-DE");
            CultureInfo.DefaultThreadCurrentCulture = ci;

            Program p = new Program();
            p.ListOfResults = new List<Result>();

            foreach (var item in p.FindFiles(@"\\SRVCAD\cad\CAD0"))
            {
                p.ParseFilename(item);
            }

            /*using (TextReader reader = new StreamReader("data.txt", Encoding.UTF8))
            {
                while( reader.Peek() >= 0 ){
                    p.ParseFilename(reader.ReadLine());
                }
            }*/

            p.Save2File();
            Console.WriteLine("End");
            Console.ReadLine();
        }

        private void Save2File()
        {
            ListOfResults.Sort(new CompareBySuccessfull());
            //using (StreamWriter writer = new StreamWriter(new FileStream("result.dat", FileMode.Open, FileAccess.ReadWrite), Encoding.UTF8))
            using (TextWriter writer = new StreamWriter(new FileStream("result.dat", FileMode.Open, FileAccess.ReadWrite), Encoding.UTF8))
            {
                writer.WriteLine("Result\tERP-Revision\tFileName\tExt\tDirectory Name\tFullName\tCreation Time(UTC)\tLast Write Time(UTC)\tLast Access Time(UTC)");
                foreach (Result item in ListOfResults)
                {
                    writer.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", item.Res, item.ERP, item.FileName, item.Extension, item.DirectoryName, item.FullName, item.CreationTime, item.LastWriteTime, item.LastAccessTime);
                }

                Console.WriteLine(string.Format(" ==> Success items: {0}, Failed items: {1}, No Prefix: {2}", Success, Failed, NoPrefix));
                writer.WriteLine(string.Format("Success items: {0}\tFailed items: {1}\tNo Prefix: {2}", Success, Failed, NoPrefix));
            }
    }
    }

    public class Result
    {
        public string Res { set; get; }
        public string ERP { set; get; }
        public string FileName { set; get; }
        public string DirectoryName { set; get; }
        public string FullName { set; get; }
        public string Extension { set; get; }
        public string CreationTime { set; get; }
        public string LastWriteTime { set; get; }
        public string LastAccessTime { set; get; }
    }

    public class CompareBySuccessfull : IComparer<Result>
    {
        public int Compare(Result x, Result y)
        {
            return x.Res.CompareTo(y.Res);
        }
    }
}
