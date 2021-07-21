using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.ViewModels.Messages
{
	class IncludedActivitiesChanged
	{
		public IncludedActivitiesChanged(Activity[] activities)
		{
			NewIncludedActivities = activities;
		}

		public Activity[] NewIncludedActivities { get; }
	}
}
