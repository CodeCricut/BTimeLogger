using BTimeLogger.Wpf.Services.AppData;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfCore.Commands;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class HomeViewModel : BaseViewModel
	{
		private readonly IReportLocationsPrincipal _reportLocationsPrincipal;

		public ObservableCollection<string> ReportLocations { get; } = new();

		public ICommand LoadCommand { get; }

		public HomeViewModel(IReportLocationsPrincipal reportLocationsPrincipal)
		{
			LoadCommand = new DelegateCommand(Load);
			_reportLocationsPrincipal = reportLocationsPrincipal;
		}

		private void Load(object obj)
		{
			ReportLocations.Clear();
			foreach (string location in _reportLocationsPrincipal.GetReportLocations())
			{
				ReportLocations.Add(location);
			}
		}
	}
}
