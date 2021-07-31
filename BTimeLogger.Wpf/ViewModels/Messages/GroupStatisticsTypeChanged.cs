namespace BTimeLogger.Wpf.ViewModels.Messages
{
	public class GroupStatisticsTypeChanged
	{
		public GroupStatisticsTypeChanged(Activity newGroupType)
		{
			NewGroupType = newGroupType;
		}

		public Activity NewGroupType { get; }
	}
}
