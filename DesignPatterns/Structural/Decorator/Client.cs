using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Decorator
{
	/*
	 * Definice: účelem je poskytnout způsob pro dynamické připojení nového stavu a chování k objektu.
	 * Daný objekt neví, že je dekorován a to z něj dělá vzor.
	 * 
	 * Výhody:
	 * 
	 * původní objekt si není vědom žádných změn
	 * neexistuje žádná jedna velká třída, která by obsahovala všechny možnosti
	 * jednotlivé dekorace jsou na sobě vzájemně nezávislé
	 * dekorace mohou být poskládány dohromady libovolným způsobem
	 * 
	 * Účastníci vzoru:
	 * 
	 * Component: původní třída, jejíž operace může být nahrazena
	 * IComponent: rozhraní identifikující třídy, která může být dekorována
	 * Operation: operace objektu IComponent
	 * Decorator: třída, která odpovídá rozhraní IComponent a přidává stav/funkci
	 */
	
	public interface IComponent
	{
		string Operation();
	}

	public class Component : IComponent
	{
		public string Operation() => "Ja jsem zakladni komponenta";
	}

	public class Decorator : IComponent
	{
		private IComponent component;

		public Decorator(IComponent c) {component = c; }

		public string Operation() => component.Operation() + " a ja ji dekoruji";
	}

	public class Client
	{
		public static void Print(string msg, IComponent c) => Console.WriteLine($"{msg} | {c.Operation()}");

		static void Main(string[] args)
		{
			IComponent component = new Component();
			Print("Zakladni komponenta", component);
			Print("Dekorator", new Decorator(component));

			Console.ReadLine();
		}
	}
}
