using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.ViewModels.Messages
{
	class ActivityReportChanged
	{
		public ActivityReportChanged(ActivityReport report)
		{
			NewReport = report;
		}

		public ActivityReport NewReport { get; }
	}
}
