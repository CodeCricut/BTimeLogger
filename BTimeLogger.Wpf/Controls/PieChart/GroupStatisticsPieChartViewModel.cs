using BTimeLogger.Domain;
using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Services;
using BTimeLogger.Wpf.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.Controls
{
	public class GroupStatisticsPieChartViewModel : PieChartViewModel
	{
		private readonly IGroupStatisticGenerator _groupStatsRepo;
		private readonly IGroupStatisticCategoriesConverter _statCategoryConverter;
		private readonly ICategoryViewModelFactory _catVMFactory;
		private readonly IEventAggregator _ea;
		private readonly IActivityRepository _activityRepository;
		private GroupStatisticSearchFilter _groupStatisticSearchFilter = new();

		public GroupStatisticsPieChartViewModel(
			IGroupStatisticGenerator groupStatsRepo,
			IGroupStatisticCategoriesConverter statCategoryConverter,
			ICategoryViewModelFactory catVMFactory,
			IEventAggregator ea,
			IPieSliceViewModelFactory pieSliceVMFactory,
			IActivityRepository activityRepository) : base(pieSliceVMFactory)
		{
			_groupStatsRepo = groupStatsRepo;
			_statCategoryConverter = statCategoryConverter;
			_catVMFactory = catVMFactory;
			_ea = ea;
			_activityRepository = activityRepository;
			ea.RegisterHandler<GroupStatisticsTypeChanged>(msg => HandleGroupStatTypeChanged(msg.NewGroupType));
			ea.RegisterHandler<ReportSourceChanged>(HandleReportSourceChanged);
			ea.RegisterHandler<StatisticsTimeSpanChanged>(HandleTimeSpanChanged);

			UpdateChartCommand.Execute();
		}

		protected override async Task SelectCategory(object selectedCategoryId)
		{
			try
			{
				ActivityCode selectedCategoryCode = ActivityCode.CreateCode(selectedCategoryId as string);
				Activity selectedCategory = await _activityRepository.GetActivity(selectedCategoryCode);
				if (selectedCategory == null || !selectedCategory.IsGroup) throw new System.Exception();

				_ea.SendMessage(new NewPieChartGroupSelected(selectedCategory));
			}
			catch (System.Exception)
			{
			}
		}

		private void HandleTimeSpanChanged(StatisticsTimeSpanChanged msg)
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
