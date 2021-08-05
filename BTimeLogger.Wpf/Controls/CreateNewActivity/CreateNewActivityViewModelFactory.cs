using MediatR;

namespace BTimeLogger.Wpf.Controls
{
	public interface ICreateNewActivityViewModelFactory
	{
		CreateNewActivityViewModel Create();
	}

	class CreateNewActivityViewModelFactory : ICreateNewActivityViewModelFactory
	{
		private readonly IMediator _mediator;
		private readonly IGroupFilterViewModelFactory _groupFilterViewModelFactory;

		public CreateNewActivityViewModelFactory(IMediator mediator,
			IGroupFilterViewModelFactory groupFilterViewModelFactory)
		{
			_mediator = mediator;
			_groupFilterViewModelFactory = groupFilterViewModelFactory;
		}

		public CreateNewActivityViewModel Create()
		{
			GroupFilterViewModel groupFilterVM = _groupFilterViewModelFactory.Create();
			return new CreateNewActivityViewModel(_mediator, groupFilterVM);
		}
	}
}
