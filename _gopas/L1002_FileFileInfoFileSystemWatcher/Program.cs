using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace L1002_FileFileInfoFileSystemWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileOriginalPath = "c:\\temp\\hello.txt";

            using (StreamWriter writer = File.CreateText(FileOriginalPath))
            {
                writer.Write("hello you little pony...");
            }

            string FileBackupPath = string.Format("{0}\\{1}.backup", 
                Path.GetDirectoryName(FileOriginalPath),
                Path.GetFileNameWithoutExtension(FileOriginalPath));

            string FileBackupPath2 = string.Format("{0}\\{1}.backup2",
                Path.GetDirectoryName(FileOriginalPath),
                Path.GetFileNameWithoutExtension(FileOriginalPath));

            if (File.Exists(FileBackupPath))
                File.Delete(FileBackupPath);

            File.Copy(FileOriginalPath, FileBackupPath);

            FileInfo info = new FileInfo(FileOriginalPath);
            if (File.Exists(FileBackupPath2))
                File.Delete(FileBackupPath2);
            info.CopyTo(FileBackupPath2);


            foreach (string item in Directory.GetFiles("c:\\", "*.*"))
            {
                Console.WriteLine(item);
            }

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "c:\\temp\\";
            watcher.Filter = "*.txt";
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Renamed += Wathcer_Renamed;
            watcher.Deleted += Wathcer_Deleted;
            watcher.Changed += Wathcer_Changed;
            watcher.EnableRaisingEvents = true;

            Console.ReadLine();
        }

        private static void Wathcer_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("deleted file: {0}", e.Name);
        }

        private static void Wathcer_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("renamed file from: {0}, to: {1}", e.OldName, e.Name);
        }

        private static void Wathcer_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File {0} was edited.", e.Name);
            Console.WriteLine("Change type: {0}", e.ChangeType.ToString());
        }
    }
}
