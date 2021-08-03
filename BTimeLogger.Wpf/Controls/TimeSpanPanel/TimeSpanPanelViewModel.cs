using System;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	// TODO: verify To after From, error message prop
	public class TimeSpanPanelViewModel : BaseViewModel
	{
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
	}
}
