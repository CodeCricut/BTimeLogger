using static BTimeLogger.Activity;

namespace BTimeLogger.Wpf.ViewModels.Messages
{
	public class GroupStatisticsTypeChanged
	{
		public GroupStatisticsTypeChanged(ActivityCode newGroupType)
		{
			NewGroupType = newGroupType;
		}

		public ActivityCode NewGroupType { get; }
	}
}
