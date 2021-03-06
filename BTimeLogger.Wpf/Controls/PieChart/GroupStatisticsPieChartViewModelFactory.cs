using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Services;

namespace BTimeLogger.Wpf.Controls;

public interface IGroupStatisticsPieChartViewModelFactory
{
	GroupStatisticsPieChartViewModel Create();
}

class GroupStatisticsPieChartViewModelFactory : IGroupStatisticsPieChartViewModelFactory
{
	private readonly IGroupStatisticGenerator _groupStatisticRepository;
	private readonly IStatisticCategoryConverter _statCategoryConvertr;
	private readonly ICategoryViewModelFactory _categoryViewModelFactory;
	private readonly IPieSliceViewModelFactory _pieSliceVMFactory;
	private readonly IEventAggregator _ea;
	private readonly IActivityRepository _activityRepository;

	public GroupStatisticsPieChartViewModelFactory(IGroupStatisticGenerator groupStatisticRepository,
		IStatisticCategoryConverter statCategoryConvertr,
		ICategoryViewModelFactory categoryViewModelFactory,
		IPieSliceViewModelFactory pieSliceVMFactory,
		IEventAggregator ea,
		IActivityRepository activityRepository)
	{
		_groupStatisticRepository = groupStatisticRepository;
		_statCategoryConvertr = statCategoryConvertr;
		_categoryViewModelFactory = categoryViewModelFactory;
		_pieSliceVMFactory = pieSliceVMFactory;
		_ea = ea;
		_activityRepository = activityRepository;
	}
	public GroupStatisticsPieChartViewModel Create()
	{
		return new GroupStatisticsPieChartViewModel(_groupStatisticRepository, _statCategoryConvertr, _categoryViewModelFactory, _ea,
			_pieSliceVMFactory, _activityRepository);
	}
}
