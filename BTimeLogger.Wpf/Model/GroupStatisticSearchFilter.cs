using System;

namespace BTimeLogger.Wpf.Model
{
	public class GroupStatisticSearchFilter
	{
		public Activity GroupType { get; set; }
		public DateTime From { get; set; } = DateTime.Now;
		public DateTime To { get; set; } = DateTime.Now;
	}
}
