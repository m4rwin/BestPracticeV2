namespace MefZooLib
{
    public interface IAnimal
	{
		string Name { get; }

        string GetTime(out string yourAge);
	}
}
