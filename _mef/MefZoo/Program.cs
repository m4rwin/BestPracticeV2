using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Linq;

namespace MefZoo
{
	public class Program
	{
		static void Main(string[] args)
		{
			Zoo z = new Zoo();
			LoadAnimals(z);

			try
			{
				string age = string.Empty;
				z.Animals.ToList().ForEach(i => Console.WriteLine($"jmeno: {i.Name}, datum: {i.GetTime(out age)}"));
				Console.WriteLine($"age: {age}");
			}
			catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }

			Console.ReadLine();
		}

		public static void LoadAnimals(Zoo zoo)
		{
			try
			{
				// A catalog that can aggregate other catalogs
				var aggrCatalog = new AggregateCatalog();

				// A directory catalog, to load parts from dlls in the Extensions folder
				var dirCatalog = new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Extension", "*.dll");
				aggrCatalog.Catalogs.Add(dirCatalog);

				// An assembly catalog to load information  about part from this assembly
				var asmCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
				aggrCatalog.Catalogs.Add(asmCatalog);

				// Create a container
				CompositionContainer container = new CompositionContainer(aggrCatalog);

				// Composing the parts
				container.ComposeParts(zoo);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
