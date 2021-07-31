﻿namespace BTimeLogger.Wpf.ViewModels.Messages
{
	public class GroupStatisticsTypeChanged
	{
		public GroupStatisticsTypeChanged(Activity newGroupType)
		{
			NewGroupType = newGroupType;
		}

		public static GroupStatisticsTypeChanged NoGroup()
		{
			return new GroupStatisticsTypeChanged(null);
		}

		public Activity NewGroupType { get; }
	}
}
