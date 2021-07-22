using BTimeLogger.Wpf.ViewModels.Domain;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class IntervalListItemViewModel : BaseViewModel
	{
		public IntervalViewModel Interval { get; }

		public bool IsLastOnDate { get; }

		public IntervalListItemViewModel(IntervalViewModel intervalVM, bool isLastOnDate)
		{
			Interval = intervalVM;
			IsLastOnDate = isLastOnDate;
		}
	}
}
