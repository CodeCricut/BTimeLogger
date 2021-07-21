//using BTimeLogger.Domain.Reporters;
//using BTimeLogger.Wpf.ViewModels.Messages;
//using System;
//using WpfCore.MessageBus;
//using WpfCore.ViewModel;

//namespace BTimeLogger.Wpf.ViewModels.Domain
//{
//	public class IntervalsReportViewModel : BaseViewModel
//	{
//		private readonly IEventAggregator _ea;
//		private readonly IIntervalsReporter _intervalsReporter;

//		private readonly ActivityReportViewModel _activityReportViewModel;

//		private IntervalsReport _intervalsReport;
//		private IntervalsReport IntervalsReport
//		{
//			get => _intervalsReport;
//			set
//			{
//				if (_intervalsReport == value) return;

//				_intervalsReport = value;
//				RaisePropertyChanged(ALL_PROPS_CHANGED);
//			}
//		}

//		// TODO: wrapper props

//		public IntervalsReportViewModel(
//			IEventAggregator ea,
//			ActivityReportViewModel activityReportViewModel,
//			IIntervalsReporter intervalsReporter)
//		{
//			_ea = ea;
//			_activityReportViewModel = activityReportViewModel;
//			_intervalsReporter = intervalsReporter;

//			ea.RegisterHandler<ActivityReportChanged>(HandleActivityReportChanged);
//			ea.RegisterHandler<IncludedActivitiesChanged>(HandleIncludedActivitiesChanged)
//		}

//		private void HandleIncludedActivitiesChanged(IncludedActivitiesChanged msg)
//		{
//			Activity[] includedActivities = msg.NewIncludedActivities;
//			IntervalsReport = _intervalsReporter.Report()
//		}

//		private void HandleActivityReportChanged(ActivityReportChanged msg)
//		{
//			ActivityReport newReport = msg.NewReport;
//			IntervalsReport = _intervalsReporter.Report(newReport, _intervalsReport.IncludedActivities, _intervalsReport.From, _intervalsReport.To);
//		}

//		private IntervalsReport UpdateIntervalsReport(ActivityReportChanged obj)
//		{
//			return _intervalsReporter.Report(obj.NewReport, );
//		}

//		private void ActivityReportVM_PropertyChagned(object sender, System.ComponentModel.PropertyChangedEventArgs e)
//		{
//		}

//		//private void HandleReportChanged(IntervalsReportChanged msg)
//		//{
//		//	IntervalsReport = msg.NewReport;
//		//}
//	}
//}
