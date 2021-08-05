using BTimeLogger.Wpf.Mediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class GroupFilterViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;
		private readonly IMediator _mediator;

		private IEnumerable<ActivityViewModel> _allActivityVMs;

		public ActivityGroupSourceViewModel GroupsSource { get; } = new();

		public Activity SelectedActivity { get => GroupsSource.SelectedGroupActivity?.Activity ?? null; }

		public AsyncDelegateCommand ReloadCommand { get; }

		public event EventHandler NoActivityGroupSelected;
		public event EventHandler GroupSelected;

		public GroupFilterViewModel(
			IEventAggregator ea,
			IMediator mediator)
		{
			_ea = ea;
			_mediator = mediator;

			ReloadCommand = new AsyncDelegateCommand(Reload);

			ea.RegisterHandler<ReportSourceChanged>(msg => ReloadCommand.Execute());

			GroupsSource.PropertyChanged += (_, args) =>
			{
				if (GroupsSource.NoActivityGroupSelected)
					NoActivityGroupSelected?.Invoke(this, args);
				else
					GroupSelected?.Invoke(this, args);
			};

			ReloadCommand.Execute();
		}

		private async Task Reload(object param = null)
		{
			_allActivityVMs = await _mediator.Send(new GetAllActivityVMsQuery());
			PopulateGroups();
		}

		private void PopulateGroups()
		{
			GroupsSource.Items.Clear();
			foreach (ActivityViewModel activity in _allActivityVMs)
			{
				if (activity.IsGroup) GroupsSource.Items.Add(activity);
			}
			GroupsSource.Items.Add(new NoneItem());
		}
	}
}
