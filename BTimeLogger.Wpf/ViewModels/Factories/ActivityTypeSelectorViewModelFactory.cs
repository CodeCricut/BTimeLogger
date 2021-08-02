﻿using MediatR;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IActivityTypeSelectorViewModelFactory
	{
		public ActivityTypeSelectorViewModel Create();
	}

	class ActivityTypeSelectorViewModelFactory : IActivityTypeSelectorViewModelFactory
	{
		private readonly IEventAggregator _ea;
		private readonly IMediator _mediator;

		public ActivityTypeSelectorViewModelFactory(IEventAggregator ea, IMediator mediator)
		{
			_ea = ea;
			_mediator = mediator;
		}
		public ActivityTypeSelectorViewModel Create()
		{
			return new ActivityTypeSelectorViewModel(_ea, _mediator);
		}
	}
}