using BTimeLogger.Csv.Helpers;
using MediatR;
using System;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class CreateNewIntervalViewModel : BaseViewModel
	{
		private readonly IMediator _mediator;

		public ActivityTypeSelectorViewModel ActivityTypeSelectorViewModel { get; }

		#region Date
		private DateTime _fromDate = DateTime.Now.Date;
		public DateTime FromDate
		{
			get => _fromDate;
			set
			{
				Set(ref _fromDate, value);
				RaisePropertyChanged(nameof(FromDateTime));
				RaisePropertyChanged(nameof(DurationString));
			}
		}

		private DateTime _toDate = DateTime.Now.Date;
		public DateTime ToDate
		{
			get => _toDate;
			set
			{
				Set(ref _toDate, value);
				RaisePropertyChanged(nameof(ToDateTime));
				RaisePropertyChanged(nameof(DurationString));
			}
		}
		#endregion
		#region Time
		private TimeSpan _fromTime = DateTime.Now.TimeOfDay;
		public TimeSpan FromTime
		{
			get => _fromTime;
			set
			{
				Set(ref _fromTime, value);
				RaisePropertyChanged(nameof(FromDateTime));
				RaisePropertyChanged(nameof(DurationString));
			}
		}

		private TimeSpan _toTime = DateTime.Now.TimeOfDay;
		public TimeSpan ToTime
		{
			get => _toTime;
			set
			{
				Set(ref _toTime, value);
				RaisePropertyChanged(nameof(ToDateTime));
				RaisePropertyChanged(nameof(DurationString));
			}
		}
		#endregion

		public DateTime FromDateTime { get => FromDate + FromTime; }
		public DateTime ToDateTime { get => ToDate + ToTime; }

		public string DurationString { get => (ToDateTime - FromDateTime).ToCsvFormat(); }

		private string _comment = string.Empty;
		public string Comment
		{
			get { return _comment; }
			set { Set(ref _comment, value); }
		}

		private bool CanCreateNewInterval(object _) => !ActivityTypeSelectorViewModel.NoneSelected;

		public AsyncDelegateCommand CreateIntervalCommand { get; }

		public event EventHandler InteractionFinished;
		public CreateNewIntervalViewModel(ActivityTypeSelectorViewModel activityTypeSelectorViewModel,
			IMediator mediator)
		{
			_mediator = mediator;

			ActivityTypeSelectorViewModel = activityTypeSelectorViewModel;

			CreateIntervalCommand = new AsyncDelegateCommand(CreateNewInterval, CanCreateNewInterval);

			ActivityTypeSelectorViewModel.PropertyChanged += (_, _) => CreateIntervalCommand.RaiseCanExecuteChanged();
		}

		private void TimeSpanPanelViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			RaisePropertyChanged(nameof(DurationString));
		}

		private async Task CreateNewInterval(object arg)
		{
			Interval updatedInterval = CreateIntervalWithProps();
			await _mediator.Send(new Mediator.CreateNewInterval(updatedInterval));
			InteractionFinished?.Invoke(this, new());
		}

		private Interval CreateIntervalWithProps()
		{
			return new Interval()
			{
				Activity = ActivityTypeSelectorViewModel.SelectedActivity.Activity,
				Comment = Comment,
				From = FromDateTime,
				To = ToDateTime,
				Duration = ToDateTime - FromDateTime
			};
		}
	}
}
