using BTimeLogger.Domain;
using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Model;
using System;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Util
{
	public static class GroupStatisticRepositoryExtensions
	{
		public static Task<GroupStatistic> CreateForGroup(this IGroupStatisticGenerator groupStatisticRepository, GroupStatisticSearchFilter searchFilter)
		{
			// TODO: change back
			return groupStatisticRepository.GenerateForGroup(searchFilter.GroupType, DateTime.MinValue, DateTime.MaxValue);
			//return groupStatisticRepository.GenerateForGroup(searchFilter.GroupType, searchFilter.From, searchFilter.To);
		}
	}
}
