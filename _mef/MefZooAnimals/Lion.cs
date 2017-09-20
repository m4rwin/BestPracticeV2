using MefZooLib;
using System.ComponentModel.Composition;
using System;

namespace MefZooAnimals
{
    [Export(typeof(IAnimal))]
    public class Lion : IAnimal
    {
        public string Name
        {
            get { return $"[{nameof(Lion)}] I am a king of the jungle"; }
        }

        public virtual string GetTime(out string yourAge)
        {
            yourAge = "33";
            return DateTime.Now.AddHours(1).ToString();
        }
    }
}
