using MediatR;
using System;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class CreateNewActivityViewModel : BaseViewModel
	{
		private readonly IMediator _mediator;

		#region Form props
		private string _activityName;
		public string ActivityName
		{
			get { return _activityName; }
			set { Set(ref _activityName, value); CreateCommand.RaiseCanExecuteChanged(); }
		}

		public GroupFilterViewModel GroupFilterViewModel { get; }

		private bool _isGroup;
		public bool IsGroup
		{
			get { return _isGroup; }
			set { Set(ref _isGroup, value); CreateCommand.RaiseCanExecuteChanged(); }
		}

		private bool _invalidActivityInfo;
		public bool InvalidActivityInfo
		{
			get { return _invalidActivityInfo; }
			set { Set(ref _invalidActivityInfo, value); }
		}
		#endregion

		public DelegateCommand CancelCommand { get; }
		public AsyncDelegateCommand CreateCommand { get; }

		public event EventHandler InteractionFinished;

		public CreateNewActivityViewModel(IMediator mediator,
			GroupFilterViewModel groupFilterViewModel)
		{
			_mediator = mediator;
			GroupFilterViewModel = groupFilterViewModel;

			CancelCommand = new DelegateCommand(Cancel);
			CreateCommand = new AsyncDelegateCommand(Create, CanCreate);
		}

		private async Task Create(object obj)
		{
			try
			{
				InvalidActivityInfo = false;

				Activity activity = CreateActivityWithProps();
				await _mediator.Send(new Mediator.CreateNewActivity(activity));
				InteractionFinished?.Invoke(this, new());
			}
			catch (Exception)
			{
				InvalidActivityInfo = true;
			}
		}

		private Activity CreateActivityWithProps()
		{
			Activity parent = GroupFilterViewModel.GroupsSource.NoneItemSelected
				? null
				: GroupFilterViewModel.SelectedActivity;
			return new()
			{
				Name = ActivityName,
				IsGroup = IsGroup,
				Parent = parent,
			};
		}

		private bool CanCreate(object arg)
		{
			return !string.IsNullOrWhiteSpace(ActivityName);
		}

		private void Cancel(object obj)
		{
			InteractionFinished?.Invoke(this, new());
		}
	}
}
