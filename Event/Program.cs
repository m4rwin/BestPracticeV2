using System;

namespace Event
{
	public class Animal : ISpy
	{
		public event EventHandler StartListening;

		private string kind;
		public string Kind
		{
			get { return kind; }
			set
			{
				kind = value;
				if (StartListening != null) { StartListening(value, null); }
			}
		}

		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				if (StartListening != null) { StartListening(value, null);}
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Animal pet1 = new Animal();
			pet1.StartListening += NameChange;
			pet1.StartListening += KindChange;
			pet1.Name = "Christoper";
			pet1.Kind = "Lion";

			Console.WriteLine(Environment.NewLine + "End");
			Console.ReadLine();
		}

		private static void NameChange(object sender, EventArgs e) => Console.WriteLine("New name: {0}", sender.ToString());
		private static void KindChange(object sender, EventArgs e) => Console.WriteLine("New kind: {0}", sender.ToString());
	}
}
