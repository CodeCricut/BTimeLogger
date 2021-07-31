using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Services;
using BTimeLogger.Wpf.ViewModels.PieChart;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IGroupStatisticsPieChartViewModelFactory
	{
		GroupStatisticsPieChartViewModel Create();
	}

	class GroupStatisticsPieChartViewModelFactory : IGroupStatisticsPieChartViewModelFactory
	{
		private readonly IGroupStatisticGenerator _groupStatisticRepository;
		private readonly IGroupStatisticCategoriesConverter _statCategoryConvertr;
		private readonly ICategoryViewModelFactory _categoryViewModelFactory;
		private readonly IEventAggregator _ea;

		public GroupStatisticsPieChartViewModelFactory(IGroupStatisticGenerator groupStatisticRepository,
			IGroupStatisticCategoriesConverter statCategoryConvertr,
			ICategoryViewModelFactory categoryViewModelFactory,
			IEventAggregator ea)
		{
			_groupStatisticRepository = groupStatisticRepository;
			_statCategoryConvertr = statCategoryConvertr;
			_categoryViewModelFactory = categoryViewModelFactory;
			_ea = ea;
		}
		public GroupStatisticsPieChartViewModel Create()
		{
			return new GroupStatisticsPieChartViewModel(_groupStatisticRepository, _statCategoryConvertr, _categoryViewModelFactory, _ea);
		}
	}
}
