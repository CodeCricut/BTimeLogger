using BTimeLogger.Wpf.ViewModels.Messages;
using System;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	// TODO: verify To after From, error message prop
	public class TimeSpanPanelViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;

		private DateTime _from = DateTime.Now;
		public DateTime From
		{
			get => _from;
			set { Set(ref _from, value); }
		}

		private DateTime _to = DateTime.Now;

		public DateTime To
		{
			get => _to;
			set { Set(ref _to, value); }
		}

		public TimeSpanPanelViewModel(IEventAggregator ea)
		{
			_ea = ea;

			PropertyChanged += (_, e) => _ea.SendMessage(new TimeSpanChanged(From, To)); ;

			ea.RegisterHandler<TimeSpanChanged>(HandleTimeSpanChanged);
		}

		private void HandleTimeSpanChanged(TimeSpanChanged msg)
		{
			From = msg.From;
			To = msg.To;
		}
	}
}
