using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.Domain
{
	public class IntervalListItemViewModel : BaseViewModel
	{
		private readonly Interval _interval;

		// TODO
		public IntervalListItemViewModel(Interval interval)
		{
			_interval = interval;
		}
	}
}
