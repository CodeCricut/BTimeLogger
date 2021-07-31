using BTimeLogger.Domain.Helpers;
using BTimeLogger.Wpf.ViewModels.Domain;
using System.Collections.Generic;
using System.Linq;
using static BTimeLogger.Activity;

namespace BTimeLogger.Wpf.Util
{
	public static class ActivityViewModelEnumerableExtensions
	{
		public static IEnumerable<Activity> SelectActivities(this IEnumerable<ActivityViewModel> activityVMs)
			=> activityVMs.Select(vm => vm.Activity);

		public static IEnumerable<ActivityCode> SelectActivityCodes(this IEnumerable<ActivityViewModel> activityVMs)
			=> activityVMs.Select(vm => vm.Activity).SelectCodes();
	}
}
