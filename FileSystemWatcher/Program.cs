using System;
using System.Threading;

namespace FileSystemWatcher
{
	public class Program
	{
		public event EventHandler FileContainChanged;
		public System.IO.FileSystemWatcher watcher;

		public void CreateWatcher()
		{
			watcher = new System.IO.FileSystemWatcher();
			watcher.Path = @"c:\temp\";
			watcher.NotifyFilter =
					System.IO.NotifyFilters.Size |
					System.IO.NotifyFilters.FileName |
					System.IO.NotifyFilters.DirectoryName |
					System.IO.NotifyFilters.CreationTime |
					System.IO.NotifyFilters.LastAccess |
					System.IO.NotifyFilters.LastWrite;

			watcher.Filter = "*.*";
			watcher.Changed += watcher_Changed;
			watcher.Created += watcher_Changed;
			watcher.Deleted += watcher_Changed;
			watcher.Renamed += new System.IO.RenamedEventHandler(watcher_Renamed);
		}

		private void watcher_Changed(object sender, System.IO.FileSystemEventArgs e)
		{
			Console.WriteLine("{0} zmenen ({1})", e.Name, e.ChangeType);
			FileContainChanged(e.Name, null);
		}

		private void watcher_Renamed(object sender, System.IO.RenamedEventArgs e)
		{
			Console.WriteLine("{0} prejmenovan na {1}", e.OldName, e.Name);
		}

		static void Main(string[] args)
		{
			Program p = new Program();
			p.CreateWatcher();
			p.watcher.EnableRaisingEvents = true;
			p.FileContainChanged += Event_FileContainChanged;

			Console.WriteLine($"Sledovany adresar: {p.watcher.Path}");

			while (true)
			{
				Thread.Sleep(1000);
			}
		}

		private static void Event_FileContainChanged(object sender, EventArgs e)
		{
			Console.WriteLine(System.IO.File.ReadAllText(System.IO.Path.Combine("c:\\temp", sender.ToString())));
		}
	}
}
