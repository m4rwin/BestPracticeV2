using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace EnterpriseLibraryManager
{
	class Program
	{
		static void Main(string[] args)
		{
			TextFormatter briefFormatter = new TextFormatter();

			var flatFileTraceListerner = new FlatFileTraceListener(@"C:\temp\xxx.log",
				"-------------------------------",
				"-------------------------------",
				briefFormatter);

			var config = new LoggingConfiguration();
			config.AddLogSource("my_log", System.Diagnostics.SourceLevels.All, true).AddTraceListener(flatFileTraceListerner);
			LogWriter logger = new LogWriter(config);
		}
	}
}
