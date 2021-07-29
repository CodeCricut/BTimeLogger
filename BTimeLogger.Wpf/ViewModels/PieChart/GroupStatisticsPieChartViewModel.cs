using BTimeLogger.Domain;
using BTimeLogger.Domain.Repositories;
using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Services;
using BTimeLogger.Wpf.ViewModels.Factories;
using BTimeLogger.Wpf.ViewModels.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfCore.MessageBus;
using static BTimeLogger.Activity;

namespace BTimeLogger.Wpf.ViewModels.PieChart
{
	public class GroupStatisticsPieChartViewModel : PieChartViewModel
	{
		private readonly IGroupStatisticRepository _groupStatsRepo;
		private readonly IGroupStatisticCategoriesConverter _statCategoryConverter;
		private readonly ICategoryViewModelFactory _catVMFactory;


		private ActivityCode _groupStatisticType = null;

		public GroupStatisticsPieChartViewModel(
			IGroupStatisticRepository groupStatsRepo,
			IGroupStatisticCategoriesConverter statCategoryConverter,
			ICategoryViewModelFactory catVMFactory,
			IEventAggregator ea)
		{
			_groupStatsRepo = groupStatsRepo;
			_statCategoryConverter = statCategoryConverter;
			_catVMFactory = catVMFactory;

			ea.RegisterHandler<GroupStatisticsTypeChanged>(msg => HandleGroupStatTypeChanged(msg.NewGroupType));
			ea.RegisterHandler<ReportSourceChanged>(HandleReportSourceChanged);

			UpdateChartCommand.Execute();
		}

		private void HandleGroupStatTypeChanged(ActivityCode newGroupType)
		{
			_groupStatisticType = newGroupType;
			UpdateChartCommand.Execute(null);
		}

		private void HandleReportSourceChanged(ReportSourceChanged msg)
		{
			UpdateChartCommand.Execute(null);
		}

		protected override Task<string> GetTitle()
		{
			return Task.FromResult(_groupStatisticType?.Value ?? "Total");
		}

		protected override async Task<IEnumerable<CategoryViewModel>> GetCategories()
		{
			GroupStatistic groupStat = await _groupStatsRepo.CreateForGroup(_groupStatisticType);
			IEnumerable<Category> childrenCategories = _statCategoryConverter.Convert(groupStat);

			List<CategoryViewModel> childCatVms = new();
			foreach (Category childCategory in childrenCategories)
				childCatVms.Add(_catVMFactory.Create(childCategory));

			return childCatVms;
		}
	}
}
