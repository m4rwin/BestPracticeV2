using System;
using System.Collections.Generic;

namespace LazyLoading
{
    #region help class
    public class Person
	{
		public string Name { set; get; }
		public string Gender { set; get; }
	}

	public class Group
	{
		public List<Person> list { set; get; }

		public Group()
		{
			Console.WriteLine("...Group Constructor");
			list = new List<Person>();
			list.Add(new Person() { Name = "Martin Hromek", Gender = "Male" });
			list.Add(new Person() { Name = "Eva Brezovska", Gender = "Fenale" });
		}

		public override string ToString()
		{
			foreach (var item in list)
			{
				Console.WriteLine("Name = {0}, Genre = {1}", item.Name,  item.Gender);
			}
			return string.Empty;
		}
	}
	#endregion

	class Program
	{
		static void Main(string[] args)
		{
			//Group g = new Group();
			Lazy<Group> g = new Lazy<Group>();
			Console.WriteLine("IsValueCreated = {0}", g.IsValueCreated);
			g.Value.ToString();
			Console.WriteLine("IsValueCreated = {0}", g.IsValueCreated);
		

			Console.ReadLine();
		}
	}
}
