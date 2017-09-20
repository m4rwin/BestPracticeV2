using CalumMcLellan.StructuredStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PropSpy
{
	class Program
	{
		static List<string> result = new List<string>(10000);
		static List<string> err_too_long_props = new List<string>(10000);
		static List<string> err_file_not_found = new List<string>(10000);
		static List<string> err_too_long_links = new List<string>(10000);

		static string Path;
		static string[] Files;

		static Stopwatch MyTimer = new Stopwatch();

		static void Main(string[] args)
		{
			DrawProgramInfo();

			Console.Write("Input folder path: (empty for current folder)");
			Path = Console.ReadLine();

			StartWatch();

			if (string.IsNullOrWhiteSpace(Path))
				Path = System.Environment.CurrentDirectory;

			Files = Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories);

			Console.WriteLine($"Number of files: {Files.Length}");

			foreach (var file in Files)
			{
				string line = $"file: {file}:";
				result.Add(line);

				if (!File.Exists(file))
				{
					err_file_not_found.Add($"File NOT EXIST. ({file})."); 
					continue;
				}

				PropertySets psets = new PropertySets(file, true);
				PropertySet psi = null;

				if (psets.Contains(PropertySetIds.UserDefinedProperties))
				{
					if ((psi = psets[PropertySetIds.UserDefinedProperties]) != null)
					{ ReadProp(file, psi); }
				}
				if (psets.Contains(PropertySetIds.DocumentSummaryInformation))
				{
					if ((psi = psets[PropertySetIds.DocumentSummaryInformation]) != null)
					{ ReadProp(file, psi); }
				}
				if (psets.Contains(PropertySetIds.ProjectInformation))
				{
					if ((psi = psets[PropertySetIds.ProjectInformation]) != null)
					{ ReadProp(file, psi); }
				}
				if (psets.Contains(PropertySetIds.SummaryInformation))
				{
					if ((psi = psets[PropertySetIds.SummaryInformation]) != null)
					{ ReadProp(file, psi); }
				}
				if (psets.Contains(PropertySetIds.ExtendedSummaryInformation))
				{
					if ((psi = psets[PropertySetIds.ExtendedSummaryInformation]) != null)
					{ ReadProp(file, psi); }
				}

				result.Add("------------------------------------------------------");
				result.Add(string.Empty);
			}

			PauseWatch();

			Console.Write("\nInput folder with links files: ");
			Path = Console.ReadLine();

			UnpauseWatch();

			if (string.IsNullOrWhiteSpace(Path))
				Console.WriteLine("Null path. Continue...");
			else
				ReadDummyFiles(Path);

			
			DrawHeader(err_too_long_props, "TOO LONG PROPERTIES");
			DrawHeader(err_file_not_found, "FILES NOT FOUND");
			DrawHeader(err_too_long_links, "TOO LONG LINKS");

			TimeSpan time = StopWatch();

			string output = $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}_PropsInfo.txt";
			File.AppendAllLines(output, err_too_long_props, Encoding.UTF8);
			File.AppendAllLines(output, err_file_not_found, Encoding.UTF8);
			File.AppendAllLines(output, err_too_long_links, Encoding.UTF8);
			File.AppendAllLines(output, result, Encoding.UTF8);

			Console.WriteLine($"\nLogfile '{output}' was created.");
			Console.WriteLine($"end program (duration: {time.TotalMilliseconds} ms, {time.TotalSeconds} s, {time.TotalMinutes} m)");
			Console.ReadLine();
		}

		private static void ReadDummyFiles(string path)
		{
			Files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
			Console.WriteLine($"Number of link files: {Files.Length}");

			foreach (var file in Files)
			{
				if (!File.Exists(file))
				{
					err_file_not_found.Add($"File NOT EXIST. ({file}).");
					continue;
				}

				string[] items = File.ReadAllText(file).Split(new string[] { ";" }, StringSplitOptions.None);

				string link = string.Empty;
				if(items.Length > 2 && !string.IsNullOrWhiteSpace(items[1]))
					link = items[1];

				if (link.Length > 255)
					LogTooLongLink(file);

			}
		}

		private static void DrawHeader(List<string> list, string msg)
		{
			list.Insert(0, $"-------- {msg} ({list.Count})--------");
			list.Insert(list.Count, $"-------- END {msg} --------");
			list.Add(string.Empty);
			list.Add(string.Empty);
			list.Add(string.Empty);
		}

		private static void ReadProp(string item, PropertySet psi)
		{
			result.Add(string.Empty);
			string line = $"{psi.Name}:";
			result.Add(line);

			foreach (var prop in psi)
			{
				int length = prop.Value?.ToString()?.Length ?? 0;
				if (length > 255 )
					LogTooLongProps(item, prop);

				line = $"({length})\t{ReturnPropName(prop.Name)}:{InputSpace(ReturnPropName(prop.Name))}{prop.Value}";
				result.Add(line);
			}
			result.Add(string.Empty);
		}

		private static string ReturnPropName(string name)
		{
			return (string.IsNullOrWhiteSpace(name)) ? "## NULL ##" : name;
		}

		private static object InputSpace(string name)
		{
			if (name.Length == 22)
				return "\t\t";
			if (name.Length >= 22)
				return "\t";
			if (name.Length >= 15)
				return "\t\t";
			else if (name.Length >= 7)
				return "\t\t\t";
			else
				return "\t\t\t\t";
		}

		private static void LogTooLongProps(string file, Property prop)
		{
			err_too_long_props.Add($"File {file} has too long property value. Prop name is '{prop.Name}' and length is {prop.Value.ToString().Length}");
		}

		private static void LogTooLongLink(string file)
		{
			err_too_long_links.Add($"File {file} has too long link value.");
		}

		#region Timer
		private static void StartWatch()
		{
			MyTimer.Reset();
			MyTimer.Start();
		}

		private static TimeSpan StopWatch()
		{
			MyTimer.Stop();
			return MyTimer.Elapsed;
		}

		private static void PauseWatch()
		{
			MyTimer.Stop();
		}

		private static void UnpauseWatch()
		{
			MyTimer.Start();
		}
		#endregion

		#region Others
		private static void DrawProgramInfo()
		{
			Console.WriteLine("|------------------|");
			Console.WriteLine("|-- PropSpy v0.2 --|");
			Console.WriteLine("|------------------|");
			Console.WriteLine(" ");
		}
		#endregion
	}
}
