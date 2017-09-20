using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefZooAnimals
{
	public class TRex : Lion
	{
		public override string GetTime(out string yourAge)
		{
			yourAge = "999";
			return base.GetTime(out yourAge);
		}
	}
}
