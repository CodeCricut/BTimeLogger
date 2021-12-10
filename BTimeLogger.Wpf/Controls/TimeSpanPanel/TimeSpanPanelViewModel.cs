using System;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	// TODO Issue #5: Add date validation and error message property
	public class TimeSpanPanelViewModel : BaseViewModel
	{
		private DateTime _from = DateTime.Now.Date;
		public DateTime From
		{
			get => _from;
			set { Set(ref _from, value); }
		}

		private DateTime _to = DateTime.Now.Date;

		public DateTime To
		{
			get => _to;
			set { Set(ref _to, value); }
		}
	}
}
