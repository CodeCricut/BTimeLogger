namespace BTimeLogger.Wpf.Controls
{
	public class NewPieChartGroupSelected
	{
		public NewPieChartGroupSelected(Activity selectedGroup)
		{
			SelectedGroup = selectedGroup;
		}

		public Activity SelectedGroup { get; }
	}
}
