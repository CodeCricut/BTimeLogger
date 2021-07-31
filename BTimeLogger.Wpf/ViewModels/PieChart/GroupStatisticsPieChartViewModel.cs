using BTimeLogger.Domain;
using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Services;
using BTimeLogger.Wpf.Util;
using BTimeLogger.Wpf.ViewModels.Factories;
using BTimeLogger.Wpf.ViewModels.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.ViewModels.PieChart
{
	public class GroupStatisticsPieChartViewModel : PieChartViewModel
	{
		private readonly IGroupStatisticGenerator _groupStatsRepo;
		private readonly IGroupStatisticCategoriesConverter _statCategoryConverter;
		private readonly ICategoryViewModelFactory _catVMFactory;

		private GroupStatisticSearchFilter _groupStatisticSearchFilter = new();


		public GroupStatisticsPieChartViewModel(
			IGroupStatisticGenerator groupStatsRepo,
			IGroupStatisticCategoriesConverter statCategoryConverter,
			ICategoryViewModelFactory catVMFactory,
			IEventAggregator ea)
		{
			_groupStatsRepo = groupStatsRepo;
			_statCategoryConverter = statCategoryConverter;
			_catVMFactory = catVMFactory;

			ea.RegisterHandler<GroupStatisticsTypeChanged>(msg => HandleGroupStatTypeChanged(msg.NewGroupType));
			ea.RegisterHandler<TimeSpanChanged>(HandleTimeSpanChanged);
			ea.RegisterHandler<ReportSourceChanged>(HandleReportSourceChanged);


			UpdateChartCommand.Execute();
		}

		private void HandleTimeSpanChanged(TimeSpanChanged msg)
		{
			_groupStatisticSearchFilter.From = msg.From;
			_groupStatisticSearchFilter.To = msg.To;
			UpdateChartCommand.Execute(null);
		}

		private void HandleGroupStatTypeChanged(Activity newGroupType)
		{
			_groupStatisticSearchFilter.GroupType = newGroupType;
			UpdateChartCommand.Execute(null);
		}

		private void HandleReportSourceChanged(ReportSourceChanged msg)
		{
			UpdateChartCommand.Execute(null);
		}

		protected override Task<string> GetTitle()
		{
			return Task.FromResult(_groupStatisticSearchFilter.GroupType?.Code.Value ?? "Total");
		}

		protected override async Task<IEnumerable<CategoryViewModel>> GetCategories()
		{
			GroupStatistic groupStat = await _groupStatsRepo.CreateForGroup(_groupStatisticSearchFilter);
			IEnumerable<Category> childrenCategories = _statCategoryConverter.Convert(groupStat);

			List<CategoryViewModel> childCatVms = new();
			foreach (Category childCategory in childrenCategories)
				childCatVms.Add(_catVMFactory.Create(childCategory));

			return childCatVms;
		}
	}
}
