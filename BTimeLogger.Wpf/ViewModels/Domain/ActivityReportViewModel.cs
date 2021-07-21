//using BTimeLogger.Wpf.ViewModels.Messages;
//using System;
//using System.Collections.ObjectModel;
//using WpfCore.MessageBus;
//using WpfCore.ViewModel;

//namespace BTimeLogger.Wpf.ViewModels.Domain
//{
//	public class ActivityReportViewModel : BaseViewModel
//	{
//		/*
//		 * TODO:
//		 * this isn't exactly a view model... It isn't directly responsible for converting a model to be displayed by the view.
//		 * Instead, it is mostly concerned with being a principal for ActivityReport, and handling global changes to ActivityReport.
//		 * 
//		 */
//		private ActivityReport _activityReport;
//		public ActivityReport ActivityReport
//		{
//			get => _activityReport;
//			set
//			{
//				if (_activityReport == value) return;

//				_activityReport = value;
//				RaisePropertyChanged();
//				RaisePropertyChanged(nameof(From));
//				RaisePropertyChanged(nameof(To));
//			}
//		}

//		//public DateTime From
//		//{
//		//	get => _activityReport.From; set
//		//	{
//		//		if (_activityReport.From != value)
//		//		{
//		//			_activityReport.From = value;
//		//			RaisePropertyChanged();
//		//		}
//		//	}
//		//}

//		//public DateTime To
//		//{
//		//	get => _activityReport.To; set
//		//	{
//		//		if (_activityReport.To != value)
//		//		{
//		//			_activityReport.To = value;
//		//			RaisePropertyChanged();
//		//		}
//		//	}
//		//}

//		//public ObservableCollection<Statistic> Statistics { get; } = new();

//		//public ObservableCollection<Interval> Intervals { get; } = new();

//		public ActivityReportViewModel(IEventAggregator ea)
//		{
//			ea.RegisterHandler<ActivityReportChanged>(HandleReportChanged);
//		}

//		private void HandleReportChanged(ActivityReportChanged msg)
//		{
//			ActivityReport = msg.NewReport;
//			//UpdateStatistics();
//			//UpdateIntervals();
//		}

//		//private void UpdateStatistics()
//		//{
//		//	Statistics.Clear();
//		//	foreach (Statistic stat in ActivityReport.Statistics)
//		//	{
//		//		Statistics.Add(stat);
//		//	}
//		//}

//		//private void UpdateIntervals()
//		//{
//		//	Intervals.Clear();
//		//	foreach (Interval interval in ActivityReport.Intervals)
//		//	{
//		//		Intervals.Add(interval);
//		//	}
//		//}
//	}
//}
