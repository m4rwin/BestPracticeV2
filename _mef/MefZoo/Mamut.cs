using MefZooLib;
using System;
using System.ComponentModel.Composition;

namespace MefZoo
{
    [Export(typeof(IAnimal))]
	public class Mamut : IAnimal
	{
		public string Name
		{
			get { return string.Format("[{0}] I am the oldest", "Mamut"); }
		}

        public string GetTime(out string yourAge)
        {
            yourAge = "153";
            return DateTime.Now.AddHours(2).ToString();
        }
    }
}
