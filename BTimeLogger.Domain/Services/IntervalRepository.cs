using BTimeLogger.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger.Domain.Services
{
	public interface IIntervalRepository
	{
		Task<IEnumerable<Interval>> GetIntervals();
		Task<IEnumerable<Interval>> GetIntervals(IEnumerable<ActivityCode> activityCodes, DateTime? from, DateTime? to);
		Task<IEnumerable<Interval>> GetIntervals(ActivityCode activityCode, DateTime? from, DateTime? to);
		Task AddInterval(Interval interval);

		Task ClearIntervals();
	}

	class IntervalRepository : IIntervalRepository
	{
		private readonly List<Interval> _intervals = new();

		public Task AddInterval(Interval interval)
		{
			return Task.Factory.StartNew(() => _intervals.Add(interval));
		}

		public Task<IEnumerable<Interval>> GetIntervals()
		{
			return Task.FromResult(_intervals.AsEnumerable());
		}

		public async Task<IEnumerable<Interval>> GetIntervals(IEnumerable<ActivityCode> activityTypes, DateTime? from, DateTime? to)
		{
			IEnumerable<Interval> intervals = await GetIntervals();

			return intervals
				.BetweenDates(from, to, useOnlyDate: true)
				.OfActivityTypesOrAll(activityTypes);
		}

		public Task ClearIntervals()
		{
			_intervals.Clear();
			return Task.CompletedTask;
		}

		public Task<IEnumerable<Interval>> GetIntervals(ActivityCode activityCode, DateTime? from, DateTime? to)
		{
			return GetIntervals(new ActivityCode[] { activityCode }, from, to);
		}
	}
}
