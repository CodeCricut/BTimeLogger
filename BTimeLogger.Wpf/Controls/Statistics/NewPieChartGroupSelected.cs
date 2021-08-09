namespace BTimeLogger.Wpf.Controls
{
	class NewPieChartGroupSelected
	{
		public NewPieChartGroupSelected(Activity selectedGroup)
		{
			SelectedGroup = selectedGroup;
		}

		public Activity SelectedGroup { get; }
	}
}
