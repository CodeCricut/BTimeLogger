using BTimeLogger.Wpf.ViewModels.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BTimeLogger.Wpf.Util
{
	public static class ActivityViewModelEnumerableExtensions
	{
		public static IEnumerable<Activity> SelectActivities(this IEnumerable<ActivityViewModel> activityVMs)
			=> activityVMs.Select(vm => vm.Activity);
	}
}
