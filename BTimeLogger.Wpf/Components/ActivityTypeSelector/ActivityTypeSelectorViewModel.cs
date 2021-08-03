using BTimeLogger.Wpf.Mediator;
using BTimeLogger.Wpf.ViewModels.Domain;
using BTimeLogger.Wpf.ViewModels.Messages;
using MediatR;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.ViewModel;
using static BTimeLogger.Wpf.ViewModels.ActivityGroupSourceViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class ActivityTypeSelectorViewModel : BaseViewModel
	{
		private IEnumerable<ActivityViewModel> _allActivityVMs;

		public ObservableCollection<ActivityViewModel> ActivitiesSource { get; } = new();

		private ActivityViewModel _selectedActivity;
		private readonly IMediator _mediator;

		public ActivityViewModel SelectedActivity
		{
			get => _selectedActivity;
			set { Set(ref _selectedActivity, value); RaisePropertyChanged(nameof(NoneSelected)); }
		}

		public bool NoneSelected { get => SelectedActivity == null || SelectedActivity is NoneItem; }

		public AsyncDelegateCommand ReloadCommand { get; }

		public ActivityTypeSelectorViewModel(IEventAggregator ea, IMediator mediator)
		{
			_mediator = mediator;

			ReloadCommand = new AsyncDelegateCommand(Reload);
			ea.RegisterHandler<ReportSourceChanged>(msg => ReloadCommand.Execute());

			ReloadCommand.Execute();
		}

		public void SelectActivity(ActivityViewModel activity)
		{
			var sourceActivity = ActivitiesSource.FirstOrDefault(aVM => aVM.Activity.Code.Equals(activity.Activity.Code));
			//if (!ActivitiesSource.Contains(activity)) throw new Exception();

			SelectedActivity = sourceActivity;
		}

		private async Task Reload(object arg)
		{
			_allActivityVMs = await _mediator.Send(new GetAllActivityVMsQuery());
			PopulateActivities();
		}

		private void PopulateActivities()
		{
			ActivitiesSource.Clear();
			foreach (ActivityViewModel activity in _allActivityVMs)
			{
				if (!activity.IsGroup)
					ActivitiesSource.Add(activity);
			}

			ActivitiesSource.Add(new NoneItem());
		}
	}
}
