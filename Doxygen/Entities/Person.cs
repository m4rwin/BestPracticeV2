namespace Doxygen.Entities
{
    /// <summary>
    /// Třída reprezentující osobu/Person
    /// </summary>
    public class Person
	{
		// Jméno osoby
		public string Name { set; get; }

		// Příjmení osoby
		public string Surname { set; get; }

		// Věk osoby
		public int Age { set; get; }

		/// <summary>
		/// Modifikovaná metoda ToString pro potřeby třídy Person
		/// </summary>
		/// <returns>Vrací řetězec Name + Surname, Age</returns>
		public override string ToString()
		{
			return string.Format("{0} {1}, {2}", this.Name, this.Surname, this.Age);
		}
	}
}
