using MefZooLib;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MefZoo
{
    public class Zoo
	{
		[ImportMany(typeof(IAnimal))]
		public IEnumerable<IAnimal> Animals { get; set; }

        
    }
}
