using BTimeLogger.Domain;
using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Model;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Util;

public static class GroupStatisticRepositoryExtensions
{
	public static Task<GroupStatistic> CreateForGroup(this IGroupStatisticGenerator groupStatisticRepository, GroupStatisticSearchFilter searchFilter)
	{
		return groupStatisticRepository.GenerateForGroup(searchFilter.GroupType, searchFilter.From, searchFilter.To);
	}
}
