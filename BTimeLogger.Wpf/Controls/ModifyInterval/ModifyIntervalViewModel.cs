using BTimeLogger.Csv.Helpers;
using BTimeLogger.Wpf.Mediator;
using MediatR;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Controls;

public class ModifyIntervalViewModel : BaseViewModel
{
	private readonly IMediator _mediator;
	private IntervalViewModel _intervalViewModel;

	public ActivityTypeSelectorViewModel ActivityTypeSelectorViewModel { get; }

	#region Date
	private DateTime _fromDate;
	public DateTime FromDate
	{
		get => _fromDate;
		set
		{
			Set(ref _fromDate, value);
			RaisePropertyChanged(nameof(FromDateTime));
			RaisePropertyChanged(nameof(DurationString));
			SaveCommand.RaiseCanExecuteChanged();
		}
	}

	private DateTime _toDate;
	public DateTime ToDate
	{
		get => _toDate;
		set
		{
			Set(ref _toDate, value);
			RaisePropertyChanged(nameof(ToDateTime));
			RaisePropertyChanged(nameof(DurationString));
			SaveCommand.RaiseCanExecuteChanged();
		}
	}
	#endregion
	#region Time
	private TimeSpan _fromTime;
	public TimeSpan FromTime
	{
		get => _fromTime;
		set
		{
			Set(ref _fromTime, value);
			RaisePropertyChanged(nameof(FromDateTime));
			RaisePropertyChanged(nameof(DurationString));
			SaveCommand.RaiseCanExecuteChanged();
		}
	}

	private TimeSpan _toTime;
	public TimeSpan ToTime
	{
		get => _toTime;
		set
		{
			Set(ref _toTime, value);
			RaisePropertyChanged(nameof(ToDateTime));
			RaisePropertyChanged(nameof(DurationString));
			SaveCommand.RaiseCanExecuteChanged();
		}
	}
	#endregion

	public DateTime FromDateTime { get => FromDate + FromTime; }
	public DateTime ToDateTime { get => ToDate + ToTime; }

	public string DurationString { get => (ToDateTime - FromDateTime).ToCsvFormat(); }

	private string _comment;
	public string Comment
	{
		get { return _comment; }
		set { _comment = value; }
	}

	private bool CanSave(object _) => !ActivityTypeSelectorViewModel.NoneSelected
		&& FromDateTime <= ToDateTime;
	public AsyncDelegateCommand SaveCommand { get; }

	public AsyncDelegateCommand DeleteCommand { get; }


	public event EventHandler InteractionFinished;
	public ModifyIntervalViewModel(IntervalViewModel intervalViewModel,
		ActivityTypeSelectorViewModel activityTypeSelectorViewModel,
		IMediator mediator)
	{
		_mediator = mediator;
		_intervalViewModel = intervalViewModel;

		ActivityTypeSelectorViewModel = activityTypeSelectorViewModel;


		SaveCommand = new AsyncDelegateCommand(Save, CanSave);
		DeleteCommand = new AsyncDelegateCommand(Delete);

		ActivityTypeSelectorViewModel.PropertyChanged += (_, _) => SaveCommand.RaiseCanExecuteChanged();

		UpdatePropsWithInterval();
	}

	private void TimeSpanPanelViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		RaisePropertyChanged(nameof(DurationString));
	}

	private async Task Save(object arg)
	{
		Interval updatedInterval = CreateIntervalWithProps();
		await _mediator.Send(new Mediator.ModifyInterval(updatedInterval));
		InteractionFinished?.Invoke(this, new());
	}


	private async Task Delete(object arg)
	{
		await _mediator.Send(new DeleteInterval(_intervalViewModel.Interval));
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
			Duration = ToDateTime - FromDateTime,
			Guid = _intervalViewModel.Interval.Guid
		};
	}

	private void UpdatePropsWithInterval()
	{
		ActivityTypeSelectorViewModel.SelectActivity(_intervalViewModel.Activity);

		FromDate = _intervalViewModel.Interval.From.Date;
		ToDate = _intervalViewModel.Interval.To.Date;

		FromTime = _intervalViewModel.Interval.From.TimeOfDay;
		ToTime = _intervalViewModel.Interval.To.TimeOfDay;

		Comment = _intervalViewModel.Comment;
	}
}
