using BTimeLogger.Wpf.ViewModels.Messages;
using BTimeLogger.Wpf.ViewModels.PieChart;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class StatisticsViewModel : BaseViewModel
	{
		public GroupStatisticsPieChartViewModel GroupStatisticsPieChartViewModel { get; }

		public GroupFilterViewModel GroupFilterViewModel { get; }

		public TimeSpanPanelViewModel TimeSpanPanelViewModel { get; }


		public StatisticsViewModel(GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel,
			GroupFilterViewModel groupFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel,
			IEventAggregator ea)
		{
			GroupStatisticsPieChartViewModel = groupStatisticsPieChartViewModel;
			GroupFilterViewModel = groupFilterViewModel;
			TimeSpanPanelViewModel = timeSpanPanelViewModel;

			GroupFilterViewModel.GroupsSource.PropertyChanged += (_, args) =>
			{
				if (GroupFilterViewModel.GroupsSource.NoActivityGroupSelected)
					ea.SendMessage(GroupStatisticsTypeChanged.NoGroup());
				else
					ea.SendMessage(new GroupStatisticsTypeChanged(GroupFilterViewModel.GroupsSource.SelectedGroupActivity.Activity));
			};
		}
	}
}
