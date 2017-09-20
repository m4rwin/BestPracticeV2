using MefZooLib;
using System;
using System.ComponentModel.Composition;

namespace MefZooAnimals
{
    [Export(typeof(IAnimal))]
    public class Monkey : IAnimal
    {
        public string Name
        {
                get { return string.Format("[{0}] I am smartest than you are", "Monkey"); }
        }

        public string GetTime(out string yourAge)
        {
            yourAge = "9";
            return DateTime.Now.AddHours(5).ToString();
        }
    }
}
